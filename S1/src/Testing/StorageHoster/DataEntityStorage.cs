//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Shared.Dto;
//using StorageGateway;
//using Attribute = Shared.Dto.Attribute;

//namespace StorageHoster
//{
//    public class DataEntityStorage : IDataEntityStorage
//    {
//        private readonly IDictionary<Guid, DataEntity> dataEntities;
//        private readonly IDictionary<Guid, IList<Guid>> attributeToDataEntityMap;
//        private readonly IDictionary<Guid, Guid> dataEntityToAuthorMap;

//        public DataEntityStorage()
//        {
//            this.dataEntities = new Dictionary<Guid, DataEntity>();
//            this.attributeToDataEntityMap = new Dictionary<Guid, IList<Guid>>();
//            this.dataEntityToAuthorMap = new Dictionary<Guid, Guid>();
//        }

//        public void InsertDataEntity(Guid userId, DataEntity dataEntity)
//        {
//            this.dataEntities.Add(dataEntity.Id, dataEntity);

//            this.dataEntityToAuthorMap.Add(dataEntity.Id, userId);

//            foreach (Attribute attribute in dataEntity.Attributes)
//            {
//                if (!this.attributeToDataEntityMap.ContainsKey(attribute.Id))
//                {
//                    this.attributeToDataEntityMap[attribute.Id] = new List<Guid>();
//                }
//                this.attributeToDataEntityMap[attribute.Id].Add(dataEntity.Id);
//            }
//        }

//        public void DeleteDataEntity(Guid dataEntityId)
//        {
//            this.dataEntities.Remove(dataEntityId);

//            foreach (KeyValuePair<Guid, IList<Guid>> pair in attributeToDataEntityMap)
//            {
//                pair.Value.Remove(dataEntityId);
//            }
//        }

//        public DataEntity FindDataEntityMetadataFromId(Guid dataEntityId)
//        {
//            if (this.dataEntities.ContainsKey(dataEntityId))
//            {
//                return this.dataEntities[dataEntityId];
//            }

//            return null;
//        }

//        public Guid FindAuthorOfDataEntity(Guid dataEntityId)
//        {
//            if (this.dataEntityToAuthorMap.ContainsKey(dataEntityId))
//            {
//                return this.dataEntityToAuthorMap[dataEntityId];
//            }

//            return Guid.Empty;
//        }

//        public IList<DataEntity> FindDataEntityMetadataFromAttribute(Guid attributeId)
//        {
//            List<DataEntity> result = new List<DataEntity>();
//            if (this.attributeToDataEntityMap.ContainsKey(attributeId))
//            {
//                IList<Guid> ids = this.attributeToDataEntityMap[attributeId];

//                result.AddRange(ids.Select(id => this.dataEntities[id]));
//            }

//            return result;
//        }
//    }
//}
