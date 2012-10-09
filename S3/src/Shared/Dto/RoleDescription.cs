using System;
using System.Collections.Generic;

namespace Shared.Dto
{
    public class RoleDescription
    {
        public Guid Id { get; set; }
        public byte[] Name { get; set; }

        public bool AssignUnassignRole { get; set; }

        public bool CanManageSubRoles { get; set; }
        public bool CanCreateRoot { get; set; }

        public bool CanCreateUsers { get; set; }

        public bool IsRoot { get; set; }

        public DataEntityPermission DataEntityPermission { get; set; }

        public IList<UserDescription> Users { get; set; }

        public IList<RoleDescription> ChildRoles { get; set; }

        public RoleDescription()
        {
            ChildRoles = new List<RoleDescription>();
            Users = new List<UserDescription>();
        }
    }
}
