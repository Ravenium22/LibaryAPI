using Microsoft.EntityFrameworkCore;
using Kutuphane.Data;
using Kutuphane.Models;

namespace Kutuphane.Services
{
    public class SeedDataService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SeedDataService> _logger;

        public SeedDataService(AppDbContext context, ILogger<SeedDataService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            _logger.LogInformation("=== SEED İŞLEMİ BAŞLADI (Database Tamamen Temizleniyor) ===");
            
            _context.Oduncler.RemoveRange(_context.Oduncler);
            _context.Kitaplar.RemoveRange(_context.Kitaplar);
            _context.Kullanicilar.RemoveRange(_context.Kullanicilar);
            _context.Yazarlar.RemoveRange(_context.Yazarlar);
            _context.Kategoriler.RemoveRange(_context.Kategoriler);
            
            await _context.SaveChangesAsync();
            _logger.LogInformation("✅ Eski veriler tamamen temizlendi");
            
            var yazarlar = await CreateYazarlarAsync();
            var kategoriler = await CreateKategorilerAsync();
            var kitaplar = await CreateKitaplarAsync(yazarlar, kategoriler);
            var kullanicilar = await CreateKullanicilarAsync();
            await CreateOdunclerAsync(kitaplar, kullanicilar);
            
            _logger.LogInformation("=== SEED İŞLEMİ TAMAMLANDI (Yepyeni İlişkili Verilerle) ===");
        }

        private async Task<List<Yazar>> CreateYazarlarAsync()
        {
            var yazarlar = new List<Yazar>
            {
                new() { Ad = "Orhan", Soyad = "Pamuk", DogumTarihi = new DateTime(1952, 6, 7), Ulke = "Türkiye" },
                new() { Ad = "Elif", Soyad = "Şafak", DogumTarihi = new DateTime(1971, 10, 25), Ulke = "Türkiye" },
                new() { Ad = "Sabahattin", Soyad = "Ali", DogumTarihi = new DateTime(1907, 2, 25), Ulke = "Türkiye" },
                new() { Ad = "Nazım", Soyad = "Hikmet", DogumTarihi = new DateTime(1902, 1, 15), Ulke = "Türkiye" },
                new() { Ad = "Yaşar", Soyad = "Kemal", DogumTarihi = new DateTime(1922, 10, 9), Ulke = "Türkiye" },
                new() { Ad = "Ahmet Hamdi", Soyad = "Tanpınar", DogumTarihi = new DateTime(1901, 6, 23), Ulke = "Türkiye" },
                new() { Ad = "Zülfü", Soyad = "Livaneli", DogumTarihi = new DateTime(1946, 6, 20), Ulke = "Türkiye" },
                new() { Ad = "Peyami", Soyad = "Safa", DogumTarihi = new DateTime(1899, 4, 2), Ulke = "Türkiye" },
                new() { Ad = "Reşat Nuri", Soyad = "Güntekin", DogumTarihi = new DateTime(1889, 11, 25), Ulke = "Türkiye" },
                new() { Ad = "Oğuz", Soyad = "Atay", DogumTarihi = new DateTime(1934, 10, 12), Ulke = "Türkiye" },
                new() { Ad = "Sait Faik", Soyad = "Abasıyanık", DogumTarihi = new DateTime(1906, 11, 23), Ulke = "Türkiye" },
                new() { Ad = "Aziz", Soyad = "Nesin", DogumTarihi = new DateTime(1915, 12, 20), Ulke = "Türkiye" },
                new() { Ad = "Fakir", Soyad = "Baykurt", DogumTarihi = new DateTime(1929, 6, 15), Ulke = "Türkiye" },
                new() { Ad = "Kemal", Soyad = "Tahir", DogumTarihi = new DateTime(1910, 3, 13), Ulke = "Türkiye" },
                new() { Ad = "Haldun", Soyad = "Taner", DogumTarihi = new DateTime(1915, 3, 16), Ulke = "Türkiye" }
            };

            _context.Yazarlar.AddRange(yazarlar);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ {Count} yazar eklendi", yazarlar.Count);
            return yazarlar;
        }

