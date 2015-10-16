using System;
using System.Web;


/*
    cartSize - cookie --> inneholder antall elementer
    product1 - cookie --> 21&3
    product2 - cookie --> ...
    
*/
namespace Nettbutikk.Infrastructure
{
    public class CookieHandler
    {

        public const string CART_SIZE_COOKIE = "cart_size";
        public const string PRODUCT = "product";

        //The default is given in days
        public const int DEFAULT_EXPIRATION = 1;

        public static bool doesCookieExist(string cookieIdentifier)
        {
            return HttpContext.Current.Request.Cookies[cookieIdentifier] != null;
        }

        public static void deleteCookie(HttpCookie cookie)
        {
            cookie.Expires = DateTime.Now.AddDays(-1d);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void deleteCookie(string cookieName)
        {
            deleteCookie(getCookie(cookieName));
        }

        public static void clearOutCart()
        {

            for (int i = 0; i < getCartSize(); i++)
            {
                string current = Convert.ToString(i);
                deleteCookie(PRODUCT + current);
            }

            deleteCookie(CART_SIZE_COOKIE);
        }

        public static int getCartSize()
        {
            var cartCookie = getCookie(CART_SIZE_COOKIE);
            return cartCookie != null ? Convert.ToInt32(cartCookie.Value) : -1;
        }

        public static HttpCookie getCookie(string cookieName)
        {
            return HttpContext.Current.Request.Cookies[cookieName];
        }


        public static bool changeCartElement(string productId, string newQuantity)
        {

            string elementQuantityStr = HttpContext.Current.Request.Cookies[CART_SIZE_COOKIE].Value;

            int elementQuantity = Convert.ToInt32(elementQuantityStr);

            for (int i = 0; i < elementQuantity; i++)
            {
                string current = Convert.ToString(i);
                HttpCookie productCookie = HttpContext.Current.Request.Cookies[PRODUCT + current];

                string[] values = productCookie.Value.Split('&');

                if (values[0] == productId)
                {
                    values[1] = newQuantity;
                    productCookie.Value = createCookieStr(values);
                    HttpContext.Current.Response.Cookies.Add(productCookie);
                    return true;
                }

            }
            return false;

        }


        public static string createCookieStr(string[] values)
        {
            string cookieStr = "";

            for (int i = 0; i < values.Length; i++)
            {
                cookieStr += values[i];
                if (i < (values.Length - 1))
                    cookieStr += "&";
            }

            return cookieStr;
        }

        public static string[] getCookieValues(HttpCookie cookie)
        {

            if (cookie == null)
                return null;

            string[] values = cookie.Value.Split('&');
            return values.Length == 2 ? values : null;
        }

        public static bool removeElementFromCart(string cartId)
        {

            HttpCookie productCookie = HttpContext.Current.Request.Cookies[PRODUCT + cartId];

            if (productCookie == null)
                return false;

            deleteCookie(productCookie);

            HttpCookie cartSizeCookie = HttpContext.Current.Request.Cookies[CART_SIZE_COOKIE];
            cartSizeCookie.Value = Convert.ToString((Convert.ToInt32(cartSizeCookie.Value) - 1));
            HttpContext.Current.Response.Cookies.Add(cartSizeCookie);

            return true;
        }


        public static void addElementToCart(string productId, string productQuantity, int daysUntilExpire = DEFAULT_EXPIRATION)
        {

            HttpCookie cartSizeCookie = HttpContext.Current.Request.Cookies[CART_SIZE_COOKIE];
            string newSize;

            if (cartSizeCookie == null)
            {
                newSize = "1";
                cartSizeCookie = new HttpCookie(CART_SIZE_COOKIE);
                cartSizeCookie.Value = newSize;
                cartSizeCookie.Expires = DateTime.Now.AddDays(DEFAULT_EXPIRATION);
            }
            else
            {
                newSize = Convert.ToString((Convert.ToInt32(cartSizeCookie.Value) + 1));
                cartSizeCookie.Value = newSize;
                cartSizeCookie.Expires = DateTime.Now.AddDays(DEFAULT_EXPIRATION);
            }

            HttpContext.Current.Response.Cookies.Add(cartSizeCookie);

            HttpCookie newProductCookie = new HttpCookie(PRODUCT + newSize);
            String[] values = new string[2];
            values[0] = productId;
            values[1] = productQuantity;
            newProductCookie.Value = createCookieStr(values);
            newProductCookie.Expires = DateTime.Now.AddDays(DEFAULT_EXPIRATION);

            HttpContext.Current.Response.Cookies.Add(newProductCookie);


        }//end of addElementToCart

    }
}