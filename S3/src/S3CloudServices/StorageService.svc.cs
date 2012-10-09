using System;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using Shared;
using Shared.Dto;
using Shared.ServiceContracts;
using Shared.ServiceProxy;

namespace S3CloudServices
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class StorageService : IGatewayService
    {
        private readonly Storage storage;

        private const string PreAddress = "http://macmsc.cloudapp.net/PreService.svc";
        //private const string PreAddress = "http://localhost:8080/s3pre";

        public StorageService()
        {
            this.storage = new Storage();
        }

        public void InitializeSystem(Guid dataOwnerId, byte[] dataOwnerName, byte[] rootRoleName, byte[] signPublicKey)
        {
            Guid doId = this.storage.GetDataOwnerUserId();
            if (doId != Guid.Empty)
            {
                //throw new InvalidOperationException("System has already been initialized");
            }

            Role r = new Role();
            GiveAllPermissionsToRole(r);
            r.Id = Guid.NewGuid();
            r.IsRoot = true;
            r.Name = rootRoleName;

            User u = new User();
            u.Id = dataOwnerId;
            u.Name = dataOwnerName;
            u.SignPublicKey = signPublicKey;

            r.Users.Add(u.Id);

            this.storage.InitializeSystem(u, r);
        }

        public void CreateUser(Guid userId, Guid roleId, User newUser)
        {
            if (newUser.Id == Guid.Empty) throw new ArgumentException("userId");
            if (newUser.Name == null || newUser.Name.Length == 0) throw new ArgumentException("userName");
            if (newUser.SignPublicKey == null) throw new ArgumentException("userSignPublicKey");

            AssertSystemIsInitialized();

            User performingUser = GetAssertUser(userId);

            Role role = GetAssertRole(roleId);

            AssertUserHasRoleOrAncestorOfRole(performingUser, role.Id);

            if (!role.CanCreateUsers)
            {
                throw new InvalidOperationException("Specified role cannot create users");
            }

            User newExistingUser = this.storage.GetUser(newUser.Id);
            if (newExistingUser != null)
            {
                throw new NotImplementedException("Support for \"recreating\" or restoring users is not implemented in the prototype");
            }

            // is null if new user is not created by the real DO who is the only one who can create new key pairs
            // and delegation tokens. In this case the parent keys and tokens are reused
            newUser.DelegationToken = newUser.DelegationToken ?? performingUser.DelegationToken;

            this.storage.CreateUser(newUser);

            if (!role.Users.Contains(newUser.Id))
            {
                role.Users.Add(newUser.Id);
                this.storage.UpdateRole(role);
            }
        }

        public void CreateSubRole(Guid userId, Guid roleId, Role newRole)
        {
            if (newRole == null) throw new ArgumentNullException("newRole");
            if (newRole.Id == Guid.Empty) throw new ArgumentException("newRoleId");
            if (newRole.Name == null || newRole.Name.Length == 0) throw new ArgumentException("newRoleName");

            AssertSystemIsInitialized();

            User user = GetAssertUser(userId);

            Role role = GetAssertRole(roleId);

            AssertUserHasRoleOrAncestorOfRole(user, role.Id);

            Role existingRole = this.storage.GetRole(newRole.Id);
            if (existingRole != null)
            {
                throw new InvalidOperationException("Role with specified id already exists");
            }

            if (!role.CanManageSubRoles && !newRole.IsRoot)
            {
                throw new InvalidOperationException("The specified role cannot create sub-roles");
            }

            if (!role.CanCreateRoot && newRole.IsRoot)
            {
                throw new InvalidOperationException("The specified role cannot create root roles");
            }

            if (newRole.IsRoot && (newRole.Users == null || newRole.Users.Count == 0))
            {
                throw new InvalidOperationException("When creating a root role at least one user must be added at creation time");
            }

            if (newRole.IsRoot && newRole.DataEntities != null && newRole.DataEntities.Count != 0)
            {
                throw new InvalidOperationException("A root role cannot include existing data entities");
            }

            if (!role.IsRoot)
            {
                AssertRoleIsSubsetOfParent(role, newRole);
            }
            else
            {
                GiveAllPermissionsToRole(role);
            }

            role.ChildRoles.Add(newRole.Id);
            this.storage.UpdateRole(role);

            this.storage.CreateRole(newRole);
        }

        public void UpdateSubRole(Guid userId, Guid roleId, Role updatedSubRole)
        {
            if (updatedSubRole == null) throw new ArgumentNullException("updatedSubRole");
            if (updatedSubRole.Id == Guid.Empty) throw new ArgumentException("roleId");
            if (updatedSubRole.Name == null || updatedSubRole.Name.Length == 0) throw new ArgumentException("roleName");

            AssertSystemIsInitialized();

            User user = GetAssertUser(userId);

            Role existingSubRole = this.storage.GetRole(updatedSubRole.Id);
            if (existingSubRole == null)
            {
                throw new InvalidOperationException("The role you are trying to update does not exist");
            }

            // the user may see only a subset of the actual child roles (cannot see root roles), 
            // so restore the collection here.
            updatedSubRole.ChildRoles = existingSubRole.ChildRoles;

            Role role = GetAssertRole(roleId);
            AssertUserHasRoleOrAncestorOfRole(user, role.Id);

            if (existingSubRole.IsRoot)
            {
                throw new InvalidOperationException("You cannot update a root role");
            }



            AssertRoleIsSubsetOfParent(role, updatedSubRole);

            if (IsRoleProperSubsetOfExisting(existingSubRole, updatedSubRole))
            {
                RestrictChildrenAccordingToRoleRecursively(updatedSubRole, updatedSubRole.ChildRoles);
            }

            this.storage.UpdateRole(updatedSubRole);
        }

        public void DeleteSubRole(Guid userId, Guid roleId, Guid roleToDeleteId)
        {
            AssertSystemIsInitialized();

            User user = GetAssertUser(userId);

            Role existingRole = this.storage.GetRole(roleToDeleteId);
            if (existingRole == null)
            {
                // role to delete does not exist
                return;
            }

            Role parentRole = null;

            if (existingRole.IsRoot)
            {
                AssertUserHasRole(user, existingRole);
                parentRole = GetParentOfRole(this.storage.GetDataOwnerRole(), roleToDeleteId);
            }
            else
            {
                Role potentialParentRole = GetAssertRole(roleId);
                AssertUserHasRoleOrAncestorOfRole(user, potentialParentRole.Id);

                if (potentialParentRole.ChildRoles.Contains(roleToDeleteId))
                {
                    parentRole = potentialParentRole;
                }
            }

            if (parentRole == null)
            {
                throw new InvalidOperationException("The specified role is not parent to the role you are trying to delete");
            }

            List<Guid> deletedRoleIds = new List<Guid>(new []{existingRole.Id});
            // move all children of the role being deleted to be children of its parent
            // except if the child is a rootRole in which it (and its tree) is deleted
            // if the existing role is a root role it (and its tree) is also deleted

            if (existingRole.IsRoot)
            {
                deletedRoleIds.AddRange(DeleteRolesRecursively(existingRole));
            }
            else
            {
                foreach (Guid childRoleId in existingRole.ChildRoles)
                {
                    Role childRole = this.storage.GetRole(childRoleId);
                    if (childRole.IsRoot)
                    {
                        deletedRoleIds.AddRange(DeleteRolesRecursively(childRole));
                    }
                    else
                    {
                        parentRole.ChildRoles.Add(childRoleId);
                    }
                }

                this.storage.DeleteRole(existingRole);
            }

            parentRole.ChildRoles.Remove(existingRole.Id);

            this.storage.UpdateRole(parentRole);

            DeleteOrphanedDataEntities();
            DeleteOrphanedUsers();
        }

        public IList<RoleDescription> GetMyRoles(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("userId");

            AssertSystemIsInitialized();

            User user = GetAssertUser(userId);

            IList<Role> userRoles = this.storage.GetRolesForUser(user.Id);

            return userRoles.Select(r => GetRolesRecursively(0, user, r)).ToList();
        }

        public IList<RoleClientInfo> GetMyImmediateRoles(Guid userId)
        {
            AssertSystemIsInitialized();

            User user = GetAssertUser(userId);

            IList<Role> userRoles = this.storage.GetRolesForUser(user.Id);

            IList<RoleClientInfo> roles = new List<RoleClientInfo>();

            foreach (Role role in userRoles)
            {
                RoleClientInfo r = new RoleClientInfo();
                r.DataEntities = role.DataEntities;
                r.DataEntityPermission = role.DataEntityPermission;
                r.Id = role.Id;
                r.IsRoot = role.IsRoot;
                r.Name = ReencryptToUser(user, role.Name);

                roles.Add(r);
            }

            return roles;
        }

        public void AssignRoleToUser(Guid userId, Guid roleId, Guid userIdToAssign)
        {
            AssertSystemIsInitialized();

            User user = GetAssertUser(userId);

            Role role = GetAssertRole(roleId);

            if (!role.AssignUnassignRole)
            {
                throw new InvalidOperationException("You cannot assign another user to this role");
            }

            if (!role.IsRoot && !HasAncestorOfRole(user, role.Id))
            {
                throw new InvalidOperationException("You cannot add others to a normal role which you cannot manage");
            }

            if (!role.IsRoot)
            {
                bool assignedUserHasOneOfMyChildRoles = false;
                IList<Role> userRoles = this.storage.GetRolesForUser(userId);
                foreach (Role userRole in userRoles)
                {
                    foreach (Guid childRoleId in userRole.ChildRoles)
                    {
                        if (HasRoleOrChildOfRole(this.storage.GetRole(childRoleId), userIdToAssign))
                        {
                            assignedUserHasOneOfMyChildRoles = true;
                            break;
                        }
                    }
                }

                if (!assignedUserHasOneOfMyChildRoles)
                {
                    throw new InvalidOperationException("You cannot see that user (as far as you know he does not exist)");
                }
            }

            User assignedUser = GetAssertUser(userIdToAssign);

            if (!role.Users.Contains(assignedUser.Id))
            {
                role.Users.Add(assignedUser.Id);
                this.storage.UpdateRole(role);
            }
        }

        public void RemoveRoleFromUser(Guid userId, Guid roleId, Guid assignedUserId)
        {
            AssertSystemIsInitialized();

            User user = GetAssertUser(userId);

            Role role = GetAssertRole(roleId);

            AssertUserHasRoleOrAncestorOfRole(user, role.Id);

            if (!role.AssignUnassignRole)
            {
                throw new InvalidOperationException("User cannot be unassigned from this role");
            }

            if (!role.IsRoot && !HasAncestorOfRole(user, role.Id))
            {
                throw new InvalidOperationException("You cannot remove yourself from a normal role which you cannot manage");
            }

            User assignedUser = GetAssertUser(assignedUserId);

            // if we removed the last user from a root role then delete the role
            if (role.IsRoot && role.Users.Count == 1 && role.Users.Contains(assignedUser.Id))
            {
                DeleteSubRole(userId, Guid.Empty, roleId);
            }
            else
            {
                role.Users.Remove(assignedUser.Id);
                this.storage.UpdateRole(role);

                DeleteOrphanedUsers();
            }
        }





        public void CreateDataEntities(Guid userId, IList<Guid> roleIds, IList<DataEntity> dataEntities)
        {
            if (dataEntities == null) throw new ArgumentNullException("dataEntities");
            if (userId == Guid.Empty) throw new ArgumentException("userId");

            AssertSystemIsInitialized();

            User user = GetAssertUser(userId);

            IList<Role> checkedRoles = new List<Role>();

            foreach (Guid roleId in roleIds)
            {
                Role r = GetAssertRole(roleId);

                AssertUserHasRoleOrAncestorOfRole(user, roleId);

                if (r.DataEntityPermission == DataEntityPermission.ReadContent)
                {
                    throw new InvalidOperationException("You cannot create data using one of the specified roles");
                }

                checkedRoles.Add(r);
            }

            AssertRolesHasSameRoot(checkedRoles);

            foreach (DataEntity dataEntity in dataEntities)
            {
                CheckDataEntity(dataEntity);

                if (!DataSigner.IsSignatureValid(dataEntity, dataEntity.Signature, user.SignPublicKey))
                {
                    throw new InvalidOperationException("A data signature is not valid");
                }
            }

            foreach (DataEntity entity in dataEntities)
            {
                this.storage.CreateDataEntity(userId, entity);
            }

            foreach (Role role in checkedRoles)
            {
                AddDataEntitiesToRoleAndAllParentsRoles(this.storage.GetDataOwnerRole(), role, dataEntities);
            }
        }

        private void AssertRolesHasSameRoot(IList<Role> roles)
        {
            if (roles.Count > 1)
            {
                Guid ancestorRootRole = FindNearestAncestorRootRole(roles[0]);

                foreach (Role role in roles)
                {
                    if (ancestorRootRole != FindNearestAncestorRootRole(role))
                    {
                        throw new InvalidOperationException("All data enctities must have the same root role");
                    }
                }
            }
        }

        private Guid FindNearestAncestorRootRole(Role role)
        {
            if (role.IsRoot)
            {
                return role.Id;
            }

            return FindNearestAncestorRootRole(GetParentOfRole(this.storage.GetDataOwnerRole(), role.Id));
        }

        public void DeleteDataEntities(Guid userId, IList<Guid> roleIds, IList<Guid> dataEntityIds)
        {
            if (userId == Guid.Empty) throw new ArgumentException("userId");

            AssertSystemIsInitialized();

            User user = GetAssertUser(userId);

            IList<Role> checkedRoles = new List<Role>();

            foreach (Guid roleId in roleIds)
            {
                Role r = GetAssertRole(roleId);

                AssertUserHasRoleOrAncestorOfRole(user, roleId);

                if (r.DataEntityPermission == DataEntityPermission.ReadContent)
                {
                    throw new InvalidOperationException("You cannot delete data using one of the specified roles");
                }

                checkedRoles.Add(r);
            }

            DeleteDataEntitiesFromAllRoles(dataEntityIds);

            DeleteOrphanedDataEntities();
        }

        public IList<DataEntity> GetDataEntitiesForRole(Guid userId, Guid roleId)
        {
            AssertSystemIsInitialized();
            User user = GetAssertUser(userId);

            AssertUserHasRoleOrAncestorOfRole(user, roleId);

            Role role = this.storage.GetRole(roleId);

            return role.DataEntities.Select(id => ReencryptDataEntity(this.storage.GetDataEntityFromId(id), user)).ToList();
        }

        public IList<DataEntity> SearchForDataEntities(Guid userId, Guid attributeId)
        {
            AssertSystemIsInitialized();

            User user = GetAssertUser(userId);

            IList<Role> userRoles = this.storage.GetRolesForUser(user.Id);

            ISet<DataEntity> entities = new HashSet<DataEntity>(new DeComparer());

            foreach (Role role in userRoles)
            {
                entities.UnionWith(role.DataEntities.Select(d => this.storage.GetDataEntityFromId(d)));
            }

            return entities.Where(e => e.Attributes.Select(a => a.Id).Contains(attributeId))
                .Select(d => ReencryptDataEntity(d, user)).ToList();
        }

        public byte[] GetPayload(Guid userId, Guid dataEntityId)
        {
            AssertSystemIsInitialized();

            User user = GetAssertUser(userId);

            IList<Role> roles = this.storage.GetRolesForUser(user.Id);

            bool userHasRoleWhichHasDataEntity = roles.Any(role => role.DataEntities.Contains(dataEntityId));
            if (!userHasRoleWhichHasDataEntity)
            {
                throw new InvalidOperationException("You do not have a role which gives access to the requested data entity");
            }

            return this.storage.GetDataEntityPayloadFromId(dataEntityId);
        }

        public bool VerifyIntegrity(Guid userId, Guid dataEntityId)
        {
            AssertSystemIsInitialized();

            User user = GetAssertUser(userId);

            IList<Role> roles = this.storage.GetRolesForUser(user.Id);

            bool userHasRoleWhichHasDataEntity = roles.Any(role => role.DataEntities.Contains(dataEntityId));
            if (!userHasRoleWhichHasDataEntity)
            {
                throw new InvalidOperationException("You do not have a role which gives access to the requested data entity");
            }


            DataEntity dataEntity = this.storage.GetDataEntityFromId(dataEntityId);

            if (dataEntity == null)
            {
                throw new InvalidOperationException("Data entity with specified id was not found");
            }

            Guid authorId = this.storage.GetAuthorOfDataEntity(dataEntityId);

            User authoringUser = this.storage.GetUser(authorId);

            dataEntity.Payload.Content = this.storage.GetDataEntityPayloadFromId(dataEntityId);

            return DataSigner.IsSignatureValid(dataEntity, dataEntity.Signature, authoringUser.SignPublicKey);
        }

        private static IPreService CreatePreProxy()
        {
            ProxyFactory.RegisterProxy(typeof(IPreService), typeof(PreServiceProxy), PreAddress);
            return ProxyFactory.CreateProxy<IPreService>();
        }

        private static void GiveAllPermissionsToRole(Role role)
        {
            role.AssignUnassignRole = true;
            role.CanCreateRoot = true;
            role.CanManageSubRoles = true;
            role.CanCreateUsers = true;
            role.DataEntityPermission = DataEntityPermission.ModifyContent;
        }

        private bool HasRoleOrChildOfRole(Role role, Guid userId)
        {
            if (role.Users.Contains(userId))
            {
                return true;
            }

            foreach (Guid childRoleId in role.ChildRoles)
            {
                Role child = this.storage.GetRole(childRoleId);
                if (HasRoleOrChildOfRole(child, userId))
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasAncestorOfRole(User user, Guid roleId)
        {
            Role doRole = this.storage.GetDataOwnerRole();
            bool userHasTopRole = doRole.Users.Contains(user.Id);

            return HasUserAncestorOfRoleRecursively(user, userHasTopRole, doRole, roleId);
        }

        private bool HasUserAncestorOfRoleRecursively(User user, bool userHasTopRole, Role topRole, Guid roleId)
        {
            if (userHasTopRole && topRole.Id != roleId && topRole.ChildRoles.Contains(roleId))
            {
                return true;
            }

            foreach (Guid childRoleId in topRole.ChildRoles)
            {
                Role childRole = this.storage.GetRole(childRoleId);
                if (HasUserAncestorOfRoleRecursively(user, childRole.Users.Contains(user.Id) || userHasTopRole, childRole, roleId))
                {
                    return true;
                }
            }

            return false;
        }

        private void AssertUserHasRoleOrAncestorOfRole(User user, Guid roleId)
        {
            Role doRole = this.storage.GetDataOwnerRole();
            bool userHasTopRole = doRole.Users.Contains(user.Id);
            if (!HasUserRoleOrAncestorOfRoleRecursively(user, userHasTopRole, doRole, roleId))
            {
                throw new InvalidOperationException("You do not have the specified role or an ancestor of the role");
            }
        }

        private void AddDataEntitiesToRoleAndAllParentsRoles(Role ancestorRole, Role role, IEnumerable<DataEntity> dataEntities)
        {
            role.DataEntities = role.DataEntities.Union(dataEntities.Select(d => d.Id)).ToList();
            this.storage.UpdateRole(role);

            Role parent = GetParentOfRole(ancestorRole, role.Id);

            if (!role.IsRoot) // stop at root roles
            {
                AddDataEntitiesToRoleAndAllParentsRoles(ancestorRole, parent, dataEntities);
            }
        }

        private void DeleteDataEntitiesFromAllRoles(IEnumerable<Guid> dataEntityIds)
        {
            foreach (Role role in this.storage.GetAllRoles())
            {
                role.DataEntities = role.DataEntities.Except(dataEntityIds).ToList();
                this.storage.UpdateRole(role);
            }
        }

        private bool HasUserRoleOrAncestorOfRoleRecursively(User user, bool userHasTopRole, Role topRole, Guid roleId)
        {
            if (userHasTopRole && topRole.Id == roleId)
            {
                return true;
            }

            foreach (Guid childRoleId in topRole.ChildRoles)
            {
                Role childRole = this.storage.GetRole(childRoleId);
                if (HasUserRoleOrAncestorOfRoleRecursively(user, childRole.Users.Contains(user.Id) || userHasTopRole, childRole, roleId))
                {
                    return true;
                }
            }

            return false;
        }

        private static void AssertUserHasRole(User user, Role role)
        {
            if (!role.Users.Contains(user.Id))
            {
                throw new InvalidOperationException("You do not have the specified role");
            }
        }

        private User GetAssertUser(Guid userId, string userTerm = "User")
        {
            User user = this.storage.GetUser(userId);
            if (user == null || user.Name == null)
            {
                throw new InvalidOperationException(userTerm + " does not exist");
            }
            return user;
        }

        private Role GetAssertRole(Guid roleId, string roleTerm = "Role")
        {
            Role role = this.storage.GetRole(roleId);
            if (role == null)
            {
                throw new InvalidOperationException(roleTerm + " does not exist");
            }
            return role;
        }

        private UserDescription ConvertUser(User user, Guid userId)
        {
            User child = this.storage.GetUser(userId);

            UserDescription desc = new UserDescription();
            desc.Id = child.Id;
            desc.Name = ReencryptToUser(user, child.Name);

            return desc;
        }

        private static byte[] ReencryptToUser(User user, byte[] value)
        {
            if (user.DelegationToken != null)   // can be null if user is DO, then just return the original ciphertext
            {
                IPreService preProxy = CreatePreProxy();
                return preProxy.Reencrypt(user.DelegationToken.ToUser, value);
            }

            return value;
        }

        private void RestrictChildrenAccordingToRoleRecursively(Role role, IEnumerable<Guid> childRoleIds)
        {
            foreach (Guid childRoleId in childRoleIds)
            {
                Role child = this.storage.GetRole(childRoleId);

                if (child.IsRoot)   // restrictions do not propagate to root roles
                {
                    continue;
                }

                if (!role.AssignUnassignRole)
                {
                    child.AssignUnassignRole = false;
                }

                if (!role.CanCreateRoot)
                {
                    child.CanCreateRoot = false;
                }
                if (!role.CanManageSubRoles)
                {
                    child.CanManageSubRoles = false;
                }
                if (!role.CanCreateUsers)
                {
                    child.CanCreateUsers = false;
                }
                if (role.DataEntities.Count < child.DataEntities.Count)
                {
                    child.DataEntities = role.DataEntities;
                }
                if (role.DataEntityPermission == DataEntityPermission.ReadContent)
                {
                    child.DataEntityPermission = DataEntityPermission.ReadContent;
                }

                this.storage.UpdateRole(child);

                RestrictChildrenAccordingToRoleRecursively(child, child.ChildRoles);
            }
        }

        private static bool IsRoleProperSubsetOfExisting(Role existingRole, Role role)
        {
            return (existingRole.AssignUnassignRole && !role.AssignUnassignRole) ||
                   (existingRole.CanCreateRoot && !role.CanCreateRoot) ||
                   (existingRole.CanManageSubRoles && !role.CanManageSubRoles) ||
                   (existingRole.CanCreateUsers && !role.CanCreateUsers) ||
                   (existingRole.DataEntityPermission == DataEntityPermission.ModifyContent && role.DataEntityPermission == DataEntityPermission.ReadContent) ||
                   (existingRole.DataEntities.Count < role.DataEntities.Count);
        }

        private IEnumerable<Guid> DeleteRolesRecursively(Role role)
        {
            List<Guid> deletedRoleIds = new List<Guid>();
            foreach (Guid childRoleId in role.ChildRoles)
            {
                Role child = this.storage.GetRole(childRoleId);
                deletedRoleIds.AddRange(DeleteRolesRecursively(child));
            }

            this.storage.DeleteRole(role);

            return deletedRoleIds;
        }

        private RoleDescription GetRolesRecursively(int depth, User user, Role role)
        {
            bool includeUsers = role.IsRoot || depth > 0;
            RoleDescription desc = ConvertRole(user, role, includeUsers);

            foreach (Guid childId in role.ChildRoles)
            {
                Role child = this.storage.GetRole(childId);

                if (child.IsRoot)   // skip root roles as parent roles cannot see and manage them
                {
                    continue;
                }

                desc.ChildRoles.Add(GetRolesRecursively(depth +1, user, child));
            }

            return desc;
        }

        private RoleDescription ConvertRole(User user, Role role, bool includeUsers)
        {
            RoleDescription desc = new RoleDescription();
            desc.AssignUnassignRole = role.AssignUnassignRole;
            desc.CanCreateRoot = role.CanCreateRoot;
            desc.CanManageSubRoles = role.CanManageSubRoles;
            desc.CanCreateUsers = role.CanCreateUsers;
            desc.DataEntityPermission = role.DataEntityPermission;
            desc.Id = role.Id;
            desc.IsRoot = role.IsRoot;

            if (includeUsers)
            {
                foreach (Guid userId in role.Users)
                {
                    desc.Users.Add(ConvertUser(user, userId));
                }
            }

            desc.Name = ReencryptToUser(user, role.Name);

            return desc;
        }

        private void DeleteOrphanedUsers()
        {
            IList<Role> allRoles = this.storage.GetAllRoles();

            ISet<Guid> usersToDelete = new HashSet<Guid>(this.storage.GetAllUsersIds());
            foreach (Role role in allRoles)
            {
                usersToDelete.ExceptWith(role.Users);
            }

            foreach (Guid userToDelete in usersToDelete)
            {
                User u = GetAssertUser(userToDelete);

                u.DelegationToken = null;
                u.Name = null;
                //u.SignPublicKey // do not remove sign public key as the user may still be the author of data entities.

                this.storage.UpdateUser(u);
            }
        }


        private void DeleteOrphanedDataEntities()
        {
            IList<Role> allRoles = this.storage.GetAllRoles();

            ISet<Guid> entitiesToDelete = new HashSet<Guid>(this.storage.GetAllDataEntityIds());
            foreach (Role role in allRoles)
            {
                entitiesToDelete.ExceptWith(role.DataEntities);
            }

            foreach (Guid entityToDelete in entitiesToDelete)
            {
                this.storage.DeleteDataEntity(entityToDelete);
            }
        }

        private Role GetParentOfRole(Role potentialAncestorRole, Guid roleId)
        {
            if (potentialAncestorRole.ChildRoles.Contains(roleId))
            {
                return potentialAncestorRole;
            }

            foreach (Guid childRoleId in potentialAncestorRole.ChildRoles)
            {
                Role r = this.storage.GetRole(childRoleId);
                if (r != null)
                {
                    Role parentRole = GetParentOfRole(r, roleId);
                    if (parentRole != null)
                    {
                        return parentRole;
                    }
                }
            }

            return null;
        }

        private static void AssertRoleIsSubsetOfParent(Role parentRole, Role role)
        {
            if (role.CanCreateRoot && !parentRole.CanCreateRoot)
            {
                throw new InvalidOperationException("You cannot create subrole which is permitted to create root roles, as the parent role does not allow this");
            }

            if (role.CanCreateUsers && !parentRole.CanCreateUsers)
            {
                throw new InvalidOperationException("You cannot create subrole which is permitted to create sub users, as the parent role does not allow this");
            }

            if (role.DataEntityPermission == DataEntityPermission.ModifyContent &&
                parentRole.DataEntityPermission == DataEntityPermission.ReadContent)
            {
                throw new InvalidOperationException("You cannot create subrole which is permitted to modify data entities, as the parent role does not allow this");
            }

            if (!role.DataEntities.IsSubsetOf(parentRole.DataEntities))
            {
                throw new InvalidOperationException("You cannot create a subrole which has access to more data entities than its parent role");
            }
        }

        private static DataEntity ReencryptDataEntity(DataEntity dataEntity, User user)
        {
            DataEntity reencryptedEntity = new DataEntity();

            byte[] reencryptedIV = ReencryptToUser(user, dataEntity.AesInfo.IV);
            byte[] reencryptedKey = ReencryptToUser(user, dataEntity.AesInfo.Key);

            reencryptedEntity.AesInfo = new AesEncryptionInfo(reencryptedKey, reencryptedIV);
            reencryptedEntity.Attributes = dataEntity.Attributes;
            reencryptedEntity.Payload = dataEntity.Payload;
            reencryptedEntity.Payload.Content = null;
            reencryptedEntity.Id = dataEntity.Id;

            return reencryptedEntity;
        }

        private static void CheckDataEntity(DataEntity dataEntity)
        {
            if (dataEntity == null) throw new ArgumentNullException("dataEntity");

            if (dataEntity.Attributes == null) throw new ArgumentNullException("dataEntity");
            if (dataEntity.Attributes.Count == 0) throw new ArgumentException("dataEntity");

            if (dataEntity.AesInfo == null) throw new ArgumentNullException("dataEntity");
            if (dataEntity.AesInfo.Key == null) throw new ArgumentNullException("dataEntity");
            if (dataEntity.AesInfo.Key.Length == 0) throw new ArgumentException("dataEntity");
            if (dataEntity.AesInfo.IV == null) throw new ArgumentNullException("dataEntity");
            if (dataEntity.AesInfo.IV.Length == 0) throw new ArgumentException("dataEntity");

            if (dataEntity.Payload == null) throw new ArgumentNullException("dataEntity");

            if (dataEntity.Payload.Content == null) throw new ArgumentNullException("dataEntity");
            if (dataEntity.Payload.Content.Length == 0) throw new ArgumentException("dataEntity");

            if (dataEntity.Payload.Name == null) throw new ArgumentNullException("dataEntity");
            if (dataEntity.Payload.Name.Length == 0) throw new ArgumentException("dataEntity");

            if (dataEntity.Id == Guid.Empty) throw new ArgumentException("dataEntity");
        }

        private void AssertSystemIsInitialized()
        {
            Guid doId = this.storage.GetDataOwnerUserId();
            if (doId == Guid.Empty)
            {
                throw new InvalidOperationException("System has not been initialized by a DO");
            }
        }

        private class DeComparer : IEqualityComparer<DataEntity>
        {
            public bool Equals(DataEntity x, DataEntity y)
            {
                return x.Id.Equals(y.Id);
            }

            public int GetHashCode(DataEntity obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }

    internal static class LinqExtender
    {
        public static bool IsSubsetOf<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            return !a.Except(b).Any();
        }
    }
}