        private async Task<List<Kategori>> CreateKategorilerAsync()
        {
            var kategoriler = new List<Kategori>
            {
                new() { Ad = "Roman", Aciklama = "Türk ve dünya romanları" },
                new() { Ad = "Şiir", Aciklama = "Şiir kitapları ve divanlar" },
                new() { Ad = "Tarih", Aciklama = "Tarih ve biyografi kitapları" },
                new() { Ad = "Felsefe", Aciklama = "Felsefe ve düşünce kitapları" },
                new() { Ad = "Çocuk", Aciklama = "Çocuk kitapları ve masallar" },
                new() { Ad = "Hikaye", Aciklama = "Kısa hikaye koleksiyonları" },
                new() { Ad = "Deneme", Aciklama = "Deneme ve makale kitapları" },
                new() { Ad = "Biyografi", Aciklama = "Ünlü kişilerin yaşam öyküleri" }
            };

            _context.Kategoriler.AddRange(kategoriler);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ {Count} kategori eklendi", kategoriler.Count);
            return kategoriler;
        }

        private async Task<List<Kitap>> CreateKitaplarAsync(List<Yazar> yazarlar, List<Kategori> kategoriler)
        {
            var random = new Random();
            var kitaplar = new List<Kitap>();

            var kitapBilgileri = new[]
            {
                new { Baslik = "Kar", YazarIndex = 0, KategoriIndex = 0 }, 
                new { Baslik = "Benim Adım Kırmızı", YazarIndex = 0, KategoriIndex = 0 },
                new { Baslik = "İstanbul Hatıraları", YazarIndex = 0, KategoriIndex = 0 },
                new { Baslik = "Aşk", YazarIndex = 1, KategoriIndex = 0 },
                new { Baslik = "İskender", YazarIndex = 1, KategoriIndex = 0 },
                new { Baslik = "Kürk Mantolu Madonna", YazarIndex = 2, KategoriIndex = 0 }, 
                new { Baslik = "İçimizdeki Şeytan", YazarIndex = 2, KategoriIndex = 0 },
                new { Baslik = "En Güzel Şiirler", YazarIndex = 3, KategoriIndex = 1 }, 
                new { Baslik = "Memleketimden İnsan Manzaraları", YazarIndex = 3, KategoriIndex = 1 },
                new { Baslik = "İnce Memed", YazarIndex = 4, KategoriIndex = 0 }, 
                new { Baslik = "Ölmez Otu", YazarIndex = 4, KategoriIndex = 0 },
                new { Baslik = "Huzur", YazarIndex = 5, KategoriIndex = 0 }, 
                new { Baslik = "Saatleri Ayarlama Enstitüsü", YazarIndex = 5, KategoriIndex = 0 },
                new { Baslik = "Bir Kedi Bir Adam Bir Ölüm", YazarIndex = 6, KategoriIndex = 0 }, 
                new { Baslik = "Konstantiniyye Hotel", YazarIndex = 6, KategoriIndex = 0 },
                new { Baslik = "Fatih Harbiye", YazarIndex = 7, KategoriIndex = 0 }, 
                new { Baslik = "Dokuzuncu Hariciye Koğuşu", YazarIndex = 7, KategoriIndex = 0 },
                new { Baslik = "Çalıkuşu", YazarIndex = 8, KategoriIndex = 0 },
                new { Baslik = "Yaprak Dökümü", YazarIndex = 8, KategoriIndex = 0 },
                new { Baslik = "Tutunamayanlar", YazarIndex = 9, KategoriIndex = 0 },
                new { Baslik = "Tehlikeli Oyunlar", YazarIndex = 9, KategoriIndex = 0 },
                new { Baslik = "Alemdağda Var Bir Yılan", YazarIndex = 10, KategoriIndex = 5 },
                new { Baslik = "Havuzbaşı", YazarIndex = 10, KategoriIndex = 5 },
                new { Baslik = "Zübük", YazarIndex = 11, KategoriIndex = 5 },
                new { Baslik = "Yaşar Ne Yaşar Ne Yaşamaz", YazarIndex = 11, KategoriIndex = 5 },
                new { Baslik = "Irazca'nın Dirliği", YazarIndex = 12, KategoriIndex = 0 }, 
                new { Baslik = "Yılanların Öcü", YazarIndex = 12, KategoriIndex = 0 },
                new { Baslik = "Devlet Ana", YazarIndex = 13, KategoriIndex = 2 }, 
                new { Baslik = "Esir Şehrin İnsanları", YazarIndex = 13, KategoriIndex = 2 },
                new { Baslik = "Şeytan Tüyü", YazarIndex = 14, KategoriIndex = 0 },
                new { Baslik = "Onikişer Çayevi", YazarIndex = 14, KategoriIndex = 5 }
            };

            for (int i = 0; i < kitapBilgileri.Length; i++)
            {
                var kitapBilgi = kitapBilgileri[i];
                var yazar = yazarlar[kitapBilgi.YazarIndex];
                var kategori = kategoriler[kitapBilgi.KategoriIndex];

                var kitap = new Kitap
                {
                    Baslik = kitapBilgi.Baslik,
                    ISBN = $"978-975-{random.Next(100, 999)}-{random.Next(1000, 9999)}-{i}",
                    YayinTarihi = new DateTime(random.Next(1950, 2020), random.Next(1, 13), random.Next(1, 28)),
                    SayfaSayisi = random.Next(150, 600),
                    MusaitMi = random.Next(0, 10) > 2, // %80 müsait
                    Konum = $"{random.Next(1, 4)}. Kat",
                    RafNo = $"{(char)('A' + random.Next(0, 6))}{random.Next(1, 200):D3}",
                    YazarId = yazar.Id,
                    KategoriId = kategori.Id
                };

                kitaplar.Add(kitap);
            }

            var ekKitapAdlari = new[]
            {
                "Göçebe", "Sevda Çiçeği", "Umut Işığı", "Karanlık Geceler", "Aşk ve Gurur",
                "Çılgın Yıllar", "Hayat Güzel", "Kalbimin Sultanı", "Anadolu Masalları",
                "Ege'nin Poyrazı", "Karadeniz'in İncisi", "Akdeniz Rüzgarları", "Trakya Ovası",
                "Marmara'nın Derinlikleri", "Dicle'nin Akışı", "Fırat'ın Sesi", "Yeşilırmak'ın Nağmesi",
                "Büyük Menderes", "Sakarya'nın Dansı", "Kızılırmak'ın Melodisi"
            };

            for (int i = 0; i < ekKitapAdlari.Length; i++)
            {
                var rastgeleYazar = yazarlar[random.Next(yazarlar.Count)];
                var rastgeleKategori = kategoriler[random.Next(kategoriler.Count)];

                var kitap = new Kitap
                {
                    Baslik = ekKitapAdlari[i],
                    ISBN = $"978-975-{random.Next(100, 999)}-{random.Next(1000, 9999)}-{i + 100}",
                    YayinTarihi = new DateTime(random.Next(1960, 2023), random.Next(1, 13), random.Next(1, 28)),
                    SayfaSayisi = random.Next(120, 500),
                    MusaitMi = random.Next(0, 10) > 2,
                    Konum = $"{random.Next(1, 4)}. Kat",
                    RafNo = $"{(char)('A' + random.Next(0, 6))}{random.Next(1, 200):D3}",
                    YazarId = rastgeleYazar.Id,
                    KategoriId = rastgeleKategori.Id
                };

                kitaplar.Add(kitap);
            }

            _context.Kitaplar.AddRange(kitaplar);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ {Count} kitap eklendi (yazarlar ve kategorilerle ilişkili)", kitaplar.Count);
            return kitaplar;
        }

