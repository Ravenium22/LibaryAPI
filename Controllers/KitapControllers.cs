using Microsoft.AspNetCore.Mvc;
using Kutuphane.Models;
using Kutuphane.Models.DTOs;
using Kutuphane.Repositories.Interfaces;

namespace Kutuphane.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KitapController : ControllerBase
    {
        private readonly IKitapRepository _kitapRepository;

        public KitapController(IKitapRepository kitapRepository)
        {
            _kitapRepository = kitapRepository;
        }

        // GET: api/kitap
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KitapResponseDto>>> GetAllKitaplar()
        {
            var kitaplar = await _kitapRepository.GetAllAsync();
            var kitapDtos = kitaplar.Select(k => new KitapResponseDto
            {
                Id = k.Id,
                Baslik = k.Baslik,
                ISBN = k.ISBN,
                YayinTarihi = k.YayinTarihi,
                SayfaSayisi = k.SayfaSayisi,
                MusaitMi = k.MusaitMi,
                YazarId = k.YazarId,
                KategoriId = k.KategoriId,
                LibraryId = k.LibraryId
            });
            return Ok(kitapDtos);
        }

        // GET: api/kitap/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KitapResponseDto>> GetKitap(int id)
        {
            var kitap = await _kitapRepository.GetByIdAsync(id);
            
            if (kitap == null)
                return NotFound();
            
            var kitapDto = new KitapResponseDto
            {
                Id = kitap.Id,
                Baslik = kitap.Baslik,
                ISBN = kitap.ISBN,
                YayinTarihi = kitap.YayinTarihi,
                SayfaSayisi = kitap.SayfaSayisi,
                MusaitMi = kitap.MusaitMi,
                YazarId = kitap.YazarId,
                KategoriId = kitap.KategoriId,
                LibraryId = kitap.LibraryId
            };
                
            return Ok(kitapDto);
        }

        // POST: api/kitap
        [HttpPost]
        public async Task<ActionResult<KitapResponseDto>> CreateKitap(KitapCreateDto kitapCreateDto)
        {
            var kitap = new Kitap
            {
                Baslik = kitapCreateDto.Baslik,
                ISBN = kitapCreateDto.ISBN,
                YayinTarihi = kitapCreateDto.YayinTarihi,
                SayfaSayisi = kitapCreateDto.SayfaSayisi,
                MusaitMi = kitapCreateDto.MusaitMi,
                YazarId = kitapCreateDto.YazarId,
                KategoriId = kitapCreateDto.KategoriId,
                LibraryId = kitapCreateDto.LibraryId
            };
            
            var createdKitap = await _kitapRepository.AddAsync(kitap);
            
            var responseDto = new KitapResponseDto
            {
                Id = createdKitap.Id,
                Baslik = createdKitap.Baslik,
                ISBN = createdKitap.ISBN,
                YayinTarihi = createdKitap.YayinTarihi,
                SayfaSayisi = createdKitap.SayfaSayisi,
                MusaitMi = createdKitap.MusaitMi,
                YazarId = createdKitap.YazarId,
                KategoriId = createdKitap.KategoriId,
                LibraryId = createdKitap.LibraryId
            };
            
            return CreatedAtAction(nameof(GetKitap), new { id = createdKitap.Id }, responseDto);
        }

        // PUT: api/kitap/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKitap(int id, KitapCreateDto kitapUpdateDto)
        {
            var existingKitap = await _kitapRepository.GetByIdAsync(id);
            if (existingKitap == null)
                return NotFound();

            existingKitap.Baslik = kitapUpdateDto.Baslik;
            existingKitap.ISBN = kitapUpdateDto.ISBN;
            existingKitap.YayinTarihi = kitapUpdateDto.YayinTarihi;
            existingKitap.SayfaSayisi = kitapUpdateDto.SayfaSayisi;
            existingKitap.MusaitMi = kitapUpdateDto.MusaitMi;
            existingKitap.YazarId = kitapUpdateDto.YazarId;
            existingKitap.KategoriId = kitapUpdateDto.KategoriId;
            existingKitap.LibraryId = kitapUpdateDto.LibraryId;

            await _kitapRepository.UpdateAsync(existingKitap);
            return NoContent();
        }

        // DELETE: api/kitap/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKitap(int id)
        {
            await _kitapRepository.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/kitap/musait
        [HttpGet("musait")]
        public async Task<ActionResult<IEnumerable<KitapResponseDto>>> GetMusaitKitaplar()
        {
            var kitaplar = await _kitapRepository.GetMusaitKitaplarAsync();
            var kitapDtos = kitaplar.Select(k => new KitapResponseDto
            {
                Id = k.Id,
                Baslik = k.Baslik,
                ISBN = k.ISBN,
                YayinTarihi = k.YayinTarihi,
                SayfaSayisi = k.SayfaSayisi,
                MusaitMi = k.MusaitMi,
                YazarId = k.YazarId,
                KategoriId = k.KategoriId,
                LibraryId = k.LibraryId
            });
            return Ok(kitapDtos);
        }
    }
}