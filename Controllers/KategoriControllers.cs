using Microsoft.AspNetCore.Mvc;
using Kutuphane.Models;
using Kutuphane.Models.DTOs;
using Kutuphane.Repositories.Interfaces;

namespace Kutuphane.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KategoriController : ControllerBase
    {
        private readonly IKategoriRepository _kategoriRepository;

        public KategoriController(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KategoriResponseDto>>> GetAllKategoriler()
        {
            var kategoriler = await _kategoriRepository.GetAllAsync();
            var kategoriDtos = kategoriler.Select(k => new KategoriResponseDto
            {
                Id = k.Id,
                Ad = k.Ad,
                Aciklama = k.Aciklama
            });
            return Ok(kategoriDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KategoriResponseDto>> GetKategori(int id)
        {
            var kategori = await _kategoriRepository.GetByIdAsync(id);
            if (kategori == null) return NotFound();
            
            var kategoriDto = new KategoriResponseDto
            {
                Id = kategori.Id,
                Ad = kategori.Ad,
                Aciklama = kategori.Aciklama
            };
            return Ok(kategoriDto);
        }

        [HttpPost]
        public async Task<ActionResult<KategoriResponseDto>> CreateKategori(KategoriCreateDto kategoriCreateDto)
        {
            var kategori = new Kategori
            {
                Ad = kategoriCreateDto.Ad,
                Aciklama = kategoriCreateDto.Aciklama
            };
            
            var createdKategori = await _kategoriRepository.AddAsync(kategori);
            
            var responseDto = new KategoriResponseDto
            {
                Id = createdKategori.Id,
                Ad = createdKategori.Ad,
                Aciklama = createdKategori.Aciklama
            };
            
            return CreatedAtAction(nameof(GetKategori), new { id = createdKategori.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKategori(int id, KategoriCreateDto kategoriUpdateDto)
        {
            var existingKategori = await _kategoriRepository.GetByIdAsync(id);
            if (existingKategori == null) return NotFound();
            
            existingKategori.Ad = kategoriUpdateDto.Ad;
            existingKategori.Aciklama = kategoriUpdateDto.Aciklama;
            
            await _kategoriRepository.UpdateAsync(existingKategori);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKategori(int id)
        {
            await _kategoriRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("with-kitaplar")]
        public async Task<ActionResult<IEnumerable<KategoriResponseDto>>> GetKategorilerWithKitaplar()
        {
            var kategoriler = await _kategoriRepository.GetKategorilerWithKitaplarAsync();
            var kategoriDtos = kategoriler.Select(k => new KategoriResponseDto
            {
                Id = k.Id,
                Ad = k.Ad,
                Aciklama = k.Aciklama
            });
            return Ok(kategoriDtos);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<KategoriResponseDto>> GetKategoriByName(string name)
        {
            var kategori = await _kategoriRepository.GetKategoriByNameAsync(name);
            if (kategori == null) return NotFound();
            
            var kategoriDto = new KategoriResponseDto
            {
                Id = kategori.Id,
                Ad = kategori.Ad,
                Aciklama = kategori.Aciklama
            };
            return Ok(kategoriDto);
        }
    }
}