using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Channels;
using Shared.Dto;
using Shared.ServiceContracts;
using Shared.ServiceProxy;

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

        public void InsertData(Guid userId, DataEntity dataEntity)
        {
            InvokeWithErrorHandling(() => this.Channel.InsertData(userId, dataEntity));
        }

        public IList<DataEntity> FindData(Guid userId, Guid attributeId)
        {
            return InvokeWithErrorHandling(() => this.Channel.FindData(userId, attributeId));
        }

        public byte[] GetPayload(Guid userId, Guid dataEntityId)
        {
            return InvokeWithErrorHandling(() => this.Channel.GetPayload(userId, dataEntityId));
        }

        public void DeleteData(Guid userId, Guid dataEntityId)
        {
            InvokeWithErrorHandling(() => this.Channel.DeleteData(userId, dataEntityId));
        }

        public bool VerifyIntegrity(Guid userId, Guid dataEntityId)
        {
            return InvokeWithErrorHandling(() => this.Channel.VerifyIntegrity(userId, dataEntityId));
        }

        public void InitializeSystem(Guid dataOwnerId)
        {
            InvokeWithErrorHandling(() => this.Channel.InitializeSystem(dataOwnerId));
        }

        public IList<Guid> GetAllUsersWithAccess(Guid dataOwnerId)
        {
            return InvokeWithErrorHandling(() => this.Channel.GetAllUsersWithAccess(dataOwnerId));
        }

        public void RegisterUser(Guid dataOwnerId, Guid userId, DelegationToken token, byte[] signPublicKey)
        {
            InvokeWithErrorHandling(() => this.Channel.RegisterUser(dataOwnerId, userId, token, signPublicKey));
        }

        public void RevokeUser(Guid dataOwnerId, Guid userId)
        {
            InvokeWithErrorHandling(() => this.Channel.RevokeUser(dataOwnerId, userId));
        }
    }
}
