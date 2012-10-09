using System;
using System.Xml.Serialization;

namespace S1CloudServices.Persistance
{
    public class EntityId
    {
        [XmlAttribute]
        public Guid Id { get; set; }

        public EntityId()
        {
        }

        public EntityId(Guid id)
        {
            Id = id;
        }
    }
}