using Microsoft.AspNetCore.Mvc;
using Kutuphane.Models;
using Kutuphane.Models.DTOs;
using Kutuphane.Repositories.Interfaces;

namespace Kutuphane.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YazarController : ControllerBase
    {
        private readonly IYazarRepository _yazarRepository;

        public YazarController(IYazarRepository yazarRepository)
        {
            _yazarRepository = yazarRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<YazarResponseDto>>> GetAllYazarlar()
        {
            var yazarlar = await _yazarRepository.GetAllAsync();
            var yazarDtos = yazarlar.Select(y => new YazarResponseDto
            {
                Id = y.Id,
                Ad = y.Ad,
                Soyad = y.Soyad,
                DogumTarihi = y.DogumTarihi,
                Ulke = y.Ulke
            });
            return Ok(yazarDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<YazarResponseDto>> GetYazar(int id)
        {
            var yazar = await _yazarRepository.GetByIdAsync(id);
            if (yazar == null) return NotFound();
            
            var yazarDto = new YazarResponseDto
            {
                Id = yazar.Id,
                Ad = yazar.Ad,
                Soyad = yazar.Soyad,
                DogumTarihi = yazar.DogumTarihi,
                Ulke = yazar.Ulke
            };
            return Ok(yazarDto);
        }

        [HttpPost]
        public async Task<ActionResult<YazarResponseDto>> CreateYazar(YazarCreateDto yazarCreateDto)
        {
            var yazar = new Yazar
            {
                Ad = yazarCreateDto.Ad,
                Soyad = yazarCreateDto.Soyad,
                DogumTarihi = yazarCreateDto.DogumTarihi,
                Ulke = yazarCreateDto.Ulke
            };
            
            var createdYazar = await _yazarRepository.AddAsync(yazar);
            
            var responseDto = new YazarResponseDto
            {
                Id = createdYazar.Id,
                Ad = createdYazar.Ad,
                Soyad = createdYazar.Soyad,
                DogumTarihi = createdYazar.DogumTarihi,
                Ulke = createdYazar.Ulke
            };
            
            return CreatedAtAction(nameof(GetYazar), new { id = createdYazar.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateYazar(int id, YazarCreateDto yazarUpdateDto)
        {
            var existingYazar = await _yazarRepository.GetByIdAsync(id);
            if (existingYazar == null) return NotFound();
            
            existingYazar.Ad = yazarUpdateDto.Ad;
            existingYazar.Soyad = yazarUpdateDto.Soyad;
            existingYazar.DogumTarihi = yazarUpdateDto.DogumTarihi;
            existingYazar.Ulke = yazarUpdateDto.Ulke;
            
            await _yazarRepository.UpdateAsync(existingYazar);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYazar(int id)
        {
            await _yazarRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("with-kitaplar")]
        public async Task<ActionResult<IEnumerable<YazarResponseDto>>> GetYazarlarWithKitaplar()
        {
            var yazarlar = await _yazarRepository.GetYazarlarWithKitaplarAsync();
            var yazarDtos = yazarlar.Select(y => new YazarResponseDto
            {
                Id = y.Id,
                Ad = y.Ad,
                Soyad = y.Soyad,
                DogumTarihi = y.DogumTarihi,
                Ulke = y.Ulke
            });
            return Ok(yazarDtos);
        }

        [HttpGet("ulke/{ulke}")]
        public async Task<ActionResult<IEnumerable<YazarResponseDto>>> GetYazarlarByUlke(string ulke)
        {
            var yazarlar = await _yazarRepository.GetYazarlarByUlkeAsync(ulke);
            var yazarDtos = yazarlar.Select(y => new YazarResponseDto
            {
                Id = y.Id,
                Ad = y.Ad,
                Soyad = y.Soyad,
                DogumTarihi = y.DogumTarihi,
                Ulke = y.Ulke
            });
            return Ok(yazarDtos);
        }
    }
}