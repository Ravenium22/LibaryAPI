

using System.ComponentModel.DataAnnotations;

namespace Kutuphane.Models.DTOs
{
public class DashboardResponseDto
{
    public int ToplamKitapSayisi { get; set; }
    public int ToplamKullaniciSayisi { get; set; }
    public int AktifOduncSayisi { get; set; }
    public int ToplamKategoriSayisi { get; set; }
    public int ToplamYazarSayisi { get; set; }
    public int ToplamGecikenIadeSayisi { get; set; }
    public int ToplamOduncSayisi { get; set; }
    public int ToplamCezaGeliri { get; set; }
    public int BuAyOduncSayisi { get; set; }
    public int MusaitKitapSayisi { get; set; }

    public List<PopulerKitapInfo> PopulerKitaplarinfo { get; set; } = new();
    public List<AylikTrend> AylikTrendler { get; set; } = new();
}

    public class PopulerKitapInfo
    {
        public string KitapAdi { get; set; } = string.Empty;
        public int OduncSayisi { get; set; }
        public string YazarAdi { get; set; } = string.Empty;
        public string KategoriAdi { get; set; } = string.Empty;
        public bool IsMusait { get; set; } = true;

        public string KapakUrl { get; set; } = string.Empty;
}

public class AylikTrend
{
    public string KategoriAdi { get; set; } = string.Empty;
    public int KitapSayisi { get; set; }
    public int OduncSayisi { get; set; }
    public string Ay { get; set; } = string.Empty;
}

public class PublicStatsResponseDto
{
    public int ToplamKitapSayisi { get; set; }
    public int ToplamKategoriSayisi { get; set; }
    public int ToplamYazarSayisi { get; set; }
    public int MusaitKitapSayisi { get; set; }
    public List<PopulerKitapInfo> PopulerKitaplar { get; set; } = new();
}

    public class AdminFinancialResponseDto
    {
        public int ToplamGecikenIadeSayisi { get; set; }
        public int ToplamCezaGeliri { get; set; }
        public int AktifOduncSayisi { get; set; }
        public int ToplamOduncSayisi { get; set; }
        public int BuAyOduncSayisi { get; set; }
    }
}