using System;
using System.Net;
using System.ServiceModel.Channels;
using Shared.Dto;
using Shared.ServiceContracts;

namespace Shared.ServiceProxy
{
    /// <summary>
    /// Handles communication with server using WCF to communicate with a webservice.
    /// </summary>
    public class PreServiceProxy : ErrorHandlingServiceBase<IPreService>, IPreService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PreServiceProxy(Binding binding, string webServiceUrl)
            : base(binding, webServiceUrl)
        {
            this.ClientCredentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;
        }

        public void ResetLibPre()
        {
            InvokeWithErrorHandling(() => this.Channel.ResetLibPre());
        }

        public DateTime GetServiceStartTime()
        {
            return InvokeWithErrorHandling(() => this.Channel.GetServiceStartTime());
        }

        public KeyPair GenerateKeyPair()
        {
            return InvokeWithErrorHandling(() => this.Channel.GenerateKeyPair());
        }

        public byte[] Encrypt(byte[] publicKey, byte[] plaintext)
        {
            return InvokeWithErrorHandling(() => this.Channel.Encrypt(publicKey, plaintext));
        }

        public byte[] Decrypt(byte[] privateKey, byte[] ciphertext)
        {
            return InvokeWithErrorHandling(() => this.Channel.Decrypt(privateKey, ciphertext));
        }

        public byte[] GenerateDelegationKey(byte[] privateKeyForDelegator, byte[] publicKeyForDelegatee)
        {
            return InvokeWithErrorHandling(() => this.Channel.GenerateDelegationKey(privateKeyForDelegator, publicKeyForDelegatee));
        }

        public byte[] Reencrypt(byte[] delegationKey, byte[] cipherText)
        {
            return InvokeWithErrorHandling(() => this.Channel.Reencrypt(delegationKey, cipherText));
        }
    }
}
