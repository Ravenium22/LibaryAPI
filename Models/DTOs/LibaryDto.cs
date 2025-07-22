namespace Kutuphane.Models.DTOs
{
    public class LibraryCreateDto
    {
        public string İsim { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public int KitapSayisi { get; set; }
        public int UyeSayisi { get; set; }
    }

    public class LibraryResponseDto
    {
        public int Id { get; set; }
        public string İsim { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public int KitapSayisi { get; set; }
        public int UyeSayisi { get; set; }
        public List<KitapResponseDto> Kitaplar { get; set; } = new();
    }
}