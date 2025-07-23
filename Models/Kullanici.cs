using Microsoft.AspNetCore.Identity;

namespace Kutuphane.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public DateTime DogumTarihi { get; set; }

        public int ToplamOduncSayisi { get; set; }
        public bool AktifMi { get; set; } = true;

        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public List<Odunc> Oduncler { get; set; } = new();
    }
}