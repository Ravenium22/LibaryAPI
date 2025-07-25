using Kutuphane.Data;
using Microsoft.EntityFrameworkCore;
using Kutuphane.Models.DTOs;
using Kutuphane.Models;
using Kutuphane.Repositories.Interfaces;

namespace Kutuphane.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;
        
        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardResponseDto> GetDashboardDataAsync()
        {
            var toplamKitap = await _context.Kitaplar.CountAsync();
            var toplamKullanici = await _context.Kullanicilar.CountAsync();
            var aktifOdunc = await _context.Oduncler.CountAsync(o => o.IadeEdildiMi == false);
            var toplamKategori = await _context.Kategoriler.CountAsync();
            var toplamYazar = await _context.Yazarlar.CountAsync();
            var toplamGecikenIade = await _context.Oduncler
                .CountAsync(o => !o.IadeEdildiMi && o.GeriVerilmesiGerekenTarih < DateTime.Now);
            var toplamOdunc = await _context.Oduncler.CountAsync();
            
            var toplamCezaGeliri = await _context.Oduncler
                .Where(o => !o.IadeEdildiMi && o.GeriVerilmesiGerekenTarih < DateTime.Now)
                .Select(o => EF.Functions.DateDiffDay(o.GeriVerilmesiGerekenTarih, DateTime.Now) * 50)
                .SumAsync();

            var buAyOduncSayisi = await _context.Oduncler
                .CountAsync(o => o.OduncAlinmaTarihi.Month == DateTime.Now.Month &&
                               o.OduncAlinmaTarihi.Year == DateTime.Now.Year);
            var musaitKitapSayisi = await _context.Kitaplar.CountAsync(k => k.MusaitMi == true);

            return new DashboardResponseDto
            {
                ToplamKitapSayisi = toplamKitap,
                ToplamKullaniciSayisi = toplamKullanici,
                AktifOduncSayisi = aktifOdunc,
                ToplamKategoriSayisi = toplamKategori,
                ToplamYazarSayisi = toplamYazar,
                ToplamGecikenIadeSayisi = toplamGecikenIade,
                ToplamOduncSayisi = toplamOdunc,
                ToplamCezaGeliri = toplamCezaGeliri,
                BuAyOduncSayisi = buAyOduncSayisi,
                MusaitKitapSayisi = musaitKitapSayisi,

                PopulerKitaplarinfo = await _context.Oduncler
                    .Include(o => o.Kitap)
                        .ThenInclude(k => k.Yazar)
                    .Include(o => o.Kitap)
                        .ThenInclude(k => k.Kategori)
                    .GroupBy(o => new
                    {
                        o.Kitap.Baslik,
                        o.KitapId
                    })
                    .Select(g => new PopulerKitapInfo
                    {
                        KitapAdi = g.Key.Baslik,
                        OduncSayisi = g.Count(),
                        YazarAdi = $"{g.FirstOrDefault().Kitap.Yazar.Ad} {g.FirstOrDefault().Kitap.Yazar.Soyad}",
                        KategoriAdi = g.FirstOrDefault().Kitap.Kategori.Ad,
                        IsMusait = g.FirstOrDefault().Kitap.MusaitMi,
                        KapakUrl = $"https://covers.openlibrary.org/b/isbn/{g.FirstOrDefault().Kitap.ISBN}-M.jpg" 
                    })
                    .OrderByDescending(p => p.OduncSayisi)
                    .Take(5)
                    .ToListAsync(),

                AylikTrendler = await _context.Oduncler
                    .Include(o => o.Kitap)
                        .ThenInclude(k => k.Kategori)
                    .Where(o => o.OduncAlinmaTarihi >= DateTime.Now.AddMonths(-12))
                    .GroupBy(o => new
                    {
                        o.Kitap.Kategori.Ad,
                        o.OduncAlinmaTarihi.Month,
                        o.OduncAlinmaTarihi.Year
                    })
                    .Select(g => new AylikTrend
                    {
                        KategoriAdi = g.Key.Ad,
                        Ay = $"{g.Key.Year}-{g.Key.Month:00}",
                        OduncSayisi = g.Count()
                    })
                    .OrderByDescending(t => t.OduncSayisi)
                    .ToListAsync()
            };
        }

        public async Task<PublicStatsResponseDto> GetPublicStatsAsync()
        {
            var toplamKitap = await _context.Kitaplar.CountAsync();
            var toplamKategori = await _context.Kategoriler.CountAsync();
            var toplamYazar = await _context.Yazarlar.CountAsync();
            var musaitKitapSayisi = await _context.Kitaplar.CountAsync(k => k.MusaitMi == true);

            var populerKitaplar = await _context.Oduncler
                .Include(o => o.Kitap)
                    .ThenInclude(k => k.Yazar)
                .Include(o => o.Kitap)
                    .ThenInclude(k => k.Kategori)
                .GroupBy(o => new
                {
                    o.Kitap.Baslik,
                    o.KitapId
                })
                .Select(g => new PopulerKitapInfo
            {
                KitapAdi = g.Key.Baslik,
                OduncSayisi = g.Count(),
                YazarAdi = $"{g.FirstOrDefault().Kitap.Yazar.Ad} {g.FirstOrDefault().Kitap.Yazar.Soyad}",
                KategoriAdi = g.FirstOrDefault().Kitap.Kategori.Ad,
                IsMusait = g.FirstOrDefault().Kitap.MusaitMi,
                KapakUrl = $"https://covers.openlibrary.org/b/isbn/{g.FirstOrDefault().Kitap.ISBN}-M.jpg" // ✨ Eksik
            })
                .OrderByDescending(p => p.OduncSayisi)
                .Take(5)
                .ToListAsync();

            return new PublicStatsResponseDto
            {
                ToplamKitapSayisi = toplamKitap,
                ToplamKategoriSayisi = toplamKategori,
                ToplamYazarSayisi = toplamYazar,
                MusaitKitapSayisi = musaitKitapSayisi,
                PopulerKitaplar = populerKitaplar
            };
        }

        public async Task<List<PopulerKitapInfo>> GetPopulerKitaplarAsync(int limit = 10)
        {
            return await _context.Oduncler
                .Include(o => o.Kitap)
                    .ThenInclude(k => k.Yazar)
                .Include(o => o.Kitap)
                    .ThenInclude(k => k.Kategori)
                .GroupBy(o => new
                {
                    o.Kitap.Baslik,
                    o.KitapId
                })
                .Select(g => new PopulerKitapInfo
            {
                KitapAdi = g.Key.Baslik,
                OduncSayisi = g.Count(),
                YazarAdi = $"{g.FirstOrDefault().Kitap.Yazar.Ad} {g.FirstOrDefault().Kitap.Yazar.Soyad}",
                KategoriAdi = g.FirstOrDefault().Kitap.Kategori.Ad,
                IsMusait = g.FirstOrDefault().Kitap.MusaitMi,
                KapakUrl = $"https://covers.openlibrary.org/b/isbn/{g.FirstOrDefault().Kitap.ISBN}-M.jpg" // ✨ Eksik
            })
                .OrderByDescending(p => p.OduncSayisi)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<AylikTrend>> GetAylikTrendlerAsync()
        {
            return await _context.Oduncler
                .Include(o => o.Kitap)
                    .ThenInclude(k => k.Kategori)
                .Where(o => o.OduncAlinmaTarihi >= DateTime.Now.AddMonths(-12))
                .GroupBy(o => new
                {
                    o.Kitap.Kategori.Ad,
                    o.OduncAlinmaTarihi.Month,
                    o.OduncAlinmaTarihi.Year
                })
                .Select(g => new AylikTrend
                {
                    KategoriAdi = g.Key.Ad,
                    Ay = $"{g.Key.Year}-{g.Key.Month:00}",
                    OduncSayisi = g.Count()
                })
                .OrderByDescending(t => t.OduncSayisi)
                .ToListAsync();
        }

        public async Task<AdminFinancialResponseDto> GetAdminFinancialAsync()
        {
            var toplamGecikenIade = await _context.Oduncler
                .CountAsync(o => !o.IadeEdildiMi && o.GeriVerilmesiGerekenTarih < DateTime.Now);
            
            var toplamCezaGeliri = await _context.Oduncler
                .Where(o => !o.IadeEdildiMi && o.GeriVerilmesiGerekenTarih < DateTime.Now)
                .Select(o => EF.Functions.DateDiffDay(o.GeriVerilmesiGerekenTarih, DateTime.Now) * 50)
                .SumAsync();
            
            var buAyOduncSayisi = await _context.Oduncler
                .CountAsync(o => o.OduncAlinmaTarihi.Month == DateTime.Now.Month &&
                               o.OduncAlinmaTarihi.Year == DateTime.Now.Year);
            
            var aktifOdunc = await _context.Oduncler.CountAsync(o => o.IadeEdildiMi == false);
            var toplamOdunc = await _context.Oduncler.CountAsync();

            return new AdminFinancialResponseDto
            {
                ToplamGecikenIadeSayisi = toplamGecikenIade,
                ToplamCezaGeliri = toplamCezaGeliri,
                BuAyOduncSayisi = buAyOduncSayisi,
                AktifOduncSayisi = aktifOdunc,
                ToplamOduncSayisi = toplamOdunc
            };
        }
    }
}