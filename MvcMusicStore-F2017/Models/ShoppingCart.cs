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
            // is this album already in this cart?
            var cartItem = db.Carts.SingleOrDefault(a => a.AlbumId == Id
                && a.CartId == ShoppingCartId);

            // if this album is not already in cart
            if (cartItem == null) { 
                cartItem = new Cart
                {
                    AlbumId = Id,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                db.Carts.Add(cartItem);
            }
            else
            {
                // add 1 to the current count of this album
                cartItem.Count++;
            }

            // save to the database
            db.SaveChanges();
        }

        // Get Cart Items
        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(c => c.CartId == ShoppingCartId).ToList();
        }

        // Get Cart Total
        public decimal GetTotal()
        {
            // get the albums in the current cart
            // calculate the total for each (count * price)
            // sum all the line totals together
            decimal? total = (from c in db.Carts
                              where c.CartId == ShoppingCartId
                              select (int?)c.Count * c.Album.Price).Sum();

            return total ?? decimal.Zero;
        }
    }
}