using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Nettbutikk.DAL;
using Nettbutikk.Infrastructure;
using System.Web;
using System.Web.Mvc;

namespace Nettbutikk.Controllers
{
    /**
    * <summary>
    * Base instance for all controllers in this project.
    * Makes managing site-wide solutions such as
    * sessions and authentication much easier.
    * </summary>
    */
    [RequireHttps]
    public class BaseController : Controller
    {
        private NettbutikkContext _db;

        private UserManager _userManager;

        private SignInManager _signInManager;

        /**
         * <summary>
         * A reference to the local database context.
         * </summary>
         */
        protected NettbutikkContext db
        {
            get
            {
                return _db ?? (_db = NettbutikkContext.Create());
            }
        }

        public BaseController()
        {
        }

        public BaseController(UserManager userManager,
            SignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private RoleManager _roleManager;

        protected RoleManager RoleManager
        {
            get
            {
                return _roleManager ?? (_roleManager =
                    Request.GetOwinContext().GetUserManager<RoleManager>());
            }
        }

        protected UserManager UserManager
        {
            get
            {
                return _userManager ?? (_userManager =
                    Request.GetOwinContext().GetUserManager<UserManager>());
            }
        }

        protected SignInManager SignInManager
        {
            get
            {
                return _signInManager ?? (_signInManager =
                    Request.GetOwinContext().GetUserManager<SignInManager>());
            }
        }

#region Helpers

        /**
         * Summary:
         *   A utility helper-method used to redirect to a local URI, or the
         *   index if it is not a local url.
         */
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (RoleManager != null)
                {
                    RoleManager.Dispose();
                    _roleManager = null;
                }

                if (UserManager != null)
                {
                    UserManager.Dispose();
                    _userManager = null;
                }

                if (SignInManager != null)
                {
                    SignInManager.Dispose();
                    _signInManager = null;
                }

            }

            base.Dispose(disposing);
        }

#endregion

    }
}