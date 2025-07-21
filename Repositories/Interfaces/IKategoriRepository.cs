using Kutuphane.Models;

namespace Kutuphane.Repositories.Interfaces
{
    public interface IKategoriRepository : IRepository<Kategori>
    {
        Task<IEnumerable<Kategori>> GetKategorilerWithKitaplarAsync();  // Kategorileri kitaplarıyla birlikte getir
        Task<Kategori?> GetKategoriByNameAsync(string name);  // Kategoriyi isme göre getir
    }
}