        private async Task<List<Kullanici>> CreateKullanicilarAsync()
        {
            var random = new Random();
            var kullanicilar = new List<Kullanici>();

            var kullaniciBilgileri = new[]
            {
                new { Ad = "Ali", Soyad = "Yılmaz", Email = "ali.yilmaz@email.com", Telefon = "0532 123 45 67" },
                new { Ad = "Ayşe", Soyad = "Demir", Email = "ayse.demir@email.com", Telefon = "0533 234 56 78" },
                new { Ad = "Mehmet", Soyad = "Çelik", Email = "mehmet.celik@email.com", Telefon = "0534 345 67 89" },
                new { Ad = "Fatma", Soyad = "Şahin", Email = "fatma.sahin@email.com", Telefon = "0535 456 78 90" },
                new { Ad = "Hasan", Soyad = "Yıldız", Email = "hasan.yildiz@email.com", Telefon = "0536 567 89 01" },
                new { Ad = "Zeynep", Soyad = "Yıldırım", Email = "zeynep.yildirim@email.com", Telefon = "0537 678 90 12" },
                new { Ad = "İbrahim", Soyad = "Öztürk", Email = "ibrahim.ozturk@email.com", Telefon = "0538 789 01 23" },
                new { Ad = "Hatice", Soyad = "Aydın", Email = "hatice.aydin@email.com", Telefon = "0539 890 12 34" },
                new { Ad = "Mustafa", Soyad = "Özdemir", Email = "mustafa.ozdemir@email.com", Telefon = "0541 901 23 45" },
                new { Ad = "Emine", Soyad = "Arslan", Email = "emine.arslan@email.com", Telefon = "0542 012 34 56" },
                new { Ad = "Ahmet", Soyad = "Doğan", Email = "ahmet.dogan@email.com", Telefon = "0543 123 45 67" },
                new { Ad = "Elif", Soyad = "Aslan", Email = "elif.aslan@email.com", Telefon = "0544 234 56 78" },
                new { Ad = "Ömer", Soyad = "Çetin", Email = "omer.cetin@email.com", Telefon = "0545 345 67 89" },
                new { Ad = "Seda", Soyad = "Kara", Email = "seda.kara@email.com", Telefon = "0546 456 78 90" },
                new { Ad = "Burak", Soyad = "Koç", Email = "burak.koc@email.com", Telefon = "0547 567 89 01" },
                new { Ad = "Canan", Soyad = "Güler", Email = "canan.guler@email.com", Telefon = "0548 678 90 12" },
                new { Ad = "Emre", Soyad = "Taş", Email = "emre.tas@email.com", Telefon = "0549 789 01 23" },
                new { Ad = "Dilek", Soyad = "Kurt", Email = "dilek.kurt@email.com", Telefon = "0551 890 12 34" },
                new { Ad = "Serkan", Soyad = "Polat", Email = "serkan.polat@email.com", Telefon = "0552 901 23 45" },
                new { Ad = "Melike", Soyad = "Erdoğan", Email = "melike.erdogan@email.com", Telefon = "0553 012 34 56" }
            };

            for (int i = 0; i < kullaniciBilgileri.Length; i++)
            {
                var bilgi = kullaniciBilgileri[i];
                
                var kullanici = new Kullanici
                {
                    Ad = bilgi.Ad,
                    Soyad = bilgi.Soyad,
                    Email = bilgi.Email,
                    Telefon = bilgi.Telefon,
                    DogumTarihi = new DateTime(random.Next(1970, 2000), random.Next(1, 13), random.Next(1, 28)),
                    ToplamOduncSayisi = random.Next(0, 25),
                    AktifMi = random.Next(0, 20) > 1 // %95 aktif
                };

                kullanicilar.Add(kullanici);
            }

            _context.Kullanicilar.AddRange(kullanicilar);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ {Count} kullanıcı eklendi", kullanicilar.Count);
            return kullanicilar;
        }

