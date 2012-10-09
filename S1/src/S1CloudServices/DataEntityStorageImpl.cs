using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using S1CloudServices.Persistance;
using Shared;
using Shared.Dto;
using StorageGateway;
using Attribute = Shared.Dto.Attribute;

namespace S1CloudServices
{
    public class DataEntityStorageImpl : IDataEntityStorage
    {
        private readonly IDictionary<Guid, DataEntity> dataEntities;
        private readonly IDictionary<Guid, IList<Guid>> attributeToDataEntityMap;
        private readonly IDictionary<Guid, Guid> dataEntityToAuthorMap;

        private StorageMetadata metadata;
        private const string entityDir = "DataEntities";
        private const string filename = "EntityMetadata.xml";
        private readonly string entityMetadataFilepath;
        private readonly string entityDirPath;

        public DataEntityStorageImpl()
        {
            this.entityDirPath = FilePathUtility.GetFullPath(entityDir);
            //this.entityDirPath = Path.Combine(Global.DriveLetter, entityDir);

            this.entityMetadataFilepath = Path.Combine(this.entityDirPath,filename);
        }

        public void InsertDataEntity(Guid userId, DataEntity dataEntity)
        {
            LoadEntityMetadata();

            AuthorMetadata author;
            List<AuthorMetadata> authors = this.metadata.Authors.Where(a => a.AuthorId == userId).ToList();

            if (authors.Count == 1)
            {
                author = authors[0];
            }
            else
            {
                author = new AuthorMetadata();
                this.metadata.Authors.Add(author);
                author.AuthorId = userId;
            }

            author.EntityIds.Add(new EntityId(dataEntity.Id));

            foreach (Attribute attribute in dataEntity.Attributes)
            {
                AttributeMetadata am;
                Attribute modifiedClosureCopy = attribute;
                List<AttributeMetadata> ams = this.metadata.Attributes.Where(a => a.AttributeId == modifiedClosureCopy.Id).ToList();

                if (ams.Count == 1)
                {
                    am = ams[0];
                }
                else
                {
                    am = new AttributeMetadata();
                    this.metadata.Attributes.Add(am);
                    am.AttributeId = attribute.Id;
                }

                am.EntityIds.Add(new EntityId(dataEntity.Id));
            }

            EntityMetadata entity;
            List<EntityMetadata> entities = this.metadata.Entities.Where(e => e.Id == dataEntity.Id).ToList();

            if (entities.Count == 1)
            {
                entity = entities[0];
            }
            else
            {
                entity = new EntityMetadata();
                this.metadata.Entities.Add(entity);
            }

            entity.Id = dataEntity.Id;
            entity.Signature = Convert.ToBase64String(dataEntity.Signature.Value);
            foreach (Attribute attribute in dataEntity.Attributes)
            {
                entity.Attributes.Add(new EntityAttribute(attribute.Id, Convert.ToBase64String(attribute.Keyword)));
            }
            entity.Name = Convert.ToBase64String(dataEntity.Payload.Name);
            entity.Size = dataEntity.Payload.Size;
            entity.AesKey = Convert.ToBase64String(dataEntity.AesInfo.Key);
            entity.AesIV = Convert.ToBase64String(dataEntity.AesInfo.IV);

            SaveEntityMetadata();

            File.WriteAllBytes(Path.Combine(this.entityDirPath, dataEntity.Id.ToString()), dataEntity.Payload.Content);
        }

