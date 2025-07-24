using Microsoft.EntityFrameworkCore;
using Kutuphane.Data;
using Kutuphane.Models;
using Kutuphane.Repositories.Interfaces;
using static OduncResponseDto;
using System.Linq;
#nullable enable
namespace Kutuphane.Repositories
{
    public class OduncRepository : IOduncRepository
    {
        private readonly AppDbContext _context;

        public OduncRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Odunc>> GetAllAsync()
        {
            return await _context.Oduncler
            .Include(o => o.Kitap)
            .ThenInclude(k => k.Yazar)
            .Include(o => o.Kullanici)
            .ToListAsync();
        }

        public async Task<Odunc?> GetByIdAsync(int id)
        {
            return await _context.Oduncler
            .Include(o => o.Kitap)
            .ThenInclude(k => k.Yazar)
            .Include(o => o.Kullanici)
            .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Odunc> AddAsync(Odunc entity)
        {
            _context.Oduncler.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Odunc entity)
        {
            _context.Oduncler.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var odunc = await GetByIdAsync(id);
            if (odunc != null)
            {
                _context.Oduncler.Remove(odunc);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Oduncler.AnyAsync(y => y.Id == id);
        }

        // Özel metodlar
        public async Task<IEnumerable<Odunc>> GetAktifOdunclerAsync()
        {
            return await _context.Oduncler
            .Include(o => o.Kitap)
                .ThenInclude(k => k.Yazar)
            .Include(o => o.Kullanici)
            .Where(o => o.IadeEdildiMi == false)
            .ToListAsync();
        }

        public async Task<IEnumerable<Odunc>> GetSuresiDolanOdunclerAsync()
        {
            return await _context.Oduncler
            .Include(o => o.Kitap)
                .ThenInclude(k => k.Yazar)
            .Include(o => o.Kullanici)
            .Where(o => o.GeriVerilmesiGerekenTarih < DateTime.Now)
            .Where(o => o.IadeEdildiMi == false)
            .ToListAsync();
        }

        public async Task<IEnumerable<Odunc>> GetKullaniciOdunclerAsync(int kullaniciId)
        {
            return await _context.Oduncler
           .Include(o => o.Kitap)
               .ThenInclude(k => k.Yazar)
           .Include(o => o.Kullanici)
           .Where(o => o.KullaniciId == kullaniciId)
           .ToListAsync();
        }
        public async Task<IEnumerable<Odunc>> GetKitapOduncGecmisiAsync(int kitapId)
        {
            return await _context.Oduncler
           .Include(o => o.Kitap)
               .ThenInclude(k => k.Yazar)
           .Include(o => o.Kullanici)
           .Where(o => o.KitapId == kitapId)
           .ToListAsync();
        }
        public async Task<IEnumerable<BorcRaporuResponseDto>> GetBorcRaporuAsync(
    decimal? minBorç = null,
    decimal? maxBorç = null,
    int? limit = null,
    string? sıralama = null)
{
    var gecikmiOduncler = await _context.Oduncler
        .Where(o => o.IadeEdildiMi == false && o.GeriVerilmesiGerekenTarih < DateTime.Now)
        .Include(o => o.Kullanici)
        .Include(o => o.Kitap)
        .ThenInclude(k => k.Yazar)
        .ToListAsync();

    var query = gecikmiOduncler
        .GroupBy(o => o.KullaniciId)
        .Select(grup => new BorcRaporuResponseDto
        {
            KullaniciId = grup.Key,
            KullaniciAdSoyad = $"{grup.First().Kullanici.Ad} {grup.First().Kullanici.Soyad}",
            Email = grup.First().Kullanici.Email,
            ToplamBorç = grup.Sum(o => o.GecikmeCezasi),
            ToplamGecikmiKitap = grup.Count(),
            GecikmiKitaplar = grup.Select(o => new GecikmisKitapDetayDto
            {
                KitapAd = o.Kitap.Baslik,
                GecikmeGun = o.GecikmeGunSayisi,
                Ceza = o.GecikmeCezasi
            }).ToList()
        });

    if (minBorç.HasValue)
        query = query.Where(x => x.ToplamBorç >= minBorç.Value);
        
    if (maxBorç.HasValue)
        query = query.Where(x => x.ToplamBorç <= maxBorç.Value);

    query = sıralama?.ToLower() switch
    {
        "ad" => query.OrderBy(x => x.KullaniciAdSoyad),
        "gecikme" => query.OrderByDescending(x => x.ToplamGecikmiKitap),
        "artan" => query.OrderBy(x => x.ToplamBorç),
        _ => query.OrderByDescending(x => x.ToplamBorç) 
    };

    if (limit.HasValue)
        query = query.Take(limit.Value);

    return query.ToList();
}
    }
}