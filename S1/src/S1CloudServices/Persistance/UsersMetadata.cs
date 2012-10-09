using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace S1CloudServices.Persistance
{
    public class UsersMetadata
    {
        public Guid DataOwnerId { get; set; }

        private Collection<UserMetadata> userMetadata;

        [XmlArray]
        public Collection<UserMetadata> UserMetadata
        {
            get { return userMetadata; }
            set { userMetadata = value; }
        }

        public UsersMetadata()
        {
            this.userMetadata = new Collection<UserMetadata>();
        }
    }
}