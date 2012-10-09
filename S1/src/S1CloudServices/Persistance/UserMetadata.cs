using System;
using System.Xml.Serialization;
using Shared.Dto;

namespace S1CloudServices.Persistance
{
    public class UserMetadata
    {
        [XmlAttribute]
        public Guid UserId { get; set; }

        [XmlElement]
        public string DelegationToken { get; set; }

        [XmlElement]
        public string SignPublicKey { get; set; }

    }
}