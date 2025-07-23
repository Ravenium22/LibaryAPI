namespace Kutuphane.Models.DTOs
{
    public class KitapCreateDto
    {
        public string Baslik { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateTime YayinTarihi { get; set; }
        public int SayfaSayisi { get; set; }
        public bool MusaitMi { get; set; } = true;
        public int YazarId { get; set; }
        public int KategoriId { get; set; }
        public string Konum { get; set; } = string.Empty;
        public string RafNo { get; set; } = string.Empty;
    }

    public class KitapResponseDto
    {
        public int Id { get; set; }
        public string Baslik { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateTime YayinTarihi { get; set; }
        public int SayfaSayisi { get; set; }
        public bool MusaitMi { get; set; }
        public int YazarId { get; set; }
        public int KategoriId { get; set; }
        public string Konum { get; set; } = string.Empty;
        public string RafNo { get; set; } = string.Empty;
        }
}