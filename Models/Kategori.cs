namespace Kutuphane.Models
{
    public class Kategori
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Aciklama { get; set; } = string.Empty;

        public List<Kitap> Kitaplar { get; set; } = new();
    }
}