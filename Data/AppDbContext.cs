using Microsoft.EntityFrameworkCore;
using Kutuphane.Models;

namespace Kutuphane.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Yazar> Yazarlar { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Odunc> Oduncler { get; set; }
    }
}