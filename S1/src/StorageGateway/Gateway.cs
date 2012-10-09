using System;
using System.Collections.Generic;
using Shared;
using Shared.Dto;
using Shared.ServiceContracts;
using Shared.ServiceProxy;

namespace StorageGateway
{
    public class Gateway
    {
        private readonly IUserMetadataStorage tokenStorage;
        private readonly IDataEntityStorage dataEntityStorage;

        private const string PreAddress = "http://macmsc.cloudapp.net/PreService.svc";

        public Gateway(IUserMetadataStorage tokenStorage, IDataEntityStorage dataEntityStorage)
        {
            this.tokenStorage = tokenStorage;
            this.dataEntityStorage = dataEntityStorage;
        }

        private static IPreService CreatePreProxy()
        {
            ProxyFactory.RegisterProxy(typeof(IPreService), typeof(PreServiceProxy), PreAddress);
            return ProxyFactory.CreateProxy<IPreService>();
        }

        public void Insert(Guid userId, DataEntity dataEntity)
        {
            if (userId == Guid.Empty) throw new ArgumentException("userId");
            CheckDataEntity(dataEntity);

            GetDelegationKey(userId);   // check that the user has a key (is still valid)

            byte[] signPublicKey = this.tokenStorage.FindSignPublicKey(userId);
            if (signPublicKey == null)
            {
                throw new InvalidOperationException("Cannot insert data as no sign key is registered for the user");
            }

            if (!DataSigner.IsSignatureValid(dataEntity, dataEntity.Signature, signPublicKey))
            {
                throw new InvalidOperationException("The data signature is not valid");
            }

            // TODO: check access rights)

            this.dataEntityStorage.InsertDataEntity(userId, dataEntity);
        }

        public IList<DataEntity> Find(Guid userId, Guid attributeId)
        {
            IList<DataEntity> searchResults = this.dataEntityStorage.FindDataEntityMetadataFromAttribute(attributeId);

            // TODO: check access rights

            for (int i = 0; i < searchResults.Count; i++)
            {
                searchResults[i] = ReencryptDataEntityMetadata(searchResults[i], userId);
            }

            return searchResults;
        }

        public byte[] GetPayload(Guid userId, Guid dataEntityId)
        {
            GetDelegationKey(userId); // to check that the user is still valid and has not been revoked

            return this.dataEntityStorage.FindDataEntityPayloadFromId(dataEntityId);
        }

        public void Delete(Guid userId, Guid dataEntityId)
        {
            GetDelegationKey(userId); // to check that the user is still valid and has not been revoked

            // TODO: check access rights

            this.dataEntityStorage.DeleteDataEntity(dataEntityId);
        }

        public bool VerifyIntegrity(Guid userId, Guid dataEntityId)
        {
            GetDelegationKey(userId); // to check that the user is still valid and has not been revoked

            DataEntity dataEntity = this.dataEntityStorage.FindDataEntityMetadataFromId(dataEntityId);

            if (dataEntity == null)
            {
                throw new InvalidOperationException("Data entity with specified id was not found");
            }

            Guid authorId = this.dataEntityStorage.FindAuthorOfDataEntity(dataEntityId);

            byte[] signPublicKey = this.tokenStorage.FindSignPublicKey(authorId);

            dataEntity.Payload.Content = this.dataEntityStorage.FindDataEntityPayloadFromId(dataEntityId);

            return DataSigner.IsSignatureValid(dataEntity, dataEntity.Signature, signPublicKey);
        }

