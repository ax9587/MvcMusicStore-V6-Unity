using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore_V5.Models
{
    public class ShoppingCart
    {
        MusicContext db = new MusicContext();
        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";
        //Singleton pattern
        public static ShoppingCart GetShoppingCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetShoppingCart(Controller controller)
        {
            return GetShoppingCart(controller);
        }

        public static string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey]==null)
            {
                if (!String.IsNullOrEmpty(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempGuid = new Guid();
                    context.Session[CartSessionKey] = tempGuid.ToString();
                }

            }
           
            return context.Session[CartSessionKey].ToString();
        }

        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(cart=>cart.CartId==ShoppingCartId).ToList();
        }

        public void AddToCart(Album album)
        {
            var cartItem = db.Carts.SingleOrDefault(cart => cart.CartId == ShoppingCartId && cart.AlbumId == album.AlbumId);
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    CartId = ShoppingCartId,
                    Count=1,
                    AlbumId=album.AlbumId,
                    DateCreated=DateTime.Now
                };
                db.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            db.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
             int itemCount = 0;
             var cartItem = db.Carts.Single(cart => cart.CartId == ShoppingCartId && cart.RecordId == id);
            if (cartItem != null)
            {
                if (cartItem.Count>1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.Carts.Remove(cartItem);
                   
                }
                db.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(cart => cart.CartId == ShoppingCartId);
            foreach(Cart cartItem in cartItems)
            {
                db.Carts.Remove(cartItem);
            }
            db.SaveChanges();
        }

        public int GetCount()
        {
            int? count=( from cartItems in db.Carts
                       where cartItems.CartId==ShoppingCartId
                       select(int?) cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total= (from cartItems in db.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count * cartItems.Album.Price).Sum();

            return total ?? decimal.Zero;
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = db.Carts.Where(c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            db.SaveChanges();
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };

                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Album.Price);

                db.OrderDetails.Add(orderDetail);

            }

            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            db.SaveChanges();

            // Empty the shopping cart
            EmptyCart();

            // Return the OrderId as the confirmation number
            return order.OrderId;
        }
    }
}