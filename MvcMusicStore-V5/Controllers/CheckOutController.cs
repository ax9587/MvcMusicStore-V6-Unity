using MvcMusicStore_V5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore_V5.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        MusicContext db = new MusicContext();
        const string PromoCode = "FREE";

        // GET: CheckOut
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                   StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //Save Order
                    db.Orders.Add(order);
                    db.SaveChanges();

                    //Process the order
                    var cart = ShoppingCart.GetShoppingCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                }
               
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

    }
}