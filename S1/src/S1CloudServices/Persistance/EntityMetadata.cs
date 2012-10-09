using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace S1CloudServices.Persistance
{
    public class EntityMetadata
    {
        [XmlElement]
        public Guid Id { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public int Size { get; set; }

        private Collection<EntityAttribute> attributes;

        [XmlArray]
        public Collection<EntityAttribute> Attributes
        {
            get { return attributes; }
            set { attributes = value; }
        }

        [XmlElement]
        public string AesKey { get; set; }

        [XmlElement]
        public string AesIV { get; set; }

        public string Signature { get; set; }

        public EntityMetadata()
        {
            this.attributes = new Collection<EntityAttribute>();
        }
    }
}