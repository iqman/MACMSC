using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Shared
{
    public static class UserIdentity
    {
        public static Guid GetIdOfCurrentUser()
        {
            return GuidCreator.CreateGuidFromString(Environment.UserName);
        }
    }
}
