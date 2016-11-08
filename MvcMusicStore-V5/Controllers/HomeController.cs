using MvcMusicStore_V5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore_V5.Controllers
{
    
    public class HomeController : Controller
    {
        private MusicContext db = new MusicContext();

        private List<Album> GetTopSellingAlbums(int Count)
        {
            return db.Albums
                  .OrderByDescending(a => a.OrderDetails.Count)
                  .Take(Count)
                  .ToList();
        }
        public ActionResult Index()
        {
            var albums = GetTopSellingAlbums(5);
            return View(albums);
        }

       
    }
}