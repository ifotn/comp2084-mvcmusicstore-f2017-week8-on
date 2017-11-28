using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMusicStore_F2017.Models
{
    public class ShoppingCart
    {
        // db 
        MusicStoreModel db = new MusicStoreModel();

        // string for unique cart Id
        string ShoppingCartId { get; set; }

        // get current cart contents
        // include httpcontextbase to access the current user info
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();

            // check for an existing Cart Id
            if (context.Session["CartId"] == null)
            {
                // if we have no current cart in the session, check if user is logged in
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session["CartId"] = context.User.Identity.Name;
                }
                else
                {
                    // user is anonymous, generate unique CartId 
                    Guid tempCartId = Guid.NewGuid();
                    context.Session["CartId"] = tempCartId;
                }
            }

            cart.ShoppingCartId = context.Session["CartId"].ToString();
            return cart;
        }

        // Add To Cart
        public void AddToCart(int Id)
        {
            var cartItem = new Cart
            {
                AlbumId = Id,
                CartId = ShoppingCartId,
                Count = 1,
                DateCreated = DateTime.Now
            };

            // save to the database
            db.Carts.Add(cartItem);
            db.SaveChanges();
        }
    }
}