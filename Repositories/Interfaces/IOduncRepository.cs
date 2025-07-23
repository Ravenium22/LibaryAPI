using Kutuphane.Models;

namespace Kutuphane.Repositories.Interfaces
{
    public interface IOduncRepository : IRepository<Odunc>  // Odunc için!
    {
        Task<IEnumerable<Odunc>> GetAktifOdunclerAsync();              // Henüz iade edilmemiş
        Task<IEnumerable<Odunc>> GetSuresiDolanOdunclerAsync();        // Süresi dolmuş
        Task<IEnumerable<Odunc>> GetKullaniciOdunclerAsync(int kullaniciId);  // Belirli kullanıcının ödünçleri
        Task<IEnumerable<Odunc>> GetKitapOduncGecmisiAsync(int kitapId);
    }
}