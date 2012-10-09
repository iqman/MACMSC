using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace S1CloudServices.Persistance
{
    public class AttributeMetadata
    {
        public Guid AttributeId { get; set; }

        private Collection<EntityId> entityIds;

        [XmlArray]
        public Collection<EntityId> EntityIds
        {
            get { return entityIds; }
            set { entityIds = value; }
        }

        public AttributeMetadata()
        {
            this.entityIds = new Collection<EntityId>();
        }
    }
}