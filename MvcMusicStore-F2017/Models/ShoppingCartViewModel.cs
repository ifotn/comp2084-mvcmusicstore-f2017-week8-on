using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMusicStore_F2017.Models
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}