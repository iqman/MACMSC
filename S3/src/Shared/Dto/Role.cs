using System;
using System.Collections.Generic;

namespace Shared.Dto
{
    public class Role
    {
        public Guid Id { get; set; }
        public byte[] Name { get; set; }

        public bool CanManageSubRoles { get; set; }
        public bool CanCreateRoot { get; set; }

        public bool CanCreateUsers { get; set; }

        public bool AssignUnassignRole { get; set; }

        public bool IsRoot { get; set; }

        public DataEntityPermission DataEntityPermission { get; set; }

        public IList<Guid> ChildRoles { get; set; }

        public IList<Guid> DataEntities { get; set; }

        public IList<Guid> Users { get; set; }

        public Role()
        {
            ChildRoles = new List<Guid>();
            DataEntities = new List<Guid>();
            Users = new List<Guid>();
        }
    }
}
