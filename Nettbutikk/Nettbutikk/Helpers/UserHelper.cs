using Nettbutikk.Models;
using System.Security.Principal;

namespace Nettbutikk.Helpers
{
    public class UserHelper
    {
        public static User CurrentUser
        {
            get;
            set;
        }

        public static bool CanAccess(IPrincipal user, string[] acceptedRoles)
        {
            if (null != user && user.Identity.IsAuthenticated)
            {
                foreach (var role in acceptedRoles)
                {
                    if(user.IsInRole(role))
                        return true;
                }
            }
            return false;
        }
    }
}