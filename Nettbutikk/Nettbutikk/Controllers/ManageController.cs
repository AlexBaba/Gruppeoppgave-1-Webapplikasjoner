using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Nettbutikk.Models.Bindings;
using Nettbutikk.Infrastructure;

namespace Nettbutikk.Controllers.Account
{
    [Authorize]
    public class ManageController : BaseController
    {
        // GET /manage/index
        public async Task<ActionResult> Index(MessageId? message)
        {
            ViewBag.StatusMessage =
                message == MessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == MessageId.Error ? "An error has occurred."
                : message == MessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == MessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new ManageAccount
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };

            return View(model);
        }
        
        // POST /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            MessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = MessageId.RemoveLoginSuccess;
            }
            else
            {
                message = MessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }
        
        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(MessageId? message)
        {
            ViewBag.StatusMessage =
                message == MessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == MessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager
                .GetLoginsAsync(User.Identity.GetUserId());

            var otherLogins = AuthenticationManager
                .GetExternalAuthenticationTypes()
                .Where(auth =>
                    userLogins.All(ul =>
                        auth.AuthenticationType != ul.LoginProvider))
                .ToList();

            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;

            return View(new ExternalAccounts
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }
        
        // POST /manage/øinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            return new AccountController
                .ChallengeResult(provider,
                    Url.Action("LinkLoginCallback", "Manage"),
                    User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = MessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = MessageId.Error });
        }
        
        // GET /manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        // POST /manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(
            RegisterAccountPhone model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Generate the token and send it
            var code = await UserManager
                .GenerateChangePhoneNumberTokenAsync(
                    User.Identity.GetUserId(), model.Number);

            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };

                await UserManager.SmsService.SendAsync(message);
            }

            return RedirectToAction("VerifyPhoneNumber",
                new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager
                .SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);

            var user = await UserManager
                .FindByIdAsync(User.Identity.GetUserId());

            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false,
                    rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager
                .SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);

            var user = await UserManager
                .FindByIdAsync(User.Identity.GetUserId());

            if (user != null)
            {
                await SignInManager.SignInAsync(user,
                    isPersistent: false, rememberBrowser: false);
            }

            return RedirectToAction("Index", "Manage");
        }

        // GET /manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager
                .GenerateChangePhoneNumberTokenAsync(
                    User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number

            if (phoneNumber == null)
            {
                return View("Error");
            }

            return View(new VerifyAccountPhone { Number = phoneNumber });
        }

        // POST /manage/verifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(
            VerifyAccountPhone model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await UserManager
                .ChangePhoneNumberAsync(User.Identity.GetUserId(),
                    model.Number, model.Code);

            if (result.Succeeded)
            {
                var user = await UserManager
                    .FindByIdAsync(User.Identity.GetUserId());

                if (user != null)
                {
                    await SignInManager
                        .SignInAsync(user, isPersistent: false,
                            rememberBrowser: false);
                }

                return RedirectToAction("Index",
                    new { Message = MessageId.AddPhoneSuccess });
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        // GET /manage/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = MessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = MessageId.RemovePhoneSuccess });
        }

        #region Helpers
        
        public enum MessageId
        {
            SetTwoFactorSuccess,
            RemoveLoginSuccess,
            AddPhoneSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}