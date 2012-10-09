using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Shared.Dto
{
    [DataContract]
    public class Signature
    {
        [DataMember]
        public byte[] Value { get; set; }

        public Signature()
        {
        }

        public Signature(byte[] value)
        {
            Value = value;
        }
    }
}
