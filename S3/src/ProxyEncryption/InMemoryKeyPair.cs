using System;

namespace ProxyEncryption
{
    public class InMemoryKeyPair
    {
        public IntPtr PublicKey { get; set; }
        public IntPtr PrivateKey { get; set; }

        public InMemoryKeyPair(IntPtr publicKey, IntPtr privateKey)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }
    }
}
