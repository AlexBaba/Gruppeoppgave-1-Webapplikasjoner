﻿using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Nettbutikk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nettbutikk.Infrastructure
{

    public class SignInManager : SignInManager<User, string>
    {
        public SignInManager(UserManager userManager,
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UserManager) UserManager);
        }

        public static SignInManager Create(
            IdentityFactoryOptions<SignInManager> options,
            IOwinContext context)
        {
            return new SignInManager(
                context.GetUserManager<UserManager>(),
                context.Authentication);
        }
    }
}