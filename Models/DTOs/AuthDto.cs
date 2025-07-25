using System.ComponentModel.DataAnnotations;

namespace Kutuphane.Models.DTOs
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email alanı gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi girin.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

    }

    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Ad boş bırakılamaz.")]
        public string Ad { get; set; } = string.Empty;
        [Required(ErrorMessage = "Soyad boş bırakılamaz.")]
        public string Soyad { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email alanı gereklidir.")]
        [EmailAddress(ErrorMessage = "Email boş bırakılamaz.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Doğum tarihi boş bırakılamaz.")]
        [DataType(DataType.Date)]
        public DateTime DogumTarihi { get; set; }
        [Required(ErrorMessage = "Telefon boş bırakılamaz.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası girin.")]

        public string Telefon { get; set; } = string.Empty;
    }

    public class RegisterResponseDto
    {
        
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}