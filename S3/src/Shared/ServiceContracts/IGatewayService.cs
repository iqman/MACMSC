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
        void InitializeSystem(Guid dataOwnerId, byte[] dataOwnerName, byte[] rootRoleName, byte[] signPublicKey);

        [OperationContract]
        void CreateUser(Guid userId, Guid roleId, User newUser);


        [OperationContract]
        void CreateSubRole(Guid userId, Guid roleId, Role newRole);

        [OperationContract]
        void UpdateSubRole(Guid userId, Guid roleId, Role updatedSubRole);

        [OperationContract]
        void DeleteSubRole(Guid userId, Guid roleId, Guid roleToDeleteId);

        [OperationContract]
        IList<RoleDescription> GetMyRoles(Guid userId);

        [OperationContract]
        IList<RoleClientInfo> GetMyImmediateRoles(Guid userId);

        [OperationContract]
        void AssignRoleToUser(Guid userId, Guid roleId, Guid userIdToAssign);

        [OperationContract]
        void RemoveRoleFromUser(Guid userId, Guid roleId, Guid assignedUserId);

        [OperationContract]
        IList<DataEntity> GetDataEntitiesForRole(Guid userId, Guid roleId);

        [OperationContract]
        void CreateDataEntities(Guid userId, IList<Guid> roleIds, IList<DataEntity> dataEntities);

        [OperationContract]
        void DeleteDataEntities(Guid userId, IList<Guid> roleIds, IList<Guid> dataEntityIds);

        [OperationContract]
        IList<DataEntity> SearchForDataEntities(Guid userId, Guid attributeId);

        [OperationContract]
        byte[] GetPayload(Guid userId, Guid dataEntityId);

        [OperationContract]
        bool VerifyIntegrity(Guid userId, Guid dataEntityId);
    }
}
