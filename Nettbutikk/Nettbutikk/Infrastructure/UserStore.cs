using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Nettbutikk.Infrastructure;
using Nettbutikk.Models;
using System;
using System.Data.Entity;

namespace Nettbutikk.Infrastructure
{
    class UserStore : UserStore<IdentityUser<Guid, UserLogin, UserRole, UserClaim>, Role, Guid, UserLogin, UserRole, UserClaim>
    {
        public UserStore(DbContext context) : base(context)
        {
        }
    }
}