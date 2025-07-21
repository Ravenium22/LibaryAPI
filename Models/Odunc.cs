namespace Kutuphane.Models
{
    public class Odunc
    {
     public int Id { get; set; }
        public DateTime OduncTarihi { get; set; }

        public DateTime TeslimTarihi { get; set; }
        public DateTime IadeTarihi { get; set; }
        public bool IadeEdildiMi { get; set; } = false;

        public int KullaniciId { get; set; }
        public int KitapId { get; set; }

        public Kitap Kitap { get; set; }
        public Kullanici Kullanici { get; set; } = null!;

    }
}