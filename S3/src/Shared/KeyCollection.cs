using System.Xml.Serialization;

namespace Shared
{
    public class KeyCollection
    {
        [XmlElement]
        public string MasterPublicKey { get; set; }

        [XmlElement]
        public string MasterPrivateKey { get; set; }

        [XmlElement]
        public string PublicKey { get; set; }

        [XmlElement]
        public string PrivateKey { get; set; }

        [XmlElement]
        public string SignKeys { get; set; }
    }
}
