using System;
using System.Runtime.Serialization;

namespace Shared.Dto
{
    [DataContract]
    public class Attribute
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public byte[] Keyword { get; set; }

        public Attribute()
        {
            
        }

        public Attribute(Guid id, byte[] value)
        {
            Id = id;
            Keyword = value;
        }

        public bool Equals(Attribute other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Attribute)) return false;
            return Equals((Attribute) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Attribute left, Attribute right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Attribute left, Attribute right)
        {
            return !Equals(left, right);
        }
    }
}
