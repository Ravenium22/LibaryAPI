using Microsoft.EntityFrameworkCore;
using Kutuphane.Data;
using Kutuphane.Models;
using Kutuphane.Repositories.Interfaces;

namespace Kutuphane.Repositories
{
    public class KategoriRepository : IKategoriRepository
    {
        private readonly AppDbContext _context;

        public KategoriRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Kategori>> GetAllAsync()
        {
            return await _context.Kategoriler.ToListAsync();
        }

        public async Task<Kategori?> GetByIdAsync(int id)
        {
            return await _context.Kategoriler.FindAsync(id);
        }

        public async Task<Kategori> AddAsync(Kategori entity)
        {
            _context.Kategoriler.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Kategori entity)
        {
            _context.Kategoriler.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var kategori = await GetByIdAsync(id);
            if (kategori != null)
            {
                _context.Kategoriler.Remove(kategori);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Kategoriler.AnyAsync(k => k.Id == id);
        }

        // Özel metodlar
        public async Task<IEnumerable<Kategori>> GetKategorilerWithKitaplarAsync()
        {
            return await _context.Kategoriler
                .Include(k => k.Kitaplar)
                .ToListAsync();
        }

        public async Task<Kategori?> GetKategoriByNameAsync(string name)
        {
            return await _context.Kategoriler
                .FirstOrDefaultAsync(k => k.Ad == name);  // Ad property'si
        }
    }
}