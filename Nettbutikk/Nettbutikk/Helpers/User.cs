using System.Security.Principal;

namespace Nettbutikk.Helpers
{
    public class User
    {
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