        private async Task CreateOdunclerAsync(List<Kitap> kitaplar, List<Kullanici> kullanicilar)
        {
            var random = new Random();
            var oduncler = new List<Odunc>();

            for (int i = 0; i < 35; i++)
            {
                var rastgeleKitap = kitaplar[random.Next(kitaplar.Count)];
                var rastgeleKullanici = kullanicilar[random.Next(kullanicilar.Count)];
                
                var oduncTarihi = DateTime.Now.AddDays(-random.Next(1, 60));
                var geriVerilmesiGerekenTarih = oduncTarihi.AddDays(14); 
                
                
                bool iadeEdildi = false; 
                DateTime? geriVerilisTarihi = null;
                
                var durum = random.Next(0, 4);
                switch (durum)
                {
                    case 0: 
                        iadeEdildi = true;
                        geriVerilisTarihi = geriVerilmesiGerekenTarih.AddDays(random.Next(-2, 1));
                        break;
                    case 1: 
                        iadeEdildi = true;
                        geriVerilisTarihi = geriVerilmesiGerekenTarih.AddDays(random.Next(1, 10));
                        break;
                    case 2: 
                        iadeEdildi = false;
                        if (DateTime.Now <= geriVerilmesiGerekenTarih) break;
                        goto case 3; 
                    case 3: 
                        iadeEdildi = false;
                        
                        oduncTarihi = DateTime.Now.AddDays(-random.Next(20, 40));
                        geriVerilmesiGerekenTarih = oduncTarihi.AddDays(14);
                        break;
                }

                var odunc = new Odunc
                {
                    OduncAlinmaTarihi = oduncTarihi,
                    GeriVerilmesiGerekenTarih = geriVerilmesiGerekenTarih,
                    GeriVerilisTarihi = geriVerilisTarihi,
                    IadeEdildiMi = iadeEdildi,
                    KitapId = rastgeleKitap.Id,
                    KullaniciId = rastgeleKullanici.Id
                };

                oduncler.Add(odunc);
            }

            _context.Oduncler.AddRange(oduncler);
            await _context.SaveChangesAsync();
            
            var aktifOduncler = oduncler.Count(o => !o.IadeEdildiMi);
            var gecikmiOduncler = oduncler.Count(o => !o.IadeEdildiMi && o.GeriVerilmesiGerekenTarih < DateTime.Now);
            
            _logger.LogInformation("✅ {Count} ödünç kaydı eklendi", oduncler.Count);
            _logger.LogInformation("   - {AktifCount} aktif ödünç", aktifOduncler);
            _logger.LogInformation("   - {GecikmiCount} gecikmiş ödünç (cezalı!)", gecikmiOduncler);
        }
    }
}