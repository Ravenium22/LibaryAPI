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
        public DbSet<Odunc> Oduncler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Kitap>()
                .HasIndex(k => k.ISBN)
                .HasDatabaseName("IX_Kitap_ISBN");

            modelBuilder.Entity<Kitap>()
                .HasIndex(k => k.Baslik)
                .HasDatabaseName("IX_Kitap_Baslik");

            modelBuilder.Entity<Kitap>()
                .HasIndex(k => k.MusaitMi)
                .HasDatabaseName("IX_Kitap_MusaitMi");

            modelBuilder.Entity<Kullanici>()
                .HasIndex(k => k.Email)
                .IsUnique()
                .HasDatabaseName("IX_Kullanici_Email");

            modelBuilder.Entity<Odunc>()
                .HasIndex(o => o.IadeEdildiMi)
                .HasDatabaseName("IX_Odunc_IadeEdildiMi");

            modelBuilder.Entity<Odunc>()
                .HasIndex(o => o.KullaniciId)
                .HasDatabaseName("IX_Odunc_KullaniciId");

            modelBuilder.Entity<Odunc>()
                .HasIndex(o => o.KitapId)
                .HasDatabaseName("IX_Odunc_KitapId");

            modelBuilder.Entity<Odunc>()
                .HasIndex(o => new { o.IadeEdildiMi, o.GeriVerilmesiGerekenTarih })
                .HasDatabaseName("IX_Odunc_IadeEdildiMi_GeriVerilmeTarihi");

            base.OnModelCreating(modelBuilder);
        }
    }
}