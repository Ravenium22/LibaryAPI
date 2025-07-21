namespace Kutuphane.Models
{
    public class Yazar
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public DateTime DogumTarihi { get; set; }
        public string Ulke { get; set; } = string.Empty;

        public List<Kitap> Kitaplar { get; set; } = new();
    }
}