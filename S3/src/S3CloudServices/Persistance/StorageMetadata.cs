using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace S3CloudServices.Persistance
{
    public class StorageMetadata
    {
        [XmlElement]
        public Guid DataOwnerUserId { get; set; }

        [XmlElement]
        public Guid DataOwnerRoleId { get; set; }

        private Collection<UserMetadata> users;

        [XmlArray]
        public Collection<UserMetadata> Users
        {
            get { return users; }
            set { users = value; }
        }

        private Collection<RoleMetadata> roles;

        [XmlArray]
        public Collection<RoleMetadata> Roles
        {
            get { return roles; }
            set { roles = value; }
        }

        private Collection<EntityMetadata> entities;

        [XmlArray]
        public Collection<EntityMetadata> Entities
        {
            get { return entities; }
            set { entities = value; }
        }

        public StorageMetadata()
        {
            this.users = new Collection<UserMetadata>();
            this.Roles = new Collection<RoleMetadata>();
            this.entities = new Collection<EntityMetadata>();
        }
    }
}