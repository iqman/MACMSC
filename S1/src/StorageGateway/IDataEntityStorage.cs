using System;
using System.Collections.Generic;
using Shared.Dto;

namespace StorageGateway
{
    public interface IDataEntityStorage
    {
        void InsertDataEntity(Guid userId, DataEntity dataEntity);

        void DeleteDataEntity(Guid dataEntityId);

        byte[] FindDataEntityPayloadFromId(Guid dataEntityId);

        DataEntity FindDataEntityMetadataFromId(Guid dataEntityId);

        Guid FindAuthorOfDataEntity(Guid dataEntityId);

        IList<DataEntity> FindDataEntityMetadataFromAttribute(Guid attributeId);
    }
}
