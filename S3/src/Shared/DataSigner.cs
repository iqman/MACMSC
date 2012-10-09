using System;
using System.Security.Cryptography;
using Shared.Dto;

namespace Shared
{
    public static class DataSigner
    {
        public static SignKeys GenerateSignKeyPair()
        {
            SignKeys keys = new SignKeys();
            using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
            {
                keys.PublicAndPrivate = dsa.ExportCspBlob(true);
                keys.PublicOnly = dsa.ExportCspBlob(false);
            }

            return keys;
        }

        public static Signature Sign(DataEntity dataEntity, byte[] signKeyPair)
        {
            return new Signature(Sign(BitConverter.GetBytes(dataEntity.GetHashCode()), signKeyPair));
        }

        public static byte[] Sign(byte[] data, byte[] signKeyPair)
        {
            using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
            {
                dsa.ImportCspBlob(signKeyPair);

                if (dsa.PublicOnly)
                {
                    throw new InvalidOperationException("You must have both the public and private key");
                }

                return dsa.SignData(data);
            }
        }

        public static bool IsSignatureValid(DataEntity dataEntity, Signature signature, byte[] signPublicKey)
        {
            return IsSignatureValid(BitConverter.GetBytes(dataEntity.GetHashCode()), signature.Value, signPublicKey);
        }

        public static bool IsSignatureValid(byte[] data, byte[] signature, byte[] signPublicKey)
        {
            using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
            {
                dsa.ImportCspBlob(signPublicKey);

                return dsa.VerifyData(data, signature);
            }
        }
    }
}
