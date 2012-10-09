using System;
using System.Collections.Generic;
using Shared.Dto;

namespace StorageGateway
{
    public interface IUserMetadataStorage
    {
        void RegisterUser(Guid userId, DelegationToken token, byte[] signPublicKey);

        void DeleteDelegationToken(Guid userId);

        void DeleteSignPublicKey(Guid userId);

        IList<Guid> FindAllUserWithDelegationTokens();

        DelegationToken FindDelegationToken(Guid userId);

        byte[] FindSignPublicKey(Guid userId);

        Guid FindDataOwnerId();

        void Initialize(Guid dataOwnerId);
    }
}
