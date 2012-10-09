using System;
using System.Collections.Generic;
using Shared.Dto;

namespace StorageGateway
{
    public interface IUserStorage
    {
        void RegisterUser(Guid parentUserId, User user);

        void DeleteUser(Guid userId);

        IList<Guid> FindAllUserIds();

        User FindUser(Guid userId);

        Guid FindDataOwnerId();

        void Initialize(Guid dataOwnerId);
    }
}
