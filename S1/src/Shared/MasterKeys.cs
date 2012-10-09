using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Shared
{
    public class MasterKeys
    {
        [XmlElement]
        public string MasterKeyPublicKey { get; set; }

        [XmlElement]
        public string MasterKeyPrivateKey { get; set; }
    }
}
