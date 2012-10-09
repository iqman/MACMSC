using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using S3CloudServices.Persistance;
using Shared;
using Shared.Dto;
using Attribute = Shared.Dto.Attribute;

namespace S3CloudServices
{
    public class Storage
    {
        private StorageMetadata metadata;
        private const string entityDir = "DataStorage";
        private const string filename = "Metadata.xml";
        private readonly string metadataFilepath;
        private readonly string entityDirPath;

        public Storage()
        {
            this.entityDirPath = FilePathUtility.GetFullPath(entityDir);

            this.metadataFilepath = Path.Combine(this.entityDirPath, filename);
        }

        public Guid GetDataOwnerUserId()
        {
            LoadMetadata();

            return this.metadata.DataOwnerUserId;
        }

        public Role GetDataOwnerRole()
        {
            LoadMetadata();

            return GetRole(this.metadata.DataOwnerRoleId);
        }

        public void InitializeSystem(User dataOwner, Role dataOwnerRootRole)
        {
            this.metadata = new StorageMetadata();
            this.metadata.DataOwnerUserId = dataOwner.Id;
            this.metadata.DataOwnerRoleId = dataOwnerRootRole.Id;

            CreateUser(dataOwner);
            CreateRole(dataOwnerRootRole);

            SaveMetadata();
        }

        public User GetUser(Guid userId)
        {
            LoadMetadata();

            List<UserMetadata> mds = this.metadata.Users.Where(m => m.Id == userId).ToList();
            if (mds.Count == 1)
            {
                return TranslateFromMetadata(mds[0]);
            }

            return null;
        }

        public void CreateUser(User user)
        {
            LoadMetadata();

            UserMetadata md;
            List<UserMetadata> mds = this.metadata.Users.Where(m => m.Id == user.Id).ToList();
            if (mds.Count == 1)
            {
                md = mds[0];
            }
            else
            {
                md = new UserMetadata();
                this.metadata.Users.Add(md);
            }

            TranslateToMetadata(md, user);

            SaveMetadata();
        }

        public void UpdateUser(User user)
        {
            LoadMetadata();

            List<UserMetadata> mds = this.metadata.Users.Where(m => m.Id == user.Id).ToList();
            if (mds.Count == 1)
            {
                TranslateToMetadata(mds[0], user);
                SaveMetadata();
            }
        }

        public Role GetRole(Guid roleId)
        {
            LoadMetadata();

            List<RoleMetadata> roles = this.metadata.Roles.Where(r => r.Id == roleId).ToList();
            if (roles.Count == 1)
            {
                return TranslateFromMetadata(roles[0]);
            }

            return null;
        }


        public IList<Role> GetAllRoles()
        {
            LoadMetadata();

            return this.metadata.Roles.Select(r => TranslateFromMetadata(r)).ToList();
        }

        public IList<Guid> GetAllDataEntityIds()
        {
            LoadMetadata();
            return this.metadata.Entities.Select(e => e.Id).ToList();
        }

        public IList<Guid> GetAllUsersIds()
        {
            LoadMetadata();
            return this.metadata.Users.Select(u => u.Id).ToList();
        }


        public IList<Role> GetRolesForUser(Guid userId)
        {
            LoadMetadata();

            return this.metadata.Roles.Where(r => r.Users.Select(uid => uid.Id).Contains(userId))
                .Select(r => TranslateFromMetadata(r)).ToList();
        }

        public void CreateRole(Role role)
        {
            LoadMetadata();

            RoleMetadata md;
            List<RoleMetadata> mds = this.metadata.Roles.Where(m => m.Id == role.Id).ToList();
            if (mds.Count == 1)
            {
                md = mds[0];
            }
            else
            {
                md = new RoleMetadata();
                this.metadata.Roles.Add(md);
            }

            TranslateToMetadata(md, role);

            SaveMetadata();
        }

        public void UpdateRole(Role role)
        {
            LoadMetadata();

            List<RoleMetadata> mds = this.metadata.Roles.Where(m => m.Id == role.Id).ToList();
            if (mds.Count == 1)
            {
                TranslateToMetadata(mds[0], role);
                SaveMetadata();
            }
        }

        public void DeleteRole(Role role)
        {
            LoadMetadata();

            List<RoleMetadata> mds = this.metadata.Roles.Where(m => m.Id == role.Id).ToList();
            if (mds.Count == 1)
            {
                this.metadata.Roles.Remove(mds[0]);
                SaveMetadata();
            }
        }


        private static byte[] ConvertBinary(string data)
        {
            return Convert.FromBase64String(data);
        }

