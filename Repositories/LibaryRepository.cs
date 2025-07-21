using Microsoft.EntityFrameworkCore;
using Kutuphane.Data;
using Kutuphane.Models;
using Kutuphane.Repositories.Interfaces;

namespace Kutuphane.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly AppDbContext _context;

        public LibraryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Library>> GetAllAsync()
        {
            return await _context.Libraries.ToListAsync();
        }

        public async Task<Library?> GetByIdAsync(int id)
        {
            return await _context.Libraries.FindAsync(id);
        }

        public async Task<Library> AddAsync(Library entity)
        {
            _context.Libraries.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Library entity)
        {
            _context.Libraries.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var library = await GetByIdAsync(id);
            if (library != null)
            {
                _context.Libraries.Remove(library);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Libraries.AnyAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Library>> GetLibrariesWithKitaplarAsync()
        {
            return await _context.Libraries
                .Include(l => l.Kitaplar)
                .ToListAsync();
        }

        public async Task<IEnumerable<Library>> GetLibrariesWithEnCokKullanicilar()
        {
            return await _context.Libraries
                .Where(l => l.UyeSayisi > 50)
                .ToListAsync();
        }
    }
}