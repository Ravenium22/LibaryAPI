using Microsoft.EntityFrameworkCore;
using Kutuphane.Data;
using Kutuphane.Models;
using Kutuphane.Repositories.Interfaces;
#nullable enable
namespace Kutuphane.Repositories
{
    public class YazarRepository : IYazarRepository
    {
        private readonly AppDbContext _context;

        public YazarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Yazar>> GetAllAsync()
        {
            return await _context.Yazarlar.ToListAsync();
        }

        public async Task<Yazar?> GetByIdAsync(int id)
        {
            return await _context.Yazarlar.FindAsync(id);
        }

        public async Task<Yazar> AddAsync(Yazar entity)
        {
            _context.Yazarlar.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Yazar entity)
        {
            _context.Yazarlar.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var yazar = await GetByIdAsync(id);
            if (yazar != null)
            {
                _context.Yazarlar.Remove(yazar);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Yazarlar.AnyAsync(y => y.Id == id);
        }

        // Özel metodlar
        public async Task<IEnumerable<Yazar>> GetYazarlarWithKitaplarAsync()
        {
            return await _context.Yazarlar
                .Include(y => y.Kitaplar)  // Kitapları da getir
                .ToListAsync();
        }

        public async Task<IEnumerable<Yazar>> GetYazarlarByUlkeAsync(string ulke)
        {
            return await _context.Yazarlar
                .Where(y => y.Ulke == ulke)
                .ToListAsync();
        }
        public async Task<IEnumerable<Yazar>> YazarSearchAsync(string query)
        {
            return await _context.Yazarlar
                .Include(k => k.Kitaplar)
                .Where(k => k.Ad.ToLower().Contains(query) || k.Soyad.ToLower().Contains(query))
                .ToListAsync();
        
        }
    }
}