        private DataEntity ReencryptDataEntityMetadata(DataEntity dataEntity, Guid userId)
        {
            byte[] delegationKey = GetDelegationKey(userId);

            DataEntity reencryptedEntity = new DataEntity();
            IPreService proxy = CreatePreProxy();
            byte[] reencryptedIV = proxy.Reencrypt(delegationKey, dataEntity.AesInfo.IV);

            proxy = CreatePreProxy();
            byte[] reencryptedKey = proxy.Reencrypt(delegationKey, dataEntity.AesInfo.Key);

            reencryptedEntity.AesInfo = new AesEncryptionInfo(reencryptedKey, reencryptedIV);
            reencryptedEntity.Attributes = dataEntity.Attributes;
            reencryptedEntity.Payload = dataEntity.Payload;
            reencryptedEntity.Id = dataEntity.Id;

            return reencryptedEntity;
        }

        private byte[] GetDelegationKey(Guid userId)
        {
            DelegationToken delegationToken = this.tokenStorage.FindDelegationToken(userId);

            if (delegationToken == null)
            {
                throw new InvalidOperationException("No proxy encryption delegation token was found for the user");
            }

            return delegationToken.ToUser;
        }

        private static void CheckDataEntity(DataEntity dataEntity)
        {
            if (dataEntity == null) throw new ArgumentNullException("dataEntity");

            if (dataEntity.Attributes == null) throw new ArgumentNullException("dataEntity");
            if (dataEntity.Attributes.Count == 0) throw new ArgumentException("dataEntity");

            if (dataEntity.AesInfo == null) throw new ArgumentNullException("dataEntity");
            if (dataEntity.AesInfo.Key == null) throw new ArgumentNullException("dataEntity");
            if (dataEntity.AesInfo.Key.Length == 0) throw new ArgumentException("dataEntity");
            if (dataEntity.AesInfo.IV == null) throw new ArgumentNullException("dataEntity");
            if (dataEntity.AesInfo.IV.Length == 0) throw new ArgumentException("dataEntity");

            if (dataEntity.Payload == null) throw new ArgumentNullException("dataEntity");

            if (dataEntity.Payload.Content == null) throw new ArgumentNullException("dataEntity");
            if (dataEntity.Payload.Content.Length == 0) throw new ArgumentException("dataEntity");

            if (dataEntity.Payload.Name == null) throw new ArgumentNullException("dataEntity");
            if (dataEntity.Payload.Name.Length == 0) throw new ArgumentException("dataEntity");

            if (dataEntity.Id == Guid.Empty) throw new ArgumentException("dataEntity");
        }

        public void Initialize(Guid dataOwnerId)
        {
            Guid doId = this.tokenStorage.FindDataOwnerId();

            if (doId != Guid.Empty)
            {
                //throw new InvalidOperationException("Sytem has already been initialized");
            }

            this.tokenStorage.Initialize(dataOwnerId);
        }

        public IList<Guid> GetAllUsersWithAccess(Guid dataOwnerId)
        {
            AssertInitializationAndCorrectDoId(dataOwnerId);

            return this.tokenStorage.FindAllUserWithDelegationTokens();
        }

        public void RegisterUser(Guid dataOwnerId, Guid userId, DelegationToken token, byte[] signPublicKey)
        {
            if (token == null) throw new ArgumentNullException("token");
            if (token.ToUser == null) throw new ArgumentNullException("token");
            if (userId == Guid.Empty) throw new ArgumentException("userId");


            AssertInitializationAndCorrectDoId(dataOwnerId);

            this.tokenStorage.RegisterUser(userId, token, signPublicKey);
        }

        public void RevokeUser(Guid dataOwnerId, Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("userId");


            AssertInitializationAndCorrectDoId(dataOwnerId);

            this.tokenStorage.DeleteDelegationToken(userId);
            // do not remove sign public key as the user may still be the author of dat entities.
        }

        private void AssertInitializationAndCorrectDoId(Guid dataOwnerId)
        {
            Guid doId = this.tokenStorage.FindDataOwnerId();

            if (doId == Guid.Empty)
            {
                throw new InvalidOperationException("System has not been initialized");
            }

            if (doId != dataOwnerId)
            {
                throw new InvalidOperationException("Your supplied DO id does not match the one used when the system was initialized");
            }
        }
    }
}
