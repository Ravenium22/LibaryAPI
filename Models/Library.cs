namespace Kutuphane.Models
{
    public class Library
    {
        public int Id { get; set; }
        public string İsim { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public int KitapSayisi { get; set; }
        public int UyeSayisi { get; set; }
        public List<Kitap> Kitaplar { get; set; } = new();
    }
}