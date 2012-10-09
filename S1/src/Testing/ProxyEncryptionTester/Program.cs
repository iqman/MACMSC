using System;
using System.IO;
using System.Text;
using ProxyEncryption;

namespace ProxyEncryptionTester
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int testnum = 0;
                Console.WriteLine("C# tester for the proxy encryption library");
                Console.WriteLine();

                Console.Write(++testnum + " Initializing library");

                Libpre l = new Libpre(Scheme.Pre1);

                Console.WriteLine(" ... OK");




                Console.Write(++testnum + " Generating first keypair");

                InMemoryKeyPair keyPair = l.GenerateKeyPair();

                Console.WriteLine(" ... OK");



                Console.Write(++testnum + " Serializing/Deserializing keypair");

                byte[] pubBuf = l.SerializePublicKey(keyPair.PublicKey);
                byte[] privBuf = l.SerializePrivateKey(keyPair.PrivateKey);

                IntPtr publicKey = l.DeserializePublicKey(pubBuf);
                IntPtr privateKey = l.DeserializePrivateKey(privBuf);

                Console.WriteLine(" ... OK");




                Console.Write(++testnum + " Encrypting and decrypting text");

                string originalCleartext = "Hello World";

                byte[] data = Encoding.UTF8.GetBytes(originalCleartext);

                byte[] cipherText = l.Encrypt(publicKey, data);
                byte[] data2 = l.Decrypt(privateKey, cipherText);

                string msg = Encoding.UTF8.GetString(data2);

                if (originalCleartext != msg)
                {
                    throw new Exception("original cleartext and decrypted cleartext were not equal");
                }

                Console.WriteLine(" ... OK");




                Console.Write(++testnum + " Generating second keypair");

                InMemoryKeyPair keyPair2 = l.GenerateKeyPair();

                Console.WriteLine(" ... OK");



                Console.Write(++testnum + " Generating delegation kesy");

                IntPtr delegationKey = l.GenerateDelegationKey(privateKey, keyPair2.PublicKey);
                IntPtr delegationKey1 = l.GenerateDelegationKey(keyPair2.PrivateKey, publicKey);

                Console.WriteLine(" ... OK");



                Console.Write(++testnum + " Serializing/Deserializing delegation key");

                byte[] sdel = l.SerializeDelegationKey(delegationKey);
                IntPtr del2 = l.DeserializeDelegationKey(sdel);

                Console.WriteLine(" ... OK");


                Console.Write(++testnum + " Re-encrypting ciphertext using delegation key");

                byte[] cipherText2 = l.Reencrypt(del2, cipherText);

                Console.WriteLine(" ... OK");



                Console.Write(++testnum + " Trying to decrypt re-encrypted ciphertext using WRONG private key");

                byte[] data3 = l.Decrypt(keyPair.PrivateKey, cipherText2); // wrong private key tested on purpose

                string msgWrong = Encoding.UTF8.GetString(data3);

                if (originalCleartext == msgWrong)
                {
                    throw new Exception("original cleartext and decrypted cleartext using WRONG decryption key were equal");
                }

                Console.WriteLine(" ... OK");



                Console.Write(++testnum + " Decrypting re-encrypted ciphertext using correct private key");

                data3 = l.Decrypt(keyPair2.PrivateKey, cipherText2);
                string msg2 = Encoding.UTF8.GetString(data3);

                if (originalCleartext != msg2)
                {
                    throw new Exception("original cleartext and decrypted (re-encrypted) cleartext were not equal");
                }

                Console.WriteLine(" ... OK");

                Console.Write(++testnum + " Deleting delegation key");

                l.DeleteDelegationKey(delegationKey);
                l.DeleteDelegationKey(del2);

                Console.WriteLine(" ... OK");


                Console.Write(++testnum + " Deleting key pairs");

                l.DeleteKeyPair(keyPair);
                l.DeleteKeyPair(keyPair2);
                l.DeletePublicKey(publicKey);
                l.DeletePrivateKey(privateKey);

                Console.WriteLine(" ... OK");

                Console.WriteLine();
                Console.WriteLine("All tests complete.");

                Console.ReadKey();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: " + e.Message);

                Console.ReadKey();
            }
        }
    }
}
