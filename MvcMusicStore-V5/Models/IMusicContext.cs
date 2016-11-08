using System.Data.Entity;

namespace MvcMusicStore_V5.Models
{
    public interface IMusicContext
    {
        DbSet<Album> Albums { get; set; }
        DbSet<Artist> Artists { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<Order> Orders { get; set; }

       
    }
}