using System;
using System.Collections.Generic;
using Shared.Dto;

namespace StorageGateway
{
    public interface IDataStorage
    {
        void InsertDataEntity(Guid userId, DataEntity dataEntity);

        void DeleteDataEntity(Guid dataEntityId);

        byte[] FindDataEntityPayloadFromId(Guid dataEntityId);

        DataEntity FindDataEntityMetadataFromId(Guid dataEntityId);

        Guid FindAuthorOfDataEntity(Guid dataEntityId);

        IList<DataEntity> FindDataEntityMetadataFromAttribute(Guid attributeId);



        void InsertWorkspace(Guid userId, Workspace workspace);

        Workspace GetWorkspaceById(Guid workspaceId);

        void DeleteWorkspace(Guid workspaceId);

        void AddDataEntitiesToWorkspace(IList<Guid> dataEntityIds);

        void RemoveDataEntitiesFromWorkspace(IList<Guid> dataEntityIds);
    }
}
