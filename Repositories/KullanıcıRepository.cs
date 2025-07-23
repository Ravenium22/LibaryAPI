using Microsoft.EntityFrameworkCore;
using Kutuphane.Data;
using Kutuphane.Models;
using Kutuphane.Repositories.Interfaces;

namespace Kutuphane.Repositories
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly AppDbContext _context;

        public KullaniciRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Kullanici>> GetAllAsync()
        {
            return await _context.Kullanicilar.ToListAsync();
        }

        public async Task<Kullanici?> GetByIdAsync(int id)
        {
            return await _context.Kullanicilar.FindAsync(id);
        }

        public async Task<Kullanici> AddAsync(Kullanici entity)
        {
            _context.Kullanicilar.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Kullanici entity)
        {
            _context.Kullanicilar.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var kullanici = await GetByIdAsync(id);
            if (kullanici != null)
            {
                _context.Kullanicilar.Remove(kullanici);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Kullanicilar.AnyAsync(k => k.Id == id);
        }

        // Özel metodlar
        public async Task<IEnumerable<Kullanici>> GetKullanicilarWithOdunclerAsync()
        {
            return await _context.Kullanicilar
            .Include(k => k.Oduncler)
            .ThenInclude(o => o.Kitap)
                .ThenInclude(kitap => kitap.Yazar)
                .ToListAsync();
        }

        public async Task<IEnumerable<Kullanici>> GetAktifKullanicilarAsync()
        {
            return await _context.Kullanicilar
                .Where(k => k.AktifMi == true)
                .ToListAsync();
        }
    }
}
