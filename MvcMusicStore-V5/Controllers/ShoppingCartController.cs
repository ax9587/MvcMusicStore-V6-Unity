using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore_V5.Models;
using MvcMusicStore_V5.ViewModels;

namespace MvcMusicStore_V5.Controllers
{
    public class ShoppingCartController : Controller
    {
        MusicContext db = new MusicContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetShoppingCart(this.HttpContext);
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                Total = cart.GetTotal()
                
            };
            
            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            var cart = ShoppingCart.GetShoppingCart(this.HttpContext);
            var album = db.Albums.Single(al => al.AlbumId == id);
            cart.AddToCart(album);
            return RedirectToAction("Index");
            //Testing redirect route
            //var result = controller.Details(-1) as RedirectToRouteResult;
            //Assert.AreEqual("Index", result.RouteValues["action"]);
            //Assert.AreEqual("SomeElse", result.RouteValues["controller"])
        }
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetShoppingCart(this.HttpContext);
            var cartItem= db.Carts.Single(c => c.RecordId == id);
            var AlbumTitle = cartItem.Album.Title;
            var itemCount=cart.RemoveFromCart(id);

            var result = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode( AlbumTitle) +" has been removed.",
                CartCount= cart.GetCount(),
                CartTotal=cart.GetTotal(),
                ItemCount=itemCount,
                DeleteId=id


            };

            return Json(result);
        }

        [ChildActionOnly]
        public ActionResult GetSummary()
        {
            var cart = ShoppingCart.GetShoppingCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("_CartSummary");
        }


    }
}