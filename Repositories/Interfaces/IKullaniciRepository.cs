using Kutuphane.Models;

namespace Kutuphane.Repositories.Interfaces
{
    public interface IKullaniciRepository : IRepository<Kullanici>
    {
        Task<IEnumerable<Kullanici>> GetKullanicilarWithOdunclerAsync();  // Kullanıcıları ödünçlerle birlikte getir
        Task<IEnumerable<Kullanici>> GetAktifKullanicilarAsync();         // Aktif kullanıcıları getir
    }
}