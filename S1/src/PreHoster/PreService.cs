using System;
using System.ServiceModel;
using ProxyEncryption;
using Shared;
using Shared.Dto;
using Shared.ServiceContracts;

namespace PreHoster
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class PreService : IPreService
    {
        private readonly Libpre pre;
        private DateTime startTime;

        public PreService()
        {
            this.pre = new Libpre(Scheme.Pre1);
            this.startTime = DateTime.UtcNow;
        }

        public void ResetLibPre()
        {
            throw new NotImplementedException();
        }

        public DateTime GetServiceStartTime()
        {
            return this.startTime;
        }

        public KeyPair GenerateKeyPair()
        {
            try
            {
                InMemoryKeyPair pair = this.pre.GenerateKeyPair();

                byte[] pubKey = this.pre.SerializePublicKey(pair.PublicKey);
                byte[] privKey = this.pre.SerializePrivateKey(pair.PrivateKey);

                //this.pre.DeletePrivateKey(pair.PrivateKey);
                //this.pre.DeletePublicKey(pair.PublicKey);

                return new KeyPair(pubKey, privKey);
            }
            catch (Exception e)
            {
                Logger.LogError("Error generating keypair", e);
                throw;
            }
        }

        public byte[] Encrypt(byte[] publicKey, byte[] plaintext)
        {
            try
            {
                IntPtr key = this.pre.DeserializePublicKey(publicKey);
                byte[] result = this.pre.Encrypt(key, plaintext);

                //this.pre.DeletePublicKey(key);

                return result;
            }
            catch (Exception e)
            {
                Logger.LogError("Error encrypting data", e);
                throw;
            }
        }

        public byte[] Decrypt(byte[] privateKey, byte[] ciphertext)
        {
            try
            {
                IntPtr privKey = this.pre.DeserializePrivateKey(privateKey);
                byte[] result = this.pre.Decrypt(privKey, ciphertext);

                //this.pre.DeletePrivateKey(privKey);
                return result;
            }
            catch (Exception e)
            {
                Logger.LogError("Error decrypting data", e);
                throw;
            }
        }

        public byte[] GenerateDelegationKey(byte[] privateKeyForDelegator, byte[] publicKeyForDelegatee)
        {
            try
            {
                IntPtr pubkey = this.pre.DeserializePublicKey(publicKeyForDelegatee);
                IntPtr privKey = this.pre.DeserializePrivateKey(privateKeyForDelegator);

                IntPtr token = this.pre.GenerateDelegationKey(privKey, pubkey);
                byte[] result = this.pre.SerializeDelegationKey(token);
                //this.pre.DeleteDelegationKey(token);
                return result;
            }
            catch (Exception e)
            {
                Logger.LogError("Error generating delegation key", e);
                throw;
            }
        }

        public byte[] Reencrypt(byte[] delegationKey, byte[] cipherText)
        {
            try
            {
                IntPtr delKey = this.pre.DeserializeDelegationKey(delegationKey);
                byte[] result = this.pre.Reencrypt(delKey, cipherText);

                //this.pre.DeleteDelegationKey(delKey);
                return result;
            }
            catch (Exception e)
            {
                Logger.LogError("Error re-encrypting data", e);
                throw;
            }
        }
    }
}
