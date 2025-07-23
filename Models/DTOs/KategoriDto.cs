namespace Kutuphane.Models.DTOs
{
    public class KategoriCreateDto
    {
        public string Ad { get; set; } = string.Empty;
        public string Aciklama { get; set; } = string.Empty;
    }

    // GET 
    public class KategoriResponseDto  
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Aciklama { get; set; } = string.Empty;
        public List<KitapResponseDto> Kitaplar { get; set; } = new();
    }
}