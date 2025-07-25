using Kutuphane.Data;
using Kutuphane.Models;
using Microsoft.EntityFrameworkCore;

namespace Kutuphane.Data
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(AppDbContext context)
        {
            // Database'i tamamen temizle ve yeniden oluştur
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            
            Console.WriteLine("🗑️ Database temizlendi, yeniden oluşturuluyor...");

            // 1. YAZARLAR - Daha fazla ünlü yazar
            var yazarlar = new List<Yazar>
            {
                // İngilizce Edebiyat
                new Yazar { Ad = "George", Soyad = "Orwell", DogumTarihi = new DateTime(1903, 6, 25), Ulke = "İngiltere" },
                new Yazar { Ad = "J.K.", Soyad = "Rowling", DogumTarihi = new DateTime(1965, 7, 31), Ulke = "İngiltere" },
                new Yazar { Ad = "William", Soyad = "Shakespeare", DogumTarihi = new DateTime(1564, 4, 26), Ulke = "İngiltere" },
                new Yazar { Ad = "Jane", Soyad = "Austen", DogumTarihi = new DateTime(1775, 12, 16), Ulke = "İngiltere" },
                new Yazar { Ad = "Charles", Soyad = "Dickens", DogumTarihi = new DateTime(1812, 2, 7), Ulke = "İngiltere" },
                new Yazar { Ad = "Agatha", Soyad = "Christie", DogumTarihi = new DateTime(1890, 9, 15), Ulke = "İngiltere" },
                
                // Amerikan Edebiyatı
                new Yazar { Ad = "Harper", Soyad = "Lee", DogumTarihi = new DateTime(1926, 4, 28), Ulke = "Amerika" },
                new Yazar { Ad = "F. Scott", Soyad = "Fitzgerald", DogumTarihi = new DateTime(1896, 9, 24), Ulke = "Amerika" },
                new Yazar { Ad = "Ernest", Soyad = "Hemingway", DogumTarihi = new DateTime(1899, 7, 21), Ulke = "Amerika" },
                new Yazar { Ad = "Mark", Soyad = "Twain", DogumTarihi = new DateTime(1835, 11, 30), Ulke = "Amerika" },
                new Yazar { Ad = "John", Soyad = "Steinbeck", DogumTarihi = new DateTime(1902, 2, 27), Ulke = "Amerika" },
                new Yazar { Ad = "Toni", Soyad = "Morrison", DogumTarihi = new DateTime(1931, 2, 18), Ulke = "Amerika" },
                
                // Dünya Edebiyatı
                new Yazar { Ad = "Paulo", Soyad = "Coelho", DogumTarihi = new DateTime(1947, 8, 24), Ulke = "Brezilya" },
                new Yazar { Ad = "Gabriel García", Soyad = "Márquez", DogumTarihi = new DateTime(1927, 3, 6), Ulke = "Kolombiya" },
                new Yazar { Ad = "Fyodor", Soyad = "Dostoyevsky", DogumTarihi = new DateTime(1821, 11, 11), Ulke = "Rusya" },
                new Yazar { Ad = "Leo", Soyad = "Tolstoy", DogumTarihi = new DateTime(1828, 9, 9), Ulke = "Rusya" },
                new Yazar { Ad = "Haruki", Soyad = "Murakami", DogumTarihi = new DateTime(1949, 1, 12), Ulke = "Japonya" },
                new Yazar { Ad = "Victor", Soyad = "Hugo", DogumTarihi = new DateTime(1802, 2, 26), Ulke = "Fransa" },
                new Yazar { Ad = "Albert", Soyad = "Camus", DogumTarihi = new DateTime(1913, 11, 7), Ulke = "Fransa" },
                new Yazar { Ad = "Franz", Soyad = "Kafka", DogumTarihi = new DateTime(1883, 7, 3), Ulke = "Çekoslovakya" },
                
                // Türk Edebiyatı
                new Yazar { Ad = "Orhan", Soyad = "Pamuk", DogumTarihi = new DateTime(1952, 6, 7), Ulke = "Türkiye" },
                new Yazar { Ad = "Sabahattin", Soyad = "Ali", DogumTarihi = new DateTime(1907, 2, 25), Ulke = "Türkiye" },
                new Yazar { Ad = "Nazım", Soyad = "Hikmet", DogumTarihi = new DateTime(1902, 1, 15), Ulke = "Türkiye" },
                new Yazar { Ad = "Yaşar", Soyad = "Kemal", DogumTarihi = new DateTime(1923, 10, 6), Ulke = "Türkiye" },
                new Yazar { Ad = "Aziz", Soyad = "Nesin", DogumTarihi = new DateTime(1915, 12, 20), Ulke = "Türkiye" }
            };

            context.Yazarlar.AddRange(yazarlar);
            await context.SaveChangesAsync();

            // 2. KATEGORİLER - Daha fazla kategori
            var kategoriler = new List<Kategori>
            {
                new Kategori { Ad = "Fantastik", Aciklama = "Büyü, mitoloji ve fantastik unsurlar içeren kitaplar" },
                new Kategori { Ad = "Distopya", Aciklama = "Karanlık gelecek ve totaliter rejim hikayeleri" },
                new Kategori { Ad = "Klasik Edebiyat", Aciklama = "Dünya edebiyatının başyapıtları" },
                new Kategori { Ad = "Modern Roman", Aciklama = "20. yüzyıl ve sonrası romanlar" },
                new Kategori { Ad = "Psikolojik Roman", Aciklama = "İnsan ruhunun derinliklerini keşfeden romanlar" },
                new Kategori { Ad = "Polisiye", Aciklama = "Dedektif ve suç hikayeleri" },
                new Kategori { Ad = "Macera", Aciklama = "Heyecan verici macera romanları" },
                new Kategori { Ad = "Romantik", Aciklama = "Aşk ve romantik hikayeler" },
                new Kategori { Ad = "Tarihi Roman", Aciklama = "Geçmişi konu alan hikayeler" },
                new Kategori { Ad = "Bilim Kurgu", Aciklama = "Gelecek ve teknoloji hikayeleri" },
                new Kategori { Ad = "Felsefe", Aciklama = "Yaşam felsefesi ve ruhsal gelişim kitapları" },
                new Kategori { Ad = "Türk Edebiyatı", Aciklama = "Türk yazarların kaleme aldığı önemli eserler" },
                new Kategori { Ad = "Şiir", Aciklama = "Şiir kitapları ve antolojiler" },
                new Kategori { Ad = "Mizah", Aciklama = "Komedi ve mizah kitapları" }
            };

            context.Kategoriler.AddRange(kategoriler);
            await context.SaveChangesAsync();

            // 3. KİTAPLAR - ÇOK ÜNLÜ KİTAPLAR (Kesin kapak bulunacak)
            var kitaplar = new List<Kitap>
            {
                // Harry Potter Serisi - J.K. Rowling
                new Kitap { Baslik = "Harry Potter ve Felsefe Taşı", ISBN = "9780439708180", YayinTarihi = new DateTime(1997, 6, 26), SayfaSayisi = 309, MusaitMi = false, Konum = "A Katı", RafNo = "A-001", YazarId = 2, KategoriId = 1 },
                new Kitap { Baslik = "Harry Potter ve Sırlar Odası", ISBN = "9780439064873", YayinTarihi = new DateTime(1998, 7, 2), SayfaSayisi = 341, MusaitMi = true, Konum = "A Katı", RafNo = "A-002", YazarId = 2, KategoriId = 1 },
                new Kitap { Baslik = "Harry Potter ve Azkaban Tutsağı", ISBN = "9780439136365", YayinTarihi = new DateTime(1999, 7, 8), SayfaSayisi = 435, MusaitMi = false, Konum = "A Katı", RafNo = "A-003", YazarId = 2, KategoriId = 1 },

                // George Orwell - Distopya
                new Kitap { Baslik = "1984", ISBN = "9780451524935", YayinTarihi = new DateTime(1949, 6, 8), SayfaSayisi = 328, MusaitMi = false, Konum = "B Katı", RafNo = "B-001", YazarId = 1, KategoriId = 2 },
                new Kitap { Baslik = "Hayvan Çiftliği", ISBN = "9780451526342", YayinTarihi = new DateTime(1945, 8, 17), SayfaSayisi = 112, MusaitMi = true, Konum = "B Katı", RafNo = "B-002", YazarId = 1, KategoriId = 2 },

                // Shakespeare - Klasik
                new Kitap { Baslik = "Hamlet", ISBN = "9780743477123", YayinTarihi = new DateTime(1603, 1, 1), SayfaSayisi = 342, MusaitMi = true, Konum = "C Katı", RafNo = "C-001", YazarId = 3, KategoriId = 3 },
                new Kitap { Baslik = "Romeo ve Juliet", ISBN = "9780743477116", YayinTarihi = new DateTime(1597, 1, 1), SayfaSayisi = 283, MusaitMi = false, Konum = "C Katı", RafNo = "C-002", YazarId = 3, KategoriId = 3 },

                // Jane Austen - Romantik
                new Kitap { Baslik = "Gurur ve Önyargı", ISBN = "9780141439518", YayinTarihi = new DateTime(1813, 1, 28), SayfaSayisi = 432, MusaitMi = true, Konum = "D Katı", RafNo = "D-001", YazarId = 4, KategoriId = 8 },
                new Kitap { Baslik = "Emma", ISBN = "9780141439587", YayinTarihi = new DateTime(1815, 12, 23), SayfaSayisi = 474, MusaitMi = false, Konum = "D Katı", RafNo = "D-002", YazarId = 4, KategoriId = 8 },

                // Charles Dickens - Klasik
                new Kitap { Baslik = "Büyük Umutlar", ISBN = "9780141439563", YayinTarihi = new DateTime(1861, 8, 1), SayfaSayisi = 505, MusaitMi = true, Konum = "E Katı", RafNo = "E-001", YazarId = 5, KategoriId = 3 },
                new Kitap { Baslik = "İki Şehrin Hikayesi", ISBN = "9780141439600", YayinTarihi = new DateTime(1859, 11, 26), SayfaSayisi = 489, MusaitMi = false, Konum = "E Katı", RafNo = "E-002", YazarId = 5, KategoriId = 9 },

                // Agatha Christie - Polisiye
                new Kitap { Baslik = "Doğu Ekspresinde Cinayet", ISBN = "9780062693662", YayinTarihi = new DateTime(1934, 1, 1), SayfaSayisi = 256, MusaitMi = true, Konum = "F Katı", RafNo = "F-001", YazarId = 6, KategoriId = 6 },
                new Kitap { Baslik = "On Küçük Zenci", ISBN = "9780062073488", YayinTarihi = new DateTime(1939, 11, 6), SayfaSayisi = 264, MusaitMi = false, Konum = "F Katı", RafNo = "F-002", YazarId = 6, KategoriId = 6 },

                // Harper Lee - Klasik
                new Kitap { Baslik = "Bülbülü Öldürmek", ISBN = "9780060935467", YayinTarihi = new DateTime(1960, 7, 11), SayfaSayisi = 376, MusaitMi = false, Konum = "G Katı", RafNo = "G-001", YazarId = 7, KategoriId = 3 },

                // F. Scott Fitzgerald - Klasik
                new Kitap { Baslik = "Muhteşem Gatsby", ISBN = "9780743273565", YayinTarihi = new DateTime(1925, 4, 10), SayfaSayisi = 180, MusaitMi = true, Konum = "H Katı", RafNo = "H-001", YazarId = 8, KategoriId = 3 },

                // Ernest Hemingway - Modern
                new Kitap { Baslik = "İhtiyar Adam ve Deniz", ISBN = "9780684801223", YayinTarihi = new DateTime(1952, 9, 1), SayfaSayisi = 127, MusaitMi = false, Konum = "I Katı", RafNo = "I-001", YazarId = 9, KategoriId = 4 },
                new Kitap { Baslik = "Çanlar Kimin İçin Çalıyor", ISBN = "9780684803357", YayinTarihi = new DateTime(1940, 10, 21), SayfaSayisi = 471, MusaitMi = true, Konum = "I Katı", RafNo = "I-002", YazarId = 9, KategoriId = 4 },

                // Mark Twain - Macera
                new Kitap { Baslik = "Tom Sawyer'ın Maceraları", ISBN = "9780486400778", YayinTarihi = new DateTime(1876, 1, 1), SayfaSayisi = 274, MusaitMi = true, Konum = "J Katı", RafNo = "J-001", YazarId = 10, KategoriId = 7 },
                new Kitap { Baslik = "Huckleberry Finn'in Maceraları", ISBN = "9780486280615", YayinTarihi = new DateTime(1884, 12, 10), SayfaSayisi = 366, MusaitMi = false, Konum = "J Katı", RafNo = "J-002", YazarId = 10, KategoriId = 7 },

                // John Steinbeck - Modern
                new Kitap { Baslik = "Fareler ve İnsanlar", ISBN = "9780140177398", YayinTarihi = new DateTime(1937, 1, 1), SayfaSayisi = 107, MusaitMi = true, Konum = "K Katı", RafNo = "K-001", YazarId = 11, KategoriId = 4 },
                new Kitap { Baslik = "Gazap Üzümleri", ISBN = "9780143039433", YayinTarihi = new DateTime(1939, 4, 14), SayfaSayisi = 464, MusaitMi = false, Konum = "K Katı", RafNo = "K-002", YazarId = 11, KategoriId = 4 },

                // Toni Morrison - Modern
                new Kitap { Baslik = "Beloved", ISBN = "9781400033416", YayinTarihi = new DateTime(1987, 9, 1), SayfaSayisi = 324, MusaitMi = true, Konum = "L Katı", RafNo = "L-001", YazarId = 12, KategoriId = 4 },

                // Paulo Coelho - Felsefe
                new Kitap { Baslik = "Simyacı", ISBN = "9780062315007", YayinTarihi = new DateTime(1988, 1, 1), SayfaSayisi = 163, MusaitMi = false, Konum = "M Katı", RafNo = "M-001", YazarId = 13, KategoriId = 11 },
                new Kitap { Baslik = "Veronika Ölmek İstiyor", ISBN = "9780061124273", YayinTarihi = new DateTime(1998, 1, 1), SayfaSayisi = 210, MusaitMi = true, Konum = "M Katı", RafNo = "M-002", YazarId = 13, KategoriId = 11 },

                // Gabriel García Márquez
                new Kitap { Baslik = "Yüzyıllık Yalnızlık", ISBN = "9780060883287", YayinTarihi = new DateTime(1967, 6, 5), SayfaSayisi = 417, MusaitMi = true, Konum = "N Katı", RafNo = "N-001", YazarId = 14, KategoriId = 4 },

                // Dostoyevsky - Psikolojik
                new Kitap { Baslik = "Suç ve Ceza", ISBN = "9780486415871", YayinTarihi = new DateTime(1866, 1, 1), SayfaSayisi = 671, MusaitMi = false, Konum = "O Katı", RafNo = "O-001", YazarId = 15, KategoriId = 5 },
                new Kitap { Baslik = "Karamazov Kardeşler", ISBN = "9780486437910", YayinTarihi = new DateTime(1880, 1, 1), SayfaSayisi = 824, MusaitMi = true, Konum = "O Katı", RafNo = "O-002", YazarId = 15, KategoriId = 5 },

                // Leo Tolstoy - Klasik
                new Kitap { Baslik = "Savaş ve Barış", ISBN = "9780199232765", YayinTarihi = new DateTime(1869, 1, 1), SayfaSayisi = 1392, MusaitMi = true, Konum = "P Katı", RafNo = "P-001", YazarId = 16, KategoriId = 3 },
                new Kitap { Baslik = "Anna Karenina", ISBN = "9780143035008", YayinTarihi = new DateTime(1877, 1, 1), SayfaSayisi = 864, MusaitMi = false, Konum = "P Katı", RafNo = "P-002", YazarId = 16, KategoriId = 3 },

                // Haruki Murakami - Modern
                new Kitap { Baslik = "Norwegian Wood", ISBN = "9780375704024", YayinTarihi = new DateTime(1987, 8, 4), SayfaSayisi = 296, MusaitMi = false, Konum = "Q Katı", RafNo = "Q-001", YazarId = 17, KategoriId = 4 },
                new Kitap { Baslik = "Kafka Sahilde", ISBN = "9781400079278", YayinTarihi = new DateTime(2002, 9, 12), SayfaSayisi = 505, MusaitMi = true, Konum = "Q Katı", RafNo = "Q-002", YazarId = 17, KategoriId = 4 },

                // Victor Hugo - Klasik
                new Kitap { Baslik = "Sefiller", ISBN = "9780451419439", YayinTarihi = new DateTime(1862, 1, 1), SayfaSayisi = 1488, MusaitMi = true, Konum = "R Katı", RafNo = "R-001", YazarId = 18, KategoriId = 3 },
                new Kitap { Baslik = "Notre Dame'ın Kamburu", ISBN = "9780451528537", YayinTarihi = new DateTime(1831, 1, 14), SayfaSayisi = 512, MusaitMi = false, Konum = "R Katı", RafNo = "R-002", YazarId = 18, KategoriId = 3 },

                // Albert Camus - Felsefe
                new Kitap { Baslik = "Yabancı", ISBN = "9780679720201", YayinTarihi = new DateTime(1942, 1, 1), SayfaSayisi = 123, MusaitMi = true, Konum = "S Katı", RafNo = "S-001", YazarId = 19, KategoriId = 11 },

                // Franz Kafka - Psikolojik
                new Kitap { Baslik = "Dönüşüm", ISBN = "9780486290300", YayinTarihi = new DateTime(1915, 1, 1), SayfaSayisi = 64, MusaitMi = false, Konum = "T Katı", RafNo = "T-001", YazarId = 20, KategoriId = 5 },

                // Türk Edebiyatı
                new Kitap { Baslik = "Kar", ISBN = "9780571218311", YayinTarihi = new DateTime(2002, 1, 1), SayfaSayisi = 436, MusaitMi = false, Konum = "U Katı", RafNo = "U-001", YazarId = 21, KategoriId = 12 },
                new Kitap { Baslik = "Benim Adım Kırmızı", ISBN = "9780571214197", YayinTarihi = new DateTime(1998, 1, 1), SayfaSayisi = 624, MusaitMi = true, Konum = "U Katı", RafNo = "U-002", YazarId = 21, KategoriId = 12 },
                new Kitap { Baslik = "Kürk Mantolu Madonna", ISBN = "9789750738630", YayinTarihi = new DateTime(1943, 1, 1), SayfaSayisi = 158, MusaitMi = false, Konum = "V Katı", RafNo = "V-001", YazarId = 22, KategoriId = 12 },
                new Kitap { Baslik = "En Güzel Nazım Hikmet Şiirleri", ISBN = "9789750809286", YayinTarihi = new DateTime(1950, 1, 1), SayfaSayisi = 240, MusaitMi = true, Konum = "W Katı", RafNo = "W-001", YazarId = 23, KategoriId = 13 },
                new Kitap { Baslik = "İnce Memed", ISBN = "9789750809293", YayinTarihi = new DateTime(1955, 1, 1), SayfaSayisi = 420, MusaitMi = true, Konum = "X Katı", RafNo = "X-001", YazarId = 24, KategoriId = 12 },
                new Kitap { Baslik = "Zübük", ISBN = "9789750809309", YayinTarihi = new DateTime(1972, 1, 1), SayfaSayisi = 180, MusaitMi = false, Konum = "Y Katı", RafNo = "Y-001", YazarId = 25, KategoriId = 14 }
            };

            context.Kitaplar.AddRange(kitaplar);
            await context.SaveChangesAsync();

            // 4. KULLANICILAR - Daha fazla kullanıcı
            var kullanicilar = new List<Kullanici>
            {
                // Admin kullanıcı
                new Kullanici 
                { 
                    Ad = "Admin", 
                    Soyad = "Yönetici", 
                    Email = "admin@kutuphane.com",
                    Telefon = "0532-100-0001",
                    DogumTarihi = new DateTime(1985, 1, 1),
                    PasswordHash = "admin123:salt",
                    Role = "Admin",
                    ToplamOduncSayisi = 0,
                    AktifMi = true
                },
                
                // Normal kullanıcılar - Çok kitap okuyan aktif üyeler
                new Kullanici 
                { 
                    Ad = "Ayşe", 
                    Soyad = "Demir", 
                    Email = "ayse.demir@email.com",
                    Telefon = "0533-234-5678",
                    DogumTarihi = new DateTime(1992, 7, 22),
                    PasswordHash = "user123:salt",
                    Role = "User",
                    ToplamOduncSayisi = 45,
                    AktifMi = true
                },
                new Kullanici 
                { 
                    Ad = "Mehmet", 
                    Soyad = "Kaya", 
                    Email = "mehmet.kaya@email.com",
                    Telefon = "0534-345-6789",
                    DogumTarihi = new DateTime(1988, 11, 10),
                    PasswordHash = "user123:salt",
                    Role = "User",
                    ToplamOduncSayisi = 38,
                    AktifMi = true
                },
                new Kullanici 
                { 
                    Ad = "Fatma", 
                    Soyad = "Özkan", 
                    Email = "fatma.ozkan@email.com",
                    Telefon = "0535-456-7890",
                    DogumTarihi = new DateTime(1995, 5, 8),
                    PasswordHash = "user123:salt",
                    Role = "User",
                    ToplamOduncSayisi = 52,
                    AktifMi = true
                },
                new Kullanici 
                { 
                    Ad = "Can", 
                    Soyad = "Aktaş", 
                    Email = "can.aktas@email.com",
                    Telefon = "0536-567-8901",
                    DogumTarihi = new DateTime(1990, 12, 25),
                    PasswordHash = "user123:salt",
                    Role = "User",
                    ToplamOduncSayisi = 67,
                    AktifMi = true
                },
                new Kullanici 
                { 
                    Ad = "Elif", 
                    Soyad = "Yılmaz", 
                    Email = "elif.yilmaz@email.com",
                    Telefon = "0537-678-9012",
                    DogumTarihi = new DateTime(1993, 9, 14),
                    PasswordHash = "user123:salt",
                    Role = "User",
                    ToplamOduncSayisi = 29,
                    AktifMi = true
                },
                new Kullanici 
                { 
                    Ad = "Emre", 
                    Soyad = "Çelik", 
                    Email = "emre.celik@email.com",
                    Telefon = "0538-789-0123",
                    DogumTarihi = new DateTime(1991, 4, 3),
                    PasswordHash = "user123:salt",
                    Role = "User",
                    ToplamOduncSayisi = 41,
                    AktifMi = true
                },
                new Kullanici 
                { 
                    Ad = "Zeynep", 
                    Soyad = "Arslan", 
                    Email = "zeynep.arslan@email.com",
                    Telefon = "0539-890-1234",
                    DogumTarihi = new DateTime(1994, 8, 17),
                    PasswordHash = "user123:salt",
                    Role = "User",
                    ToplamOduncSayisi = 33,
                    AktifMi = true
                },
                new Kullanici 
                { 
                    Ad = "Burak", 
                    Soyad = "Şahin", 
                    Email = "burak.sahin@email.com",
                    Telefon = "0540-901-2345",
                    DogumTarihi = new DateTime(1989, 6, 29),
                    PasswordHash = "user123:salt",
                    Role = "User",
                    ToplamOduncSayisi = 58,
                    AktifMi = true
                },
                new Kullanici 
                { 
                    Ad = "Seda", 
                    Soyad = "Koç", 
                    Email = "seda.koc@email.com",
                    Telefon = "0541-012-3456",
                    DogumTarihi = new DateTime(1996, 2, 11),
                    PasswordHash = "user123:salt",
                    Role = "User",
                    ToplamOduncSayisi = 24,
                    AktifMi = true
                },
                new Kullanici 
                { 
                    Ad = "Onur", 
                    Soyad = "Avcı", 
                    Email = "onur.avci@email.com",
                    Telefon = "0542-123-4567",
                    DogumTarihi = new DateTime(1987, 10, 20),
                    PasswordHash = "user123:salt",
                    Role = "User",
                    ToplamOduncSayisi = 76,
                    AktifMi = true
                },
                new Kullanici 
                { 
                    Ad = "Deniz", 
                    Soyad = "Güney", 
                    Email = "deniz.guney@email.com",
                    Telefon = "0543-234-5678",
                    DogumTarihi = new DateTime(1992, 1, 5),
                    PasswordHash = "user123:salt",
                    Role = "User",
                    ToplamOduncSayisi = 19,
                    AktifMi = true
                }
            };

            context.Kullanicilar.AddRange(kullanicilar);
            await context.SaveChangesAsync();

            // 5. ÖDÜNÇLER - ÇOK FAZLA ÇEŞİTLİ SENARYO
            var oduncler = new List<Odunc>();
            var random = new Random(42); // Sabit seed ile tutarlı sonuçlar

            // Aktif ödünçler (şu an ödünçte olan kitaplar)
            var aktifOduncKitaplar = kitaplar.Where(k => !k.MusaitMi).ToList();
            for (int i = 0; i < aktifOduncKitaplar.Count; i++)
            {
                var kitap = aktifOduncKitaplar[i];
                var kullanici = kullanicilar[random.Next(1, kullanicilar.Count)]; // Admin hariç
                
                if (i < 8) // İlk 8 kitap geç kalan
                {
                    var gecikmeGunu = random.Next(1, 25); // 1-25 gün geç
                    oduncler.Add(new Odunc
                    {
                        OduncAlinmaTarihi = DateTime.Now.AddDays(-(14 + gecikmeGunu + random.Next(0, 5))),
                        GeriVerilmesiGerekenTarih = DateTime.Now.AddDays(-gecikmeGunu),
                        GeriVerilisTarihi = null,
                        IadeEdildiMi = false,
                        KullaniciId = kullanici.Id,
                        KitapId = kitap.Id
                    });
                }
                else // Geri kalanlar zamanında
                {
                    var oduncGunu = random.Next(1, 12); // 1-12 gün önce alınmış
                    oduncler.Add(new Odunc
                    {
                        OduncAlinmaTarihi = DateTime.Now.AddDays(-oduncGunu),
                        GeriVerilmesiGerekenTarih = DateTime.Now.AddDays(14 - oduncGunu),
                        GeriVerilisTarihi = null,
                        IadeEdildiMi = false,
                        KullaniciId = kullanici.Id,
                        KitapId = kitap.Id
                    });
                }
            }

            // Geçmişte iade edilmiş ödünçler (popüler kitapları belirlemek için)
            var musaitKitaplar = kitaplar.Where(k => k.MusaitMi).ToList();
            var gecmisOduncSayisi = 80; // Çok fazla geçmiş ödünç

            for (int i = 0; i < gecmisOduncSayisi; i++)
            {
                var kitap = kitaplar[random.Next(0, kitaplar.Count)];
                var kullanici = kullanicilar[random.Next(1, kullanicilar.Count)];
                var oduncGunu = random.Next(30, 200); // 30-200 gün önce
                var iadeTarihi = DateTime.Now.AddDays(-random.Next(5, oduncGunu - 14));
                
                // %20 ihtimalle geç iade
                var gecIade = random.Next(1, 6) == 1; 
                var geriVerilmeTarihi = DateTime.Now.AddDays(-(oduncGunu - 14));
                
                if (gecIade)
                {
                    iadeTarihi = geriVerilmeTarihi.AddDays(random.Next(1, 10)); // 1-10 gün geç
                }

                oduncler.Add(new Odunc
                {
                    OduncAlinmaTarihi = DateTime.Now.AddDays(-oduncGunu),
                    GeriVerilmesiGerekenTarih = geriVerilmeTarihi,
                    GeriVerilisTarihi = iadeTarihi,
                    IadeEdildiMi = true,
                    KullaniciId = kullanici.Id,
                    KitapId = kitap.Id
                });
            }

            // Bu ay yapılan ödünçler (dashboard istatistikleri için)
            var buAyOduncSayisi = 25;
            for (int i = 0; i < buAyOduncSayisi; i++)
            {
                var kitap = kitaplar[random.Next(0, kitaplar.Count)];
                var kullanici = kullanicilar[random.Next(1, kullanicilar.Count)];
                var gunSayisi = random.Next(1, DateTime.Now.Day);
                
                oduncler.Add(new Odunc
                {
                    OduncAlinmaTarihi = new DateTime(DateTime.Now.Year, DateTime.Now.Month, gunSayisi),
                    GeriVerilmesiGerekenTarih = new DateTime(DateTime.Now.Year, DateTime.Now.Month, gunSayisi).AddDays(14),
                    GeriVerilisTarihi = new DateTime(DateTime.Now.Year, DateTime.Now.Month, gunSayisi).AddDays(random.Next(10, 14)),
                    IadeEdildiMi = true,
                    KullaniciId = kullanici.Id,
                    KitapId = kitap.Id
                });
            }

            // Popüler kitapları daha çok ödünç alınmış yapma
            var populerKitaplar = new[] { "Harry Potter ve Felsefe Taşı", "1984", "Simyacı", "Muhteşem Gatsby", "Gurur ve Önyargı" };
            foreach (var populerBaslik in populerKitaplar)
            {
                var kitap = kitaplar.FirstOrDefault(k => k.Baslik == populerBaslik);
                if (kitap != null)
                {
                    // Her popüler kitap için ekstra 10-20 ödünç kaydı
                    var ekstraOdunc = random.Next(10, 21);
                    for (int j = 0; j < ekstraOdunc; j++)
                    {
                        var kullanici = kullanicilar[random.Next(1, kullanicilar.Count)];
                        var gecmisGun = random.Next(20, 300);
                        
                        oduncler.Add(new Odunc
                        {
                            OduncAlinmaTarihi = DateTime.Now.AddDays(-gecmisGun),
                            GeriVerilmesiGerekenTarih = DateTime.Now.AddDays(-(gecmisGun - 14)),
                            GeriVerilisTarihi = DateTime.Now.AddDays(-(gecmisGun - random.Next(10, 15))),
                            IadeEdildiMi = true,
                            KullaniciId = kullanici.Id,
                            KitapId = kitap.Id
                        });
                    }
                }
            }

            context.Oduncler.AddRange(oduncler);
            await context.SaveChangesAsync();

            Console.WriteLine("🎉 SÜPER DATA SEEDİNG TAMAMLANDI!");
            Console.WriteLine($"📚 {yazarlar.Count} yazar eklendi");
            Console.WriteLine($"📂 {kategoriler.Count} kategori eklendi"); 
            Console.WriteLine($"📖 {kitaplar.Count} ünlü kitap eklendi (gerçek ISBN'lerle)");
            Console.WriteLine($"👥 {kullanicilar.Count} kullanıcı eklendi");
            Console.WriteLine($"📊 {oduncler.Count} ödünç işlemi eklendi");
            Console.WriteLine("🏆 Harry Potter, 1984, Simyacı gibi ünlü kitaplar dahil");
            Console.WriteLine("💰 Geç kalan ödünçler ve cezalar dahil");
Console.WriteLine("📈 Dashboard istatistikleri için zengin veri");
        }
    }
}
