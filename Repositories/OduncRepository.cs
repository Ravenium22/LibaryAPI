using Microsoft.EntityFrameworkCore;
using Kutuphane.Data;
using Kutuphane.Models;
using Kutuphane.Repositories.Interfaces;

namespace Kutuphane.Repositories
{
    public class OduncRepository : IOduncRepository
    {
        private readonly AppDbContext _context;

        public OduncRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Odunc>> GetAllAsync()
        {
            return await _context.Oduncler
            .Include(o => o.Kitap)
            .ThenInclude(k => k.Yazar)
            .Include(o => o.Kullanici)
            .ToListAsync();        }

        public async Task<Odunc?> GetByIdAsync(int id)
        {
            return await _context.Oduncler
            .Include(o => o.Kitap)
            .ThenInclude(k => k.Yazar)
            .Include(o => o.Kullanici)
            .FirstOrDefaultAsync(o => o.Id == id);        }

        public async Task<Odunc> AddAsync(Odunc entity)
        {
            _context.Oduncler.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Odunc entity)
        {
            _context.Oduncler.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var odunc = await GetByIdAsync(id);
            if (odunc != null)
            {
                _context.Oduncler.Remove(odunc);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Oduncler.AnyAsync(y => y.Id == id);
        }

        // Özel metodlar
        public async Task<IEnumerable<Odunc>> GetAktifOdunclerAsync()
        {
        return await _context.Oduncler
        .Include(o => o.Kitap)
            .ThenInclude(k => k.Yazar)
        .Include(o => o.Kullanici)
        .Where(o => o.IadeEdildiMi == false)
        .ToListAsync();
        }

        public async Task<IEnumerable<Odunc>> GetSuresiDolanOdunclerAsync()
        {
        return await _context.Oduncler
        .Include(o => o.Kitap)
            .ThenInclude(k => k.Yazar)
        .Include(o => o.Kullanici)
        .Where(o => o.GeriVerilmesiGerekenTarih < DateTime.Now)
        .Where(o => o.IadeEdildiMi == false)
        .ToListAsync();
        }  

        public async Task<IEnumerable<Odunc>> GetKullaniciOdunclerAsync(int kullaniciId){
         return await _context.Oduncler
        .Include(o => o.Kitap)
            .ThenInclude(k => k.Yazar)
        .Include(o => o.Kullanici)
        .Where(o => o.KullaniciId == kullaniciId)
        .ToListAsync();
        }
        public async Task<IEnumerable<Odunc>> GetKitapOduncGecmisiAsync(int kitapId) {
         return await _context.Oduncler
        .Include(o => o.Kitap)
            .ThenInclude(k => k.Yazar)
        .Include(o => o.Kullanici)
        .Where(o => o.KitapId == kitapId)
        .ToListAsync();
        }
    }
}