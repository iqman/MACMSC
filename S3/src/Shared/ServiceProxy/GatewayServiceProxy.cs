using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Channels;
using Shared.Dto;
using Shared.ServiceContracts;

namespace Shared.ServiceProxy
{
    /// <summary>
    /// Handles communication with server using WCF to communicate with a webservice.
    /// </summary>
    public class GatewayServiceProxy : ErrorHandlingServiceBase<IGatewayService>, IGatewayService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public GatewayServiceProxy(Binding binding, string webServiceUrl)
            : base(binding, webServiceUrl)
        {
            this.ClientCredentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;
        }

        public void InitializeSystem(Guid dataOwnerId, byte[] dataOwnerName, byte[] rootRoleName, byte[] signPublicKey)
        {
            InvokeWithErrorHandling(() => this.Channel.InitializeSystem(dataOwnerId, dataOwnerName, rootRoleName, signPublicKey));
        }

        public void CreateUser(Guid userId, Guid roleId, User newUser)
        {
            InvokeWithErrorHandling(() => this.Channel.CreateUser(userId, roleId, newUser));
        }

        public void CreateSubRole(Guid userId, Guid roleId, Role newRole)
        {
            InvokeWithErrorHandling(() => this.Channel.CreateSubRole(userId, roleId, newRole));
        }

        public void UpdateSubRole(Guid userId, Guid roleId, Role updatedSubRole)
        {
            InvokeWithErrorHandling(() => this.Channel.UpdateSubRole(userId, roleId, updatedSubRole));
        }

        public void DeleteSubRole(Guid userId, Guid roleId, Guid roleToDeleteId)
        {
            InvokeWithErrorHandling(() => this.Channel.DeleteSubRole(userId, roleId, roleToDeleteId));
        }

        public IList<RoleDescription> GetMyRoles(Guid userId)
        {
            return InvokeWithErrorHandling(() => this.Channel.GetMyRoles(userId));
        }

        public IList<RoleClientInfo> GetMyImmediateRoles(Guid userId)
        {
            return InvokeWithErrorHandling(() => this.Channel.GetMyImmediateRoles(userId));
        }

        public void AssignRoleToUser(Guid userId, Guid roleId, Guid userIdToAssign)
        {
            InvokeWithErrorHandling(() => this.Channel.AssignRoleToUser(userId, roleId, userIdToAssign));
        }

        public void RemoveRoleFromUser(Guid userId, Guid roleId, Guid assignedUserId)
        {
            InvokeWithErrorHandling(() => this.Channel.RemoveRoleFromUser(userId, roleId, assignedUserId));
        }

        public void CreateDataEntities(Guid userId, IList<Guid> roleIds, IList<DataEntity> dataEntities)
        {
            InvokeWithErrorHandling(() => this.Channel.CreateDataEntities(userId, roleIds, dataEntities));
        }

        public void DeleteDataEntities(Guid userId, IList<Guid> roleIds, IList<Guid> dataEntityIds)
        {
            InvokeWithErrorHandling(() => this.Channel.DeleteDataEntities(userId, roleIds, dataEntityIds));
        }

        public IList<DataEntity> GetDataEntitiesForRole(Guid userId, Guid roleId)
        {
            return InvokeWithErrorHandling(() => this.Channel.GetDataEntitiesForRole(userId, roleId));
        }

        public IList<DataEntity> SearchForDataEntities(Guid userId, Guid attributeId)
        {
            return InvokeWithErrorHandling(() => this.Channel.SearchForDataEntities(userId, attributeId));
        }

        public byte[] GetPayload(Guid userId, Guid dataEntityId)
        {
            return InvokeWithErrorHandling(() => this.Channel.GetPayload(userId, dataEntityId));
        }

        public bool VerifyIntegrity(Guid userId, Guid dataEntityId)
        {
            return InvokeWithErrorHandling(() => this.Channel.VerifyIntegrity(userId, dataEntityId));
        }
    }
}
