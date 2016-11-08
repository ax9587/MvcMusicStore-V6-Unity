using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace MvcMusicStore_V5.Models
{
    public class MusicsRepository : IMusicsRepository
    {
        private MusicContext db = new MusicContext();
        public List<Album> GetAllAlbums()
        {
          return  db.Albums.Include(a => a.Artist).Include(a => a.Genre).ToList();
        }
    }
}