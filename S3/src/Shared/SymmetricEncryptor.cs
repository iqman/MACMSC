using System;
using System.IO;
using System.Security.Cryptography;
using Shared.Dto;

namespace Shared
{
    public static class SymmetricEncryptor
    {
        public static AesEncryptionInfo GenerateSymmetricKeyInfo()
        {
            AesManaged aes = new AesManaged();

            aes.GenerateIV();
            aes.GenerateKey();

            return new AesEncryptionInfo(aes.Key, aes.IV);
        }

        public static byte[] Encrypt(byte[] plaintext, AesEncryptionInfo info)
        {
            if (info == null) throw new ArgumentNullException("info");
            if (plaintext == null || plaintext.Length <= 0)
                throw new ArgumentNullException("plaintext");
            if (info.Key == null || info.Key.Length <= 0)
                throw new ArgumentNullException("key");
            if (info.IV == null || info.IV.Length <= 0)
                throw new ArgumentNullException("iv");

            MemoryStream memoryStream;
            AesManaged aesAlg = null;

            try
            {
                // Create the encryption algorithm object with the specified key and IV.
                aesAlg = new AesManaged();
                aesAlg.Key = info.Key;
                aesAlg.IV = info.IV;

                // Create an encryptor to perform the stream transform.
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                memoryStream = new MemoryStream();

                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plaintext, 0, plaintext.Length);
                }
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return memoryStream.ToArray();
        }

        public static byte[] Decrypt(byte[] ciphertext, AesEncryptionInfo info)
        {
            if (info == null) throw new ArgumentNullException("info");
            if (ciphertext == null || ciphertext.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (info.Key == null || info.Key.Length <= 0)
                throw new ArgumentNullException("key");
            if (info.IV == null || info.IV.Length <= 0)
                throw new ArgumentNullException("iv");

            AesManaged aesAlg = null;

            try
            {
                // Create a the encryption algorithm object with the specified key and IV.
                aesAlg = new AesManaged();
                aesAlg.Key = info.Key;
                aesAlg.IV = info.IV;

                // Create a decrytor to perform the stream transform.
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (var memoryStream = new MemoryStream(ciphertext))
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    int len = cryptoStream.Read(ciphertext, 0, ciphertext.Length);
                    return ciphertext.RangeSubset(0, len);
                }
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }
        }

    }
}
