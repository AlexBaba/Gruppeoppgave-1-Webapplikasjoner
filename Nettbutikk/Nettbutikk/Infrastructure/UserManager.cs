using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Nettbutikk.Infrastructure.Services;
using Nettbutikk.Models;
using System;
using System.Threading.Tasks;

namespace Nettbutikk.Infrastructure
{
    // Configure the application user manager used in this application.
    // UserManager is defined in ASP.NET Identity and is used by the
    // application.
    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public async Task SendEmailConfirmationAsync(string userId, string callbackUrl, string protocol)
        {
            string code = await GenerateEmailConfirmationTokenAsync(userId);
            
            return SendEmailAsync(userId, "Confirm your account",
                "Please confirm your account by clicking <a href=\"" +
                callbackUrl + "\">here</a>");
        }

        public static UserManager Create(
            IdentityFactoryOptions<UserManager> options,
            IOwinContext context)
        {
            var manager = new UserManager(context.Get<UserStore<User>>());

            // User validation.
            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 12,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application
            // uses Phone and Emails as a step of receiving a code for
            // verifying the user.
            manager.RegisterTwoFactorProvider("Phone Code",
                new PhoneNumberTokenProvider<User>
                {
                    MessageFormat = "Your security code is {0}"
                });
            manager.RegisterTwoFactorProvider("Email Code",
                new EmailTokenProvider<User>
                {
                    Subject = "Nettbutikk: Login Security Code",
                    BodyFormat = "Your security code is {0}"
                });

            manager.EmailService = new UserEmailValidation();
            manager.SmsService = new UserSmsValidation();

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User>(
                        dataProtectionProvider.Create("Nettbutikk"));
            }

            return manager;
        }
    }
}