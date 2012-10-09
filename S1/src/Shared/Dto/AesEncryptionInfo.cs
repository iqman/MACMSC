using System.Runtime.Serialization;

namespace Shared.Dto
{
    [DataContract]
    public class AesEncryptionInfo
    {
        public AesEncryptionInfo(byte[] key, byte[] iv)
        {
            Key = key;
            IV = iv;
        }

        [DataMember]
        public byte[] Key { get; set; }

        [DataMember]
        public byte[] IV { get; set; }

        public override int GetHashCode()
        {
            return IV.ComputeContentHash() ^ Key.ComputeContentHash();
        }
    }
}
