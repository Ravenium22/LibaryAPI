using Kutuphane.Models;
using static OduncResponseDto;
#nullable enable
namespace Kutuphane.Repositories.Interfaces
{
    public interface IOduncRepository : IRepository<Odunc>
    {
        Task<IEnumerable<Odunc>> GetAktifOdunclerAsync();
        Task<IEnumerable<Odunc>> GetSuresiDolanOdunclerAsync();
        Task<IEnumerable<Odunc>> GetKullaniciOdunclerAsync(int kullaniciId);
        Task<IEnumerable<Odunc>> GetKitapOduncGecmisiAsync(int kitapId);
        Task<IEnumerable<BorcRaporuResponseDto>> GetBorcRaporuAsync(
        decimal? minBorç, 
        decimal? maxBorç, 
        int? limit, 
        string? sıralama
        );
    }
    
}