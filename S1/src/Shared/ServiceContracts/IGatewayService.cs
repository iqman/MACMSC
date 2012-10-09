using System;
using System.Collections.Generic;
using System.ServiceModel;
using Shared.Dto;

namespace Shared.ServiceContracts
{
    [ServiceContract]
    public interface IGatewayService
    {
        [OperationContract]
        void InsertData(Guid userId, DataEntity dataEntity);

        [OperationContract]
        IList<DataEntity> FindData(Guid userId, Guid attributeId);

        [OperationContract]
        byte[] GetPayload(Guid userId, Guid dataEntityId);

        [OperationContract]
        void DeleteData(Guid userId, Guid dataEntityId);

        [OperationContract]
        bool VerifyIntegrity(Guid userId, Guid dataEntityId);

        [OperationContract]
        void InitializeSystem(Guid dataOwnerId);

        [OperationContract]
        IList<Guid> GetAllUsersWithAccess(Guid dataOwnerId);

        [OperationContract]
        void RegisterUser(Guid dataOwnerId, Guid userId, DelegationToken token, byte[] signPublicKey);

        [OperationContract]
        void RevokeUser(Guid dataOwnerId, Guid userId);
    }
}
