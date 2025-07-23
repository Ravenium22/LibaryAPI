public class OduncCreateDto
{
    public DateTime OduncAlinmaTarihi { get; set; }
    public DateTime GeriVerilmesiGerekenTarih { get; set; }
    public DateTime? GeriVerilisTarihi { get; set; }
    public bool IadeEdildiMi { get; set; } = false;
    public int KitapId { get; set; }
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
}