using Kutuphane.Models;

namespace Kutuphane.Repositories.Interfaces
{
    public interface ILibraryRepository : IRepository<Library>
    {
        Task<IEnumerable<Library>> GetLibrariesWithKitaplarAsync();
        Task<IEnumerable<Library>> GetLibrariesWithEnCokKullanicilar();
    }
}