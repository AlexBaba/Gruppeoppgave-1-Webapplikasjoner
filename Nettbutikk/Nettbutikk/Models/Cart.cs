using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nettbutikk.Models
{
    public class Cart
    {
        public const string SessionKey = "Cart";

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public virtual ICollection<CartItem> Items { get; set; }
        
        public static Cart GetCart(HttpContextBase context)
        {
            var cart = new Cart();
            cart.Id = cart.GetCartId(context);
            return cart;
        }

        public void Add(Product product, uint amount = 1)
        {
            var item = Items.Where(i => i.Product.Id == product.Id).FirstOrDefault();

            if (null == item)
            {
                Items.Add(new CartItem { Product = product, Amount = amount });
            } else
            {
                item.Amount += amount;
            }
        }

        public void Remove(Product product, uint amount = 0)
        {
            CartItem item = Items.Where(i => i.Product.Id == product.Id).FirstOrDefault();

            if (amount < 1)
                Items.Remove(item);
            else
                item.Amount -= amount;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[SessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[SessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    context.Session[SessionKey] = Guid.NewGuid().ToString();
                }
            }
            return context.Session[SessionKey].ToString();
        }

        public static Cart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
    }
}