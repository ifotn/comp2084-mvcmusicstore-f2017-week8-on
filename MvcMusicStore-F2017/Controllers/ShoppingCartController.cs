using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore_F2017.Models;

namespace MvcMusicStore_F2017.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            // get current cart 
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // set up ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            // pass the cart to the Index view
            return View(viewModel);
        }

        // GET: AddToCart/300
        public ActionResult AddToCart(int Id)
        {
            // get the current Cart if any and pass in the user info
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // add the current Album to the cart
            cart.AddToCart(Id);

            // redirect to Index which will display the current cart
            return RedirectToAction("Index");

        }
    }
}