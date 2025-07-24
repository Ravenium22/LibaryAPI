using Kutuphane.Models;

namespace Kutuphane.Repositories.Interfaces
{
    public interface IYazarRepository : IRepository<Yazar>
    {
        Task<IEnumerable<Yazar>> GetYazarlarWithKitaplarAsync();  // Yazarları kitaplarıyla birlikte getir
        Task<IEnumerable<Yazar>> GetYazarlarByUlkeAsync(string ulke);

        Task<IEnumerable<Yazar>> YazarSearchAsync(string query);

    }
}