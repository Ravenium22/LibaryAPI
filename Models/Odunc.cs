namespace Kutuphane.Models
{
    public class Odunc
    {
        public int Id { get; set; }
        public DateTime OduncAlinmaTarihi { get; set; }
        public DateTime GeriVerilmesiGerekenTarih { get; set; }
        public DateTime? GeriVerilisTarihi { get; set; }
        public bool IadeEdildiMi { get; set; } = false;

        public int KullaniciId { get; set; }
        public int KitapId { get; set; }

        public Kitap Kitap { get; set; }
        public Kullanici Kullanici { get; set; } = null!;

        public int GecikmeGunSayisi
        {
            get
            {
                if (IadeEdildiMi) return 0;
                var bugun = DateTime.Now.Date;
                var gecikmeTarihi = GeriVerilmesiGerekenTarih.Date;
                return bugun > gecikmeTarihi ? (bugun - gecikmeTarihi).Days : 0;
            }
        }

        public decimal GecikmeCezasi => GecikmeGunSayisi * 50m;

        public string Durumu
        {
            get
            {
                if (IadeEdildiMi) return "İade Edildi";
                if (GecikmeGunSayisi > 0) return "Gecikmiş";
                return "Zamanında";
            }
        }
    }
}