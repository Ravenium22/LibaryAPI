using Kutuphane.Models;

namespace Kutuphane.Repositories.Interfaces
{
    public interface IKitapRepository : IRepository<Kitap>
    {
        Task<IEnumerable<Kitap>> GetKitaplarByYazarAsync(int yazarId);
        Task<IEnumerable<Kitap>> GetMusaitKitaplarAsync();

        Task<IEnumerable<Kitap>> GetMesgulKitaplarAsync();

        Task<IEnumerable<Kitap>> KitapSearchAsync(string searchTerm);
    }
}