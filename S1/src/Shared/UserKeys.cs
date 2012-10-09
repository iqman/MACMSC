using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Shared
{
    public class UserKeys
    {
        [XmlElement]
        public string MasterKeyPublicKey { get; set; }

        [XmlElement]
        public string UserPrivateKey { get; set; }

        [XmlElement]
        public string UserSignKeys { get; set; }
    }
}