        private static string ConvertBinary(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        private static User TranslateFromMetadata(UserMetadata md)
        {
            User u = new User();
            u.DelegationToken = md.DelegationToken != null ? new DelegationToken(ConvertBinary(md.DelegationToken)) : null;
            u.Id = md.Id;
            u.Name = md.Name != null ? ConvertBinary(md.Name) : null;
            u.SignPublicKey = ConvertBinary(md.SignPublicKey);

            return u;
        }

        private static Role TranslateFromMetadata(RoleMetadata rm)
        {
            Role r = new Role();

            r.AssignUnassignRole = rm.AssignUnassignRole;
            r.CanCreateRoot = rm.CanCreateRoot;
            r.CanManageSubRoles = rm.CanManageSubRoles;
            r.CanCreateUsers = rm.CanCreateUsers;
            r.ChildRoles = rm.ChildRoles.Select(c => c.Id).ToList();
            r.DataEntities = rm.DataEntities.Select(d => d.Id).ToList();
            r.DataEntityPermission = rm.DataEntityPermission;
            r.Id = rm.Id;
            r.IsRoot = rm.IsRoot;
            r.Name = ConvertBinary(rm.Name);
            r.Users = rm.Users.Select(u => u.Id).ToList();

            return r;
        }

        private static DataEntity TranslateFromMetadata(EntityMetadata em)
        {
            DataEntity e = new DataEntity();
            e.AesInfo = new AesEncryptionInfo(ConvertBinary(em.AesKey), ConvertBinary(em.AesIV));
            e.Attributes = new List<Attribute>(em.Attributes.Select(a => new Attribute(a.Id, ConvertBinary(a.Keyword))));
            e.Id = em.Id;
            e.Payload = new FilePayload(ConvertBinary(em.Name), em.Size);
            e.Signature = new Signature(ConvertBinary(em.Signature));
            return e;
        }

        private static void TranslateToMetadata(UserMetadata md, User user)
        {
            // can be null if user is DO or when deleting user
            md.DelegationToken = user.DelegationToken != null ? ConvertBinary(user.DelegationToken.ToUser) : null;
            md.Id = user.Id;
            md.Name = user.Name != null ? ConvertBinary(user.Name) : null;  // can be null when deleting user by clearing his name
            md.SignPublicKey = ConvertBinary(user.SignPublicKey);
        }

        private static void TranslateToMetadata(RoleMetadata rm, Role role)
        {
            rm.AssignUnassignRole = role.AssignUnassignRole;
            rm.CanCreateRoot = role.CanCreateRoot;
            rm.CanManageSubRoles = role.CanManageSubRoles;
            rm.CanCreateUsers = role.CanCreateUsers;
            rm.ChildRoles = new Collection<ChildId>(role.ChildRoles.Select(id => new ChildId(id)).ToList());
            rm.DataEntities = new Collection<EntityId>(role.DataEntities.Select(id => new EntityId(id)).ToList());
            rm.DataEntityPermission = role.DataEntityPermission;
            rm.Id = role.Id;
            rm.IsRoot = role.IsRoot;
            rm.Name = ConvertBinary(role.Name);
            rm.Users = new Collection<ChildId>(role.Users.Select(id => new ChildId(id)).ToList());
        }

        private static void TranslateToMetadata(Guid authorId, EntityMetadata rm, DataEntity e)
        {
            rm.AesIV = ConvertBinary(e.AesInfo.IV);
            rm.AesKey = ConvertBinary(e.AesInfo.Key);
            rm.Attributes = new Collection<EntityAttribute>(e.Attributes.Select(a => new EntityAttribute(a.Id, ConvertBinary(a.Keyword))).ToList());
            rm.AuthorId = authorId;
            rm.Id = e.Id;
            rm.Name = ConvertBinary(e.Payload.Name);
            rm.Signature = ConvertBinary(e.Signature.Value);
            rm.Size = e.Payload.Size;
        }

        private void LoadMetadata()
        {
            if (this.metadata == null)
            {
                Logger.LogInfo("We have no existing in-memory metadata");
                if (File.Exists(this.metadataFilepath))
                {
                    try
                    {
                        this.metadata = XmlFile.ReadFile<StorageMetadata>(this.metadataFilepath);
                        Logger.LogInfo("Succesfully loaded metadata from file");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Error loading metadata from file", ex);
                    }
                }

                if (this.metadata == null)  // still null. Not loaded from file, or something went wrong when loading from file
                {
                    this.metadata = new StorageMetadata();
                }
            }
        }
        private void SaveMetadata()
        {
            try
            {
                XmlFile.WriteFile(this.metadata, this.metadataFilepath);
                Logger.LogInfo("Succesfully saved metadata to file");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error saving metadata", ex);
            }
        }








        public void CreateDataEntity(Guid userId, DataEntity dataEntity)
        {
            LoadMetadata();

            EntityMetadata em;
            List<EntityMetadata> ems = this.metadata.Entities.Where(e => e.Id == dataEntity.Id).ToList();
            if (ems.Count == 1)
            {
                em = ems[0];
            }
            else
            {
                em = new EntityMetadata();
                this.metadata.Entities.Add(em);
            }

            TranslateToMetadata(userId, em, dataEntity);

            SaveMetadata();

            File.WriteAllBytes(Path.Combine(this.entityDirPath, dataEntity.Id.ToString()), dataEntity.Payload.Content);
        }

        public void DeleteDataEntity(Guid dataEntityId)
        {
            LoadMetadata();

            List<EntityMetadata> ems = this.metadata.Entities.Where(e => e.Id == dataEntityId).ToList();
            if (ems.Count == 1)
            {
                this.metadata.Entities.Remove(ems[0]);
                SaveMetadata();

                string payloadPath = Path.Combine(this.entityDirPath, dataEntityId.ToString());
                File.Delete(payloadPath);
            }
        }

        public Guid GetAuthorOfDataEntity(Guid dataEntityId)
        {
            LoadMetadata();

            List<EntityMetadata> ems = this.metadata.Entities.Where(e => e.Id == dataEntityId).ToList();
            if (ems.Count == 1)
            {
                return ems[0].AuthorId;
            }

            return Guid.Empty;
        }

        public byte[] GetDataEntityPayloadFromId(Guid dataEntityId)
        {
            string payloadPath = Path.Combine(this.entityDirPath, dataEntityId.ToString());
            if (File.Exists(payloadPath))
            {
                return File.ReadAllBytes(payloadPath);
            }

            return null;
        }

        public DataEntity GetDataEntityFromId(Guid dataEntityId)
        {
            LoadMetadata();

            List<EntityMetadata> ems = this.metadata.Entities.Where(e => e.Id == dataEntityId).ToList();
            if (ems.Count == 1)
            {
                return TranslateFromMetadata(ems[0]);
            }

            return null;
        }
    }
}
