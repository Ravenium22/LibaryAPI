namespace Kutuphane.Models
{
    public class Kitap
    {
        public int Id { get; set; }
        public string Baslik { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateTime YayinTarihi { get; set; }
        public int SayfaSayisi { get; set; }
        public bool MusaitMi { get; set; } = true;
        
        public int YazarId { get; set; }
        public int KategoriId { get; set; }
        public int LibraryId { get; set; }  
        
        public Yazar? Yazar { get; set; }
        public Kategori? Kategori { get; set; }
        public Library? Library { get; set; } 
        public List<Odunc> Oduncler { get; set; } = new();
    }
}