using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Shared.Dto;

namespace Shared.ServiceContracts
{
    [ServiceContract]
    public interface IPreService
    {
        [OperationContract]
        void ResetLibPre();

        [OperationContract]
        DateTime GetServiceStartTime();

        [OperationContract]
        KeyPair GenerateKeyPair();

        [OperationContract]
        byte[] Encrypt(byte[] publicKey, byte[] plaintext);

        [OperationContract]
        byte[] Decrypt(byte[] privateKey, byte[] ciphertext);

        [OperationContract]
        byte[] GenerateDelegationKey(byte[] privateKeyForDelegator, byte[] publicKeyForDelegatee);

        [OperationContract]
        byte[] Reencrypt(byte[] delegationKey, byte[] cipherText);
    }
}
