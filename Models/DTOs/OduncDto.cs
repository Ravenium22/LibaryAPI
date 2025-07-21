namespace Kutuphane.Models.DTOs
{
    public class OduncCreateDto
    {
        public DateTime OduncTarihi { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public DateTime IadeTarihi { get; set; }
        public bool IadeEdildiMi { get; set; } = false;
        public int KitapId { get; set; }
        public int KullaniciId { get; set; }
    }

    public class OduncResponseDto
    {
        public int Id { get; set; }
        public DateTime OduncTarihi { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public DateTime IadeTarihi { get; set; }
        public bool IadeEdildiMi { get; set; }
        public int KitapId { get; set; }
        public int KullaniciId { get; set; }
    }
}