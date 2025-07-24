using System.ComponentModel.DataAnnotations;

#nullable enable

public class OduncCreateDto


{
    [Required(ErrorMessage = "Ödünç alınma tarihi zorunludur.")]
    public DateTime OduncAlinmaTarihi { get; set; }
    [Required(ErrorMessage = "Geri verilmesi gereken tarih zorunludur.")]
    public DateTime GeriVerilmesiGerekenTarih { get; set; }
    public DateTime? GeriVerilisTarihi { get; set; }

    public bool IadeEdildiMi { get; set; } = false;
    [Required(ErrorMessage = "Kitap ID Zorunludur.")]
    public int KitapId { get; set; }
    [Required(ErrorMessage = "Kullanıcı ID zorunludur")]
    public int KullaniciId { get; set; }
}

public class OduncResponseDto
{
    public int Id { get; set; }
    public DateTime OduncAlinmaTarihi { get; set; }
    public DateTime GeriVerilmesiGerekenTarih { get; set; }
    public DateTime? GeriVerilisTarihi { get; set; }
    public bool IadeEdildiMi { get; set; }
    public int KitapId { get; set; }
    public int KullaniciId { get; set; }

    public string KitapBaslik { get; set; } = string.Empty;
    public string YazarAdSoyad { get; set; } = string.Empty;
    public string KullaniciAdSoyad { get; set; } = string.Empty;

    public int GecikmeGunSayisi { get; set; }
    public decimal GecikmeCezasi { get; set; }
    public string Durumu { get; set; } = string.Empty;
    public class BorcRaporuFilterDto
    {
        public decimal? MinBorç { get; set; }
        public decimal? MaxBorç { get; set; }
        public int? Limit { get; set; }
        public string? Sıralama { get; set; } = "borç";
    }
public class BorcRaporuResponseDto
{
    public int KullaniciId { get; set; }
    public string KullaniciAdSoyad { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<GecikmisKitapDetayDto> GecikmiKitaplar { get; set; } = new();
    public decimal ToplamBorç { get; set; }
    public int ToplamGecikmiKitap { get; set; }
}

    public class GecikmisKitapDetayDto
    {
        public string KitapAd { get; set; } = string.Empty;
        public int GecikmeGun { get; set; }
        public decimal Ceza { get; set; }
    }

}