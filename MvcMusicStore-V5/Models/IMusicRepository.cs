using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMusicStore_V5.Models
{
    public interface IMusicsRepository
    {
        List<Album> GetAllAlbums();
    }
}
