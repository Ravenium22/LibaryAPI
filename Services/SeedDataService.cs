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
            _logger.LogInformation("=== SEED İŞLEMİ BAŞLADI ===");
            
            var yazarSayisi = await _context.Yazarlar.CountAsync();
            var kitapSayisi = await _context.Kitaplar.CountAsync();
            var kullaniciSayisi = await _context.Kullanicilar.CountAsync();
            var kategoriSayisi = await _context.Kategoriler.CountAsync();
            var kutuphaneSayisi = await _context.Libraries.CountAsync();

            _logger.LogInformation("Mevcut veriler - Yazar: {YazarCount}, Kitap: {KitapCount}, Kullanici: {KullaniciCount}, Kategori: {KategoriCount}, Kutuphane: {KutuphaneCount}", 
                yazarSayisi, kitapSayisi, kullaniciSayisi, kategoriSayisi, kutuphaneSayisi);

            // Hedef sayılar
            const int hedefYazar = 15;
            const int hedefKitap = 50;
            const int hedefKullanici = 20;
            const int hedefKategori = 8;
            const int hedefKutuphane = 5;

            List<Yazar> yazarlar = await _context.Yazarlar.ToListAsync();
            List<Kategori> kategoriler = await _context.Kategoriler.ToListAsync();
            List<Library> kutuphaneler = await _context.Libraries.ToListAsync();

            // İlk kez çalışıyorsa temel verileri ekle
            if (yazarSayisi == 0)
            {
                yazarlar = await SeedTemelYazarlarAsync();
            }

            if (kategoriSayisi == 0)
            {
                kategoriler = await SeedTemelKategorilerAsync();
            }

            if (kutuphaneSayisi == 0)
            {
                kutuphaneler = await SeedTemelKutuphanelerAsync();
            }

            // Eksik yazarları ekle
            if (yazarSayisi < hedefYazar)
            {
                var yeniYazarlar = await SeedEkYazarlarAsync(hedefYazar - yazarSayisi);
                yazarlar.AddRange(yeniYazarlar);
            }

            // Eksik kategorileri ekle  
            if (kategoriSayisi < hedefKategori)
            {
                var yeniKategoriler = await SeedEkKategorilerAsync(hedefKategori - kategoriSayisi);
                kategoriler.AddRange(yeniKategoriler);
            }

            // Eksik kütüphaneleri ekle
            if (kutuphaneSayisi < hedefKutuphane)
            {
                var yeniKutuphaneler = await SeedEkKutuphanelerAsync(hedefKutuphane - kutuphaneSayisi);
                kutuphaneler.AddRange(yeniKutuphaneler);
            }

            // Eksik kitapları ekle
            if (kitapSayisi < hedefKitap)
            {
                await SeedEkKitaplarAsync(yazarlar, kategoriler, kutuphaneler, hedefKitap - kitapSayisi);
            }

            // Eksik kullanıcıları ekle
            if (kullaniciSayisi < hedefKullanici)
            {
                await SeedEkKullanicilarAsync(hedefKullanici - kullaniciSayisi);
            }

            // Ödünç kayıtları varsa ekleme
            var oduncSayisi = await _context.Oduncler.CountAsync();
            if (oduncSayisi == 0)
            {
                await SeedOdunclerAsync();
            }

            _logger.LogInformation("=== SEED İŞLEMİ TAMAMLANDI ===");
        }

        private async Task<List<Yazar>> SeedTemelYazarlarAsync()
        {
            var yazarlar = new List<Yazar>
            {
                new() { Ad = "Orhan", Soyad = "Pamuk", DogumTarihi = new DateTime(1952, 6, 7), Ulke = "Türkiye" },
                new() { Ad = "Sabahattin", Soyad = "Ali", DogumTarihi = new DateTime(1907, 2, 25), Ulke = "Türkiye" },
                new() { Ad = "Nazım", Soyad = "Hikmet", DogumTarihi = new DateTime(1902, 1, 15), Ulke = "Türkiye" },
                new() { Ad = "Ahmet Hamdi", Soyad = "Tanpınar", DogumTarihi = new DateTime(1901, 6, 23), Ulke = "Türkiye" },
                new() { Ad = "Yaşar", Soyad = "Kemal", DogumTarihi = new DateTime(1922, 10, 9), Ulke = "Türkiye" }
            };

            _context.Yazarlar.AddRange(yazarlar);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("{Count} temel yazar eklendi", yazarlar.Count);
            return yazarlar;
        }

        private async Task<List<Kategori>> SeedTemelKategorilerAsync()
        {
            var kategoriler = new List<Kategori>
            {
                new() { Ad = "Roman", Aciklama = "Türk ve dünya romanları" },
                new() { Ad = "Şiir", Aciklama = "Şiir kitapları ve divanlar" },
                new() { Ad = "Tarih", Aciklama = "Tarih ve biyografi kitapları" },
                new() { Ad = "Felsefe", Aciklama = "Felsefe ve düşünce kitapları" },
                new() { Ad = "Çocuk", Aciklama = "Çocuk kitapları" }
            };

            _context.Kategoriler.AddRange(kategoriler);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("{Count} temel kategori eklendi", kategoriler.Count);
            return kategoriler;
        }

        private async Task<List<Library>> SeedTemelKutuphanelerAsync()
        {
            var kutuphaneler = new List<Library>
            {
                new() { İsim = "Merkez Kütüphane", Adres = "Atatürk Caddesi No:1, Beyoğlu", Telefon = "0212 555 0001", KitapSayisi = 5000, UyeSayisi = 1200 },
                new() { İsim = "Kadıköy Şubesi", Adres = "Bahariye Caddesi No:45, Kadıköy", Telefon = "0216 555 0002", KitapSayisi = 3000, UyeSayisi = 800 },
                new() { İsim = "Beşiktaş Şubesi", Adres = "Barbaros Bulvarı No:20, Beşiktaş", Telefon = "0212 555 0003", KitapSayisi = 2500, UyeSayisi = 600 }
            };

            _context.Libraries.AddRange(kutuphaneler);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("{Count} temel kütüphane eklendi", kutuphaneler.Count);
            return kutuphaneler;
        }

        private async Task<List<Yazar>> SeedEkYazarlarAsync(int miktar)
        {
            var yeniYazarlar = new List<Yazar>
            {
                new() { Ad = "Elif", Soyad = "Şafak", DogumTarihi = new DateTime(1971, 10, 25), Ulke = "Türkiye" },
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

            var eklenecekler = yeniYazarlar.Take(miktar).ToList();
            _context.Yazarlar.AddRange(eklenecekler);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("{Count} yeni yazar eklendi", eklenecekler.Count);
            return eklenecekler;
        }

        private async Task<List<Kategori>> SeedEkKategorilerAsync(int miktar)
        {
            var yeniKategoriler = new List<Kategori>
            {
                new() { Ad = "Hikaye", Aciklama = "Kısa hikaye koleksiyonları" },
                new() { Ad = "Deneme", Aciklama = "Deneme ve makale kitapları" },
                new() { Ad = "Biyografi", Aciklama = "Yaşam öyküleri" }
            };

            var eklenecekler = yeniKategoriler.Take(miktar).ToList();
            _context.Kategoriler.AddRange(eklenecekler);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("{Count} yeni kategori eklendi", eklenecekler.Count);
            return eklenecekler;
        }

        private async Task<List<Library>> SeedEkKutuphanelerAsync(int miktar)
        {
            var yeniKutuphaneler = new List<Library>
            {
                new() { İsim = "Üsküdar Şubesi", Adres = "İskele Caddesi No:12, Üsküdar", Telefon = "0216 555 0004", KitapSayisi = 2000, UyeSayisi = 450 },
                new() { İsim = "Şişli Şubesi", Adres = "Meşrutiyet Caddesi No:78, Şişli", Telefon = "0212 555 0005", KitapSayisi = 1800, UyeSayisi = 380 }
            };

            var eklenecekler = yeniKutuphaneler.Take(miktar).ToList();
            _context.Libraries.AddRange(eklenecekler);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("{Count} yeni kütüphane eklendi", eklenecekler.Count);
            return eklenecekler;
        }

        private async Task SeedEkKitaplarAsync(List<Yazar> yazarlar, List<Kategori> kategoriler, List<Library> kutuphaneler, int miktar)
        {
            var random = new Random();
            var yeniKitaplar = new List<Kitap>();

            var kitapAdlari = new[]
            {
                "Kar", "Benim Adım Kırmızı", "İstanbul", "Kürk Mantolu Madonna", "İçimizdeki Şeytan",
                "En Güzel Şiirler", "Serenad", "Bir Gün Tek Başına", "Çalıkuşu", "Yaprak Dökümü",
                "Tehlikeli Oyun", "Şeker Portakalı", "Göçebe", "Kırmızı Pelerinli Kız", "Denizler Altında",
                "Yalnız Efe", "Gurbet Kuşları", "Rüya", "Sevginin Gücü", "Karanlık Geceler",
                "Aşk ve Gurur", "Çılgın Yıllar", "Hayat Güzel", "Umut Işığı", "Sevda Çiçeği",
                "Kalbimin Sultanı", "İstanbul Hatıraları", "Anadolu Masalları", "Ege'nin Poyrazı", "Karadeniz'in İncisi",
                "Akdeniz Rüzgarları", "Trakya Ovası", "Marmara'nın Derinlikleri", "Dicle'nin Akışı", "Fırat'ın Sesi",
                "Menderes'in Türküsü", "Gediz'in Şarkısı", "Sakarya'nın Dansı", "Kızılırmak'ın Melodisi", "Yeşilırmak'ın Nağmesi",
                "Büyük Menderes", "Küçük Menderes", "Bartın Çayı", "Göksu'nun Hikayesi", "Seyhan'ın Destanı",
                "Ceyhan'ın Masalı", "Berdan'ın Şiiri", "Zamanti'nin Türküsü", "Tarsus Çayı", "Asi'nin Efsanesi"
            };

            for (int i = 0; i < miktar && i < kitapAdlari.Length; i++)
            {
                var rastgeleYazar = yazarlar[random.Next(yazarlar.Count)];
                var rastgeleKategori = kategoriler[random.Next(kategoriler.Count)];
                var rastgeleKutuphane = kutuphaneler[random.Next(kutuphaneler.Count)];

                var kitap = new Kitap
                {
                    Baslik = kitapAdlari[i],
                    ISBN = $"978-975-{random.Next(100, 999)}-{random.Next(1000, 9999)}-{i}",
                    YayinTarihi = new DateTime(random.Next(1950, 2023), random.Next(1, 13), random.Next(1, 28)),
                    SayfaSayisi = random.Next(120, 600),
                    MusaitMi = random.Next(0, 2) == 1,
                    YazarId = rastgeleYazar.Id,
                    KategoriId = rastgeleKategori.Id,
                    LibraryId = rastgeleKutuphane.Id
                };

                yeniKitaplar.Add(kitap);
            }

            _context.Kitaplar.AddRange(yeniKitaplar);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("{Count} yeni kitap eklendi", yeniKitaplar.Count);
        }

        private async Task SeedEkKullanicilarAsync(int miktar)
        {
            var random = new Random();
            var adlar = new[] { "Ali", "Ayşe", "Mehmet", "Fatma", "Hasan", "Zeynep", "İbrahim", "Hatice", "Mustafa", "Emine", "Ahmet", "Elif", "Ömer", "Seda", "Burak", "Canan", "Emre", "Dilek" };
            var soyadlar = new[] { "Yılmaz", "Demir", "Çelik", "Şahin", "Yıldız", "Yıldırım", "Öztürk", "Aydin", "Özdemir", "Arslan", "Doğan", "Aslan", "Çetin", "Kara", "Koç" };

            var yeniKullanicilar = new List<Kullanici>();

            for (int i = 0; i < miktar; i++)
            {
                var ad = adlar[random.Next(adlar.Length)];
                var soyad = soyadlar[random.Next(soyadlar.Length)];
                
                var kullanici = new Kullanici
                {
                    Ad = ad,
                    Soyad = soyad,
                    Email = $"{ad.ToLower()}.{soyad.ToLower()}{i}@test.com",
                    Telefon = $"05{random.Next(50, 60)} {random.Next(100, 999)} {random.Next(10, 99)} {random.Next(10, 99)}",
                    DogumTarihi = new DateTime(random.Next(1970, 2005), random.Next(1, 13), random.Next(1, 28)),
                    ToplamOduncSayisi = random.Next(0, 15),
                    AktifMi = random.Next(0, 10) > 1 // %90 aktif
                };

                yeniKullanicilar.Add(kullanici);
            }

            _context.Kullanicilar.AddRange(yeniKullanicilar);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("{Count} yeni kullanıcı eklendi", yeniKullanicilar.Count);
        }

        private async Task SeedOdunclerAsync()
        {
            var random = new Random();
            var kitaplar = await _context.Kitaplar.Take(10).ToListAsync();
            var kullanicilar = await _context.Kullanicilar.Take(10).ToListAsync();

            if (!kitaplar.Any() || !kullanicilar.Any())
            {
                _logger.LogWarning("Ödünç kayıtları oluşturulamadı: Yeterli kitap veya kullanıcı yok");
                return;
            }

            var oduncler = new List<Odunc>();

            for (int i = 0; i < Math.Min(15, kitaplar.Count * kullanicilar.Count / 5); i++)
            {
                var rastgeleKitap = kitaplar[random.Next(kitaplar.Count)];
                var rastgeleKullanici = kullanicilar[random.Next(kullanicilar.Count)];
                
                var oduncTarihi = DateTime.Now.AddDays(-random.Next(1, 30));
                var teslimTarihi = oduncTarihi.AddDays(14); // 2 hafta ödünç süresi
                var iadeEdildi = random.Next(0, 3) > 0; // %66 iade edilmiş

                var odunc = new Odunc
                {
                    OduncTarihi = oduncTarihi,
                    TeslimTarihi = teslimTarihi,
                    IadeTarihi = iadeEdildi ? teslimTarihi.AddDays(random.Next(-5, 3)) : null,
                    IadeEdildiMi = iadeEdildi,
                    KitapId = rastgeleKitap.Id,
                    KullaniciId = rastgeleKullanici.Id
                };

                oduncler.Add(odunc);
            }

            _context.Oduncler.AddRange(oduncler);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("{Count} ödünç kaydı eklendi", oduncler.Count);
        }
    }
}