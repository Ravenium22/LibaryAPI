using System.ComponentModel.DataAnnotations;

namespace Kutuphane.Models.DTOs
{
    public class KullaniciCreateDto
    {
        [Required(ErrorMessage = "Ad zorunludur.")]

        public string Ad { get; set; } = string.Empty;
        [Required(ErrorMessage = "Soyad zorunludur.")]
        public string Soyad { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi girin.")]
        [Required(ErrorMessage = "Email zorunludur.")]
        public string Email { get; set; } = string.Empty;
        [Phone(ErrorMessage = "Geçerli bir telefon numarası girin.")]
        [Required(ErrorMessage = "Telefon zorunludur.")]
        public string Telefon { get; set; } = string.Empty;
        [Required(ErrorMessage = "Doğum tarihi zorunludur.")]
        [DataType(DataType.Date)]
        public DateTime DogumTarihi { get; set; }
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

    public class KullaniciRegisterDto
    {
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public DateTime DogumTarihi { get; set; }    }
}