using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nettbutikk.Infrastructure
{
    public class UserLogin : IdentityUserLogin<Guid> { }
    public class UserRole : IdentityUserRole<Guid> { }
    public class UserClaim : IdentityUserClaim<Guid> { }
    public class Role : IdentityRole<Guid, UserRole> { }
}