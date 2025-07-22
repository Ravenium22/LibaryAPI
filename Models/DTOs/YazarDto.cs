namespace Kutuphane.Models.DTOs
{
    public class YazarCreateDto
    {
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public DateTime DogumTarihi { get; set; }
        public string Ulke { get; set; } = string.Empty;
    }

    public class YazarResponseDto
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public DateTime DogumTarihi { get; set; }
        public string Ulke { get; set; } = string.Empty;
    }

    public class YazarWithKitaplarDto
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public DateTime DogumTarihi { get; set; }
        public string Ulke { get; set; } = string.Empty;
        public List<KitapResponseDto> Kitaplar { get; set; } = new();
    }
}