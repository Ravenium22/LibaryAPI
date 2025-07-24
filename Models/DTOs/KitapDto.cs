using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Kutuphane.Models.DTOs
{
    public class KitapCreateDto


    {
        [Required(ErrorMessage = "Baslik zorunludur.")]
        public string Baslik { get; set; } = string.Empty;
        [Required(ErrorMessage = "ISBN zorunlu.")]
        
        public string ISBN { get; set; } = string.Empty;
        [Required(ErrorMessage = "Yayın tarihi zorunludur.")]
        [DataType(DataType.Date)]
        public DateTime YayinTarihi { get; set; }
        [Required(ErrorMessage = "Sayfa sayısı zorunludur.")]
        [Range(0.01, 9999999999999999, ErrorMessage = "Sayfa sayısı pozitif bir sayı olmalıdır.")]
        public int SayfaSayisi { get; set; }
        
        public bool MusaitMi { get; set; } = true;
        [Required(ErrorMessage = "Yazar ID zorunludur.")]

        public int YazarId { get; set; }
        [Required(ErrorMessage = "Kategori ID zorunludur.")]
        public int KategoriId { get; set; }
        [Required(ErrorMessage = "Konum zorunludur(Hangi katta olduğunu yazmanız gerekiyor)")]
        public string Konum { get; set; } = string.Empty;
        [Required(ErrorMessage = "Raf numarası zorunludur.")]
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