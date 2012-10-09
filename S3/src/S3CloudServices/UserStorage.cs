//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using S3CloudServices.Persistance;
//using Shared;
//using Shared.Dto;
//using StorageGateway;

//namespace S3CloudServices
//{
//    public class UserStorage : IUserStorage
//    {
//        private UsersMetadata metadata;
//        private const string filename = "UserMetadata.xml";
//        private readonly string path;

//        public UserStorage()
//        {
//            this.path = FilePathUtility.GetFullPath(filename);
//        }

//        public void RegisterUser(Guid parentUserId, User user)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteUser(Guid userId)
//        {
//            throw new NotImplementedException();
//        }

//        public IList<Guid> FindAllUserIds()
//        {
//            throw new NotImplementedException();
//        }

//        public User FindUser(Guid userId)
//        {
//            throw new NotImplementedException();
//        }

//        public Guid FindDataOwnerId()
//        {
//            throw new NotImplementedException();
//        }

//        public void Initialize(Guid dataOwnerId)
//        {
//            throw new NotImplementedException();
//        }

//        //public void RegisterUser(Guid userId, DelegationToken token, byte[] signPublicKey)
//        //{
//        //    LoadMetadata();

//        //    UserMetadata md;
//        //    List<UserMetadata> mds = this.metadata.UserMetadata.Where(m => m.UserId == userId).ToList();
//        //    if (mds.Count == 1)
//        //    {
//        //        md = mds[0];
//        //    }
//        //    else
//        //    {
//        //        md = new UserMetadata();
//        //        this.metadata.UserMetadata.Add(md);
//        //    }

//        //    md.UserId = userId;
//        //    md.DelegationToken = Convert.ToBase64String(token.ToUser);
//        //    md.SignPublicKey = Convert.ToBase64String(signPublicKey);

//        //    SaveMetadata();
//        //}

//        //public void DeleteDelegationToken(Guid userId)
//        //{
//        //    LoadMetadata();

//        //    List<UserMetadata> md = this.metadata.UserMetadata.Where(m => m.UserId == userId).ToList();
//        //    if (md.Count == 1)
//        //    {
//        //        md[0].DelegationToken = null;
//        //    }

//        //    SaveMetadata();
//        //}

//        //public void DeleteSignPublicKey(Guid userId)
//        //{
//        //    LoadMetadata();

//        //    List<UserMetadata> md = this.metadata.UserMetadata.Where(m => m.UserId == userId).ToList();
//        //    if (md.Count == 1)
//        //    {
//        //        md[0].SignPublicKey = null;
//        //    }

//        //    SaveMetadata();
//        //}

//        //public IList<Guid> FindAllUserWithDelegationTokens()
//        //{
//        //    LoadMetadata();

//        //    return this.metadata.UserMetadata.Where(u => u.DelegationToken != null).Select(m => m.UserId).ToList();
//        //}

//        //public DelegationToken FindDelegationToken(Guid userId)
//        //{
//        //    LoadMetadata();

//        //    List<UserMetadata> md = this.metadata.UserMetadata.Where(m => m.UserId == userId).ToList();
//        //    if (md.Count == 1)
//        //    {
//        //        return new DelegationToken(Convert.FromBase64String(md[0].DelegationToken));
//        //    }

//        //    return null;
//        //}

//        //public byte[] FindSignPublicKey(Guid userId)
//        //{
//        //    LoadMetadata();

//        //    List<UserMetadata> md = this.metadata.UserMetadata.Where(m => m.UserId == userId).ToList();
//        //    if (md.Count == 1)
//        //    {
//        //        return Convert.FromBase64String(md[0].SignPublicKey);
//        //    }

//        //    return null;
//        //}

//        //public Guid FindDataOwnerId()
//        //{
//        //    LoadMetadata();

//        //    return this.metadata.DataOwnerId;
//        //}

//        //public void Initialize(Guid dataOwnerId)
//        //{
//        //    this.metadata = new UsersMetadata();
//        //    this.metadata.DataOwnerId = dataOwnerId;

//        //    SaveMetadata();
//        //}

//        //private void LoadMetadata()
//        //{
//        //    if (this.metadata == null)
//        //    {
//        //        Logger.LogInfo("We have no existing in-memory metadata");
//        //        if (File.Exists(path))
//        //        {
//        //            try
//        //            {
//        //                this.metadata = XmlFile.ReadFile<UsersMetadata>(path);
//        //                Logger.LogInfo("Succesfully loaded metadata from file");
//        //            }
//        //            catch (Exception ex)
//        //            {
//        //                Logger.LogError("Error loading metadata from file", ex);
//        //            }
//        //        }

//        //        if (this.metadata == null)  // still null. Not loaded from file, or something went wrong when loading from file
//        //        {
//        //            this.metadata = new UsersMetadata();
//        //        }
//        //    }
//        //}
//        //private void SaveMetadata()
//        //{
//        //    try
//        //    {
//        //        XmlFile.WriteFile(this.metadata, path);
//        //        Logger.LogInfo("Succesfully saved metadata to file");
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Logger.LogError("Error saving metadata", ex);
//        //    }
//        //}
//    }
//}
