using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Nettbutikk.Models;
using Nettbutikk.Infrastructure;
using Nettbutikk.Models.Bindings;

namespace Nettbutikk.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        // GET /account/login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        // POST /account/login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AccountLogin model,
            string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var result = await SignInManager.
                PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, shouldLockout: true);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    var routeValues = new {
                        ReturnUrl = returnUrl,
                        RememberMe = model.RememberMe
                    };

                    return RedirectToAction("SendCode", routeValues);

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        
        // GET /account/verifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider,
            string returnUrl, bool rememberMe)
        {
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyAccountCode
                {
                    Provider = provider,
                    ReturnUrl = returnUrl,
                    RememberMe = rememberMe
                });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyAccountCode model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var result = await SignInManager.TwoFactorSignInAsync(
                model.Provider, model.Code, isPersistent:  model.RememberMe,
                rememberBrowser: model.RememberBrowser);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }
        
        // GET /account/register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        
        // POST /account/register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterAccount model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await UserManager.CreateAsync(user,
                    model.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent:false,
                        rememberBrowser:false);

                    // Account confirmation/Password reset:
                    // http://go.microsoft.com/fwlink/?LinkID=320771
                    // That logic should go here

                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
            }

            // Something of the above logic failed. Redisplay.
            return View(model);
        }
        
        // GET /account/confirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId,
            string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var result = await UserManager.ConfirmEmailAsync(userId, code);

            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
        
        // GET /account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        
        // POST /account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(
            ForgotAccountPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null ||
                    !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // By redisplaying this we don't leak any information
                    // about existing user accounts
                    return View("ForgotPasswordConfirmation");
                }

                // Account confirmation/Password reset:
                // http://go.microsoft.com/fwlink/?LinkID=320771
                // Logic should go here
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        
        // GET /account/forgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        
        // GET /account/resetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }
        
        // POST /account/resetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetAccountPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                // Don't leak information about possibly existing users
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            var result = await UserManager.ResetPasswordAsync(user.Id,
                model.Code, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            AddErrors(result);

            return View();
        }
        
        // GET /account/resetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        
        // POST /account/externalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider,
                Url.Action("ExternalLoginCallback",
                    "Account", new { ReturnUrl = returnUrl }));
        }
        
        // GET /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl,
            bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();

            if (userId == null)
            {
                return View("Error");
            }

            var userFactors = await UserManager
                .GetValidTwoFactorProvidersAsync(userId);

            var factorOptions = userFactors
                .Select(purpose => new SelectListItem
                {
                    Text = purpose,
                    Value = purpose
                }).ToList();

            return View(new RegisterAccountSendCode
            {
                Providers = factorOptions,
                ReturnUrl = returnUrl,
                RememberMe = rememberMe
            });
        }
        
        // POST /account/sendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(RegisterAccountSendCode model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(
                    model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode",
                new {
                    Provider = model.SelectedProvider,
                    ReturnUrl = model.ReturnUrl,
                    RememberMe = model.RememberMe
                });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager
                .GetExternalLoginInfoAsync();

            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Check if the user is already signed in with external provider:
            var result = await SignInManager.ExternalSignInAsync(loginInfo,
                isPersistent: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode",
                        new { ReturnUrl = returnUrl, RememberMe = false });

                case SignInStatus.Failure:
                default:
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation",
                        new ExternalAccountLoginConfirmation {
                            Email = loginInfo.Email });
            }
        }

        //
        // POST /account/externalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(
            ExternalAccountLoginConfirmation model,
            string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get user info from external provider
                var info = await AuthenticationManager
                    .GetExternalLoginInfoAsync();

                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }

                var user = new User {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    result = await UserManager
                        .AddLoginAsync(user.Id, info.Login);

                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user,
                            isPersistent: false, rememberBrowser: false);

                        return RedirectToLocal(returnUrl);
                    }
                }

                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;

            return View(model);
        }
        
        // POST /account/logOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }
        
        // GET /account/externalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "${[Nettbutikk-XSRF-Key]}";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider,
                string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties
                {
                    RedirectUri = RedirectUri
                };

                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }

                context.HttpContext
                    .GetOwinContext()
                    .Authentication
                    .Challenge(properties, LoginProvider);
            }
        }
#endregion
    }
}