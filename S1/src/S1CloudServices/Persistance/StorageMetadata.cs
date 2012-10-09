using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace S1CloudServices.Persistance
{
    public class StorageMetadata
    {
        private Collection<EntityMetadata> entities;

        [XmlArray]
        public Collection<EntityMetadata> Entities
        {
            get { return entities; }
            set { entities = value; }
        }

        private Collection<AttributeMetadata> attributes;

        [XmlArray]
        public Collection<AttributeMetadata> Attributes
        {
            get { return attributes; }
            set { attributes = value; }
        }

        private Collection<AuthorMetadata> authors;

        [XmlArray]
        public Collection<AuthorMetadata> Authors
        {
            get { return authors; }
            set { authors = value; }
        }

        public StorageMetadata()
        {
            this.entities = new Collection<EntityMetadata>();
            this.attributes = new Collection<AttributeMetadata>();
            this.authors = new Collection<AuthorMetadata>();
        }
    }
}