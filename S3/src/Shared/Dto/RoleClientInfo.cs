using System;
using System.Collections.Generic;

namespace Shared.Dto
{
    public class RoleClientInfo
    {
        public Guid Id { get; set; }
        public byte[] Name { get; set; }

        public bool IsRoot { get; set; }

        public DataEntityPermission DataEntityPermission { get; set; }

        public IList<Guid> DataEntities { get; set; }

        public RoleClientInfo()
        {
            DataEntities = new List<Guid>();
        }
    }
}
