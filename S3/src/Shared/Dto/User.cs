using System;
using System.Collections.Generic;

namespace Shared.Dto
{
    public class User
    {
        public Guid Id { get; set; }
        public byte[] Name { get; set; }

        public DelegationToken DelegationToken { get; set; }
        public byte[] SignPublicKey { get; set; }
    }
}
