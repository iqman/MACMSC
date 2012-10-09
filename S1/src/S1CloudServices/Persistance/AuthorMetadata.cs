using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace S1CloudServices.Persistance
{
    public class AuthorMetadata
    {
        public Guid AuthorId { get; set; }

        private Collection<EntityId> entityIds;

        [XmlArray]
        public Collection<EntityId> EntityIds
        {
            get { return entityIds; }
            set { entityIds = value; }
        }

        public AuthorMetadata()
        {
            this.entityIds = new Collection<EntityId>();
        }
    }
}