using System;
using System.Xml.Serialization;

namespace S3CloudServices.Persistance
{
    public class UserMetadata
    {
        [XmlAttribute]
        public Guid Id { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public string DelegationToken { get; set; }

        [XmlElement]
        public string SignPublicKey { get; set; }
    }
}