using MvcMusicStore_V5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore_V5.Controllers
{
    public class StoreController : Controller
    {
        private MusicContext db = new MusicContext();
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Browse(int GenreId)
        {
            var albums = db.Albums.Where(a => a.GenreId == GenreId);
            return View(albums.ToList());
          
        }
        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            
            var genres = db.Genres.ToList();
            return PartialView(genres);
        }
    }
}