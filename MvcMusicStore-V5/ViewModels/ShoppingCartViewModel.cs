using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcMusicStore_V5.Models;


namespace MvcMusicStore_V5.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal Total { get; set; }
    }
}