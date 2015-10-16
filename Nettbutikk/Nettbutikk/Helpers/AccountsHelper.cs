using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Nettbutikk.Helpers
{
    public static class AccountsHelper
    {
        private static Dictionary<string, object> htmlAttributes = new Dictionary<string, object>()
        {
            { "data-toggle", "modal" }
        };

        private static object routeData = new
        {
            controller = "Account"
        };

        private static MvcHtmlString Link(this HtmlHelper html, string linkText, string actionName)
        {
            return html.ActionLink(linkText, actionName, routeData, htmlAttributes);
        }

        public static MvcHtmlString LoginLink(this HtmlHelper html)
        {
            return Link(html, "Log in", "Login");
        }

        public static MvcHtmlString LogoutLink(this HtmlHelper html)
        {
            return Link(html, "Log out", "Logout");
        }

        public static MvcHtmlString RegisterLink(this HtmlHelper html)
        {
            return Link(html, "Register", "Register");
        }

        public static MvcHtmlString HomeLink(this HtmlHelper html)
        {
            return html.ActionLink("Manage", "Index", new { controller = "manage"}, htmlAttributes);
        }
    }
}