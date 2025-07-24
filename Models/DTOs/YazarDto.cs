using System.ComponentModel.DataAnnotations;

namespace Kutuphane.Models.DTOs
{
    public class YazarCreateDto
    {
        [Required(ErrorMessage = "Yazar adı zorunludur.")]

        public string Ad { get; set; } = string.Empty;
        [Required(ErrorMessage = "Yazar soyadı zorunludur.(Yazar soyadı bilinmiyorsa . koyup geçebilirsiniz)")]
        public string Soyad { get; set; } = string.Empty;

        public DateTime DogumTarihi { get; set; }
        [Required(ErrorMessage = "Yazar ülke bilgisi zorunludur.")]
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