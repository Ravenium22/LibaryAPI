namespace Kutuphane.Models.DTOs
{
    public class KullaniciCreateDto
    {
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public DateTime DogumTarihi { get; set; }
        public int ToplamOduncSayisi { get; set; }
        public bool AktifMi { get; set; } = true;
    }

    public class KullaniciResponseDto
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public DateTime DogumTarihi { get; set; }
        public int ToplamOduncSayisi { get; set; }
        public bool AktifMi { get; set; }
        public List<OduncResponseDto> Oduncler { get; set; } = new();
    }
}