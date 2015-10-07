using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Nettbutikk.DAL;
using System.Web;
using System.Web.Mvc;

namespace Nettbutikk.Controllers
{
    [RequireHttps]
    public class BaseController : Controller
    {
        /**
         * A single db-context shared across all controllers.
         * For ease of access.
         */
        private NettbutikkContext _db = new NettbutikkContext();
        private SignInManager _signInManager;
        private UserManager _userManager;
        /**
         * Page title
         */
        private string title = "TankShop";


        // Used for XSRF protection when adding external logins
        protected const string XsrfKey = "${{Nettbutikk-XSRF-Key}}";
        
        public BaseController()
        {
        }

        public BaseController(UserManager userManager, SignInManager signInManager)
            : this()
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }


        protected NettbutikkContext db
        {
            get
            {
                return _db ?? (_db = NettbutikkContext.Create());
            }
            private set
            {
                _db = value;
            }
        }

        protected string PageTitle
        {
            get { return ViewBag.Title; }
            set { ViewBag.Title = title + " / " + value;  }
        }

        protected SignInManager SignInManager
        {
            get
            {
                return _signInManager ?? (_signInManager = HttpContext.GetOwinContext().Get<SignInManager>());
            }
            private set
            {
                _signInManager = value;
            }
        }

        protected UserManager UserManager
        {
            get
            {
                return _userManager ?? (_userManager = HttpContext.GetOwinContext().GetUserManager<UserManager>());
            }
            private set
            {
                _userManager = value;
            }
        }

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }

                if (disposing)
                {
                    _db.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        protected bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        #endregion Helpers
    }
}