        public void DeleteDataEntity(Guid dataEntityId)
        {
            LoadEntityMetadata();

            Guid authorId = FindAuthorOfDataEntity(dataEntityId);

            AuthorMetadata author = this.metadata.Authors.First(a => a.AuthorId == authorId);

            for (int i = 0; i < author.EntityIds.Count; i++)
            {
                if (author.EntityIds[i].Id == dataEntityId)
                {
                    author.EntityIds.RemoveAt(i);
                    break;
                }
            }
            if (author.EntityIds.Count == 0)
            {
                this.metadata.Authors.Remove(author);
            }

            EntityMetadata entity = this.metadata.Entities.First(e => e.Id == dataEntityId);

            foreach (EntityAttribute attribute in entity.Attributes)
            {
                AttributeMetadata amd = this.metadata.Attributes.First(a => a.AttributeId == attribute.Id);
                for (int i = 0; i < amd.EntityIds.Count; i++)
                {
                    if (amd.EntityIds[i].Id == dataEntityId)
                    {
                        amd.EntityIds.RemoveAt(i);
                        break;
                    }
                }
                if (amd.EntityIds.Count == 0)
                {
                    this.metadata.Attributes.Remove(amd);
                }
            }

            this.metadata.Entities.Remove(entity);

            string payloadPath = Path.Combine(this.entityDirPath, dataEntityId.ToString());
            File.Delete(payloadPath);

            SaveEntityMetadata();
        }

        public byte[] FindDataEntityPayloadFromId(Guid dataEntityId)
        {
            string payloadPath = Path.Combine(this.entityDirPath, dataEntityId.ToString());
            if (File.Exists(payloadPath))
            {
                return File.ReadAllBytes(payloadPath);
            }

            return null;
        }

        public DataEntity FindDataEntityMetadataFromId(Guid dataEntityId)
        {
            LoadEntityMetadata();

            EntityMetadata md = this.metadata.Entities.First(e => e.Id == dataEntityId);
            return Translate(md);
        }

        public Guid FindAuthorOfDataEntity(Guid dataEntityId)
        {
            LoadEntityMetadata();

            foreach (AuthorMetadata author in this.metadata.Authors)
            {
                if (author.EntityIds.Any(e => e.Id == dataEntityId))
                {
                    return author.AuthorId;
                }
            }

            return Guid.Empty;
        }

        public IList<DataEntity> FindDataEntityMetadataFromAttribute(Guid attributeId)
        {
            LoadEntityMetadata();

            IList<Guid> entityIds = new List<Guid>();

            foreach (AttributeMetadata amd in this.metadata.Attributes.Where(a => a.AttributeId == attributeId))
            {
                entityIds = entityIds.Union(amd.EntityIds.Select(e => e.Id)).ToList();
            }

            IList<DataEntity> finalEntities = new List<DataEntity>();
            foreach (EntityMetadata entity in this.metadata.Entities.Where(e => entityIds.Contains(e.Id)))
            {
                finalEntities.Add(Translate(entity));
            }

            return finalEntities;
        }

        private static DataEntity Translate(EntityMetadata md)
        {
            DataEntity de = new DataEntity();
            de.Id = md.Id;
            de.Signature = new Signature(Convert.FromBase64String(md.Signature));
            de.Payload = new FilePayload(Convert.FromBase64String(md.Name), md.Size);
            de.Attributes = new List<Attribute>();
            foreach (EntityAttribute a in md.Attributes)
            {
                de.Attributes.Add(new Attribute(a.Id, Convert.FromBase64String(a.Keyword)));
            }
            de.AesInfo = new AesEncryptionInfo(Convert.FromBase64String(md.AesKey), Convert.FromBase64String(md.AesIV));

            return de;
        }

        private void LoadEntityMetadata()
        {
            if (this.metadata == null)
            {
                Logger.LogInfo("We have no existing in-memory entity metadata");
                if (File.Exists(this.entityMetadataFilepath))
                {
                    try
                    {
                        this.metadata = XmlFile.ReadFile<StorageMetadata>(this.entityMetadataFilepath);
                        Logger.LogInfo("Succesfully loaded entity metadata from file");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Error loading entity metadata from file", ex);
                    }
                }

                if (this.metadata == null)  // still null. Not loaded from file, or something went wrong when loading from file
                {
                    this.metadata = new StorageMetadata();
                }
            }
        }
        private void SaveEntityMetadata()
        {
            try
            {
                XmlFile.WriteFile(this.metadata, this.entityMetadataFilepath);
                Logger.LogInfo("Succesfully saved entity metadata to file");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error saving entity metadata", ex);
            }
        }
    }
}
