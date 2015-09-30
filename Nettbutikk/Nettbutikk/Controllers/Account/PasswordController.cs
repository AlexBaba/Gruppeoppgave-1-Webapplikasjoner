using Microsoft.AspNet.Identity;
using Nettbutikk.Models.Bindings;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nettbutikk.Controllers.Account
{
    [Route("password")]
    public class PasswordController : BaseController
    {
        // GET /account/password/change
        public ActionResult Change()
        {
            return View();
        }

        // POST /account/password/change
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Change(
            ChangeAccountPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await UserManager
                .ChangePasswordAsync(User.Identity.GetUserId(),
                    model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = MessageId.ChangePasswordSuccess });
            }

            AddErrors(result);

            return View(model);
        }
        
        // GET: /account/password
        public ActionResult Password()
        {
            return View("manage", "account");
        }
        
        // POST /account/password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Password(SetAccountPassword model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = MessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private enum MessageId
        {
            SetPasswordSuccess,
            ChangePasswordSuccess,
            Error
        }
    }
}