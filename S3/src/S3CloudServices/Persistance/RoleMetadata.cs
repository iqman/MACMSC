using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Shared.Dto;

namespace S3CloudServices.Persistance
{
    public class RoleMetadata
    {
        [XmlAttribute]
        public Guid Id { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public bool AssignUnassignRole { get; set; }

        [XmlElement]
        public bool CanManageSubRoles { get; set; }

        [XmlElement]
        public bool CanCreateRoot { get; set; }

        [XmlElement]
        public bool CanCreateUsers { get; set; }

        [XmlElement]
        public bool IsRoot { get; set; }

        [XmlElement]
        public DataEntityPermission DataEntityPermission { get; set; }

        private Collection<ChildId> childRoles;

        [XmlArray]
        public Collection<ChildId> ChildRoles
        {
            get { return childRoles; }
            set { childRoles = value; }
        }

        private Collection<ChildId> users;

        [XmlArray]
        public Collection<ChildId> Users
        {
            get { return users; }
            set { users = value; }
        }

        private Collection<EntityId> dataEntities;

        [XmlArray]
        public Collection<EntityId> DataEntities
        {
            get { return dataEntities; }
            set { dataEntities = value; }
        }

        public RoleMetadata()
        {
            ChildRoles = new Collection<ChildId>();
            Users = new Collection<ChildId>();
            DataEntities = new Collection<EntityId>();
        }
    }
}