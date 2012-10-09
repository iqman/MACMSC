using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using S1CloudServices;
using StorageGateway;
using Shared.Dto;
using Shared.ServiceContracts;

namespace StorageHoster
{
    [ServiceBehavior(ConcurrencyMode =  ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class GatewayService : IGatewayService
    {
        private readonly Gateway gw;

        public GatewayService()
        {
            this.gw = new Gateway(new UserMetadataStorageImpl(), new DataEntityStorageImpl());
        }

        public void InsertData(Guid userId, DataEntity dataEntity)
        {
            this.gw.Insert(userId, dataEntity);
        }

        public IList<DataEntity> FindData(Guid userId, Guid attributeId)
        {
            return this.gw.Find(userId, attributeId);
        }

        public IList<Guid> GetAllUsersWithAccess(Guid dataOwnerId)
        {
            return this.gw.GetAllUsersWithAccess(dataOwnerId);
        }

        public byte[] GetPayload(Guid userId, Guid dataEntityId)
        {
            return this.gw.GetPayload(userId, dataEntityId);
        }

        public void DeleteData(Guid userId, Guid dataEntityId)
        {
            this.gw.Delete(userId, dataEntityId);
        }

        public bool VerifyIntegrity(Guid userId, Guid dataEntityId)
        {
            return this.gw.VerifyIntegrity(userId, dataEntityId);
        }

        public void InitializeSystem(Guid dataOwnerId)
        {
            this.gw.Initialize(dataOwnerId);
        }

        public void RegisterUser(Guid dataOwnerId, Guid userId, DelegationToken token, byte[] signPublicKey)
        {
            this.gw.RegisterUser(dataOwnerId, userId, token, signPublicKey);
        }

        public void RevokeUser(Guid dataOwnerId, Guid userId)
        {
            this.gw.RevokeUser(dataOwnerId, userId);
        }
    }
}
