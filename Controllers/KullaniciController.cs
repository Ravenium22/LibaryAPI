using Microsoft.AspNetCore.Mvc;
using Kutuphane.Models;
using Kutuphane.Models.DTOs;
using Kutuphane.Repositories.Interfaces;

namespace Kutuphane.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciRepository _kullaniciRepository;

        public KullaniciController(IKullaniciRepository kullaniciRepository)
        {
            _kullaniciRepository = kullaniciRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KullaniciResponseDto>>> GetAllKullanicilar()
        {
            var kullanicilar = await _kullaniciRepository.GetAllAsync();
            var kullaniciDtos = kullanicilar.Select(k => new KullaniciResponseDto
            {
                Id = k.Id,
                Ad = k.Ad,
                Soyad = k.Soyad,
                Email = k.Email,
                Telefon = k.Telefon,
                DogumTarihi = k.DogumTarihi,
                ToplamOduncSayisi = k.ToplamOduncSayisi,
                AktifMi = k.AktifMi
            });
            return Ok(kullaniciDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KullaniciResponseDto>> GetKullanici(int id)
        {
            var kullanici = await _kullaniciRepository.GetByIdAsync(id);
            if (kullanici == null) return NotFound();
            
            var kullaniciDto = new KullaniciResponseDto
            {
                Id = kullanici.Id,
                Ad = kullanici.Ad,
                Soyad = kullanici.Soyad,
                Email = kullanici.Email,
                Telefon = kullanici.Telefon,
                DogumTarihi = kullanici.DogumTarihi,
                ToplamOduncSayisi = kullanici.ToplamOduncSayisi,
                AktifMi = kullanici.AktifMi
            };
            return Ok(kullaniciDto);
        }

        [HttpPost]
        public async Task<ActionResult<KullaniciResponseDto>> CreateKullanici(KullaniciCreateDto kullaniciCreateDto)
        {
            var kullanici = new Kullanici
            {
                Ad = kullaniciCreateDto.Ad,
                Soyad = kullaniciCreateDto.Soyad,
                Email = kullaniciCreateDto.Email,
                Telefon = kullaniciCreateDto.Telefon,
                DogumTarihi = kullaniciCreateDto.DogumTarihi,
                ToplamOduncSayisi = kullaniciCreateDto.ToplamOduncSayisi,
                AktifMi = kullaniciCreateDto.AktifMi
            };
            
            var createdKullanici = await _kullaniciRepository.AddAsync(kullanici);
            
            var responseDto = new KullaniciResponseDto
            {
                Id = createdKullanici.Id,
                Ad = createdKullanici.Ad,
                Soyad = createdKullanici.Soyad,
                Email = createdKullanici.Email,
                Telefon = createdKullanici.Telefon,
                DogumTarihi = createdKullanici.DogumTarihi,
                ToplamOduncSayisi = createdKullanici.ToplamOduncSayisi,
                AktifMi = createdKullanici.AktifMi
            };
            
            return CreatedAtAction(nameof(GetKullanici), new { id = createdKullanici.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKullanici(int id, KullaniciCreateDto kullaniciUpdateDto)
        {
            var existingKullanici = await _kullaniciRepository.GetByIdAsync(id);
            if (existingKullanici == null) return NotFound();
            
            existingKullanici.Ad = kullaniciUpdateDto.Ad;
            existingKullanici.Soyad = kullaniciUpdateDto.Soyad;
            existingKullanici.Email = kullaniciUpdateDto.Email;
            existingKullanici.Telefon = kullaniciUpdateDto.Telefon;
            existingKullanici.DogumTarihi = kullaniciUpdateDto.DogumTarihi;
            existingKullanici.ToplamOduncSayisi = kullaniciUpdateDto.ToplamOduncSayisi;
            existingKullanici.AktifMi = kullaniciUpdateDto.AktifMi;
            
            await _kullaniciRepository.UpdateAsync(existingKullanici);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKullanici(int id)
        {
            await _kullaniciRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("with-oduncler")]
        public async Task<ActionResult<IEnumerable<KullaniciResponseDto>>> GetKullanicilarWithOduncler()
        {
            var kullanicilar = await _kullaniciRepository.GetKullanicilarWithOdunclerAsync();
            var kullaniciDtos = kullanicilar.Select(k => new KullaniciResponseDto
            {
                Id = k.Id,
                Ad = k.Ad,
                Soyad = k.Soyad,
                Email = k.Email,
                Telefon = k.Telefon,
                DogumTarihi = k.DogumTarihi,
                ToplamOduncSayisi = k.ToplamOduncSayisi,
                AktifMi = k.AktifMi
            });
            return Ok(kullaniciDtos);
        }

        [HttpGet("aktif")]
        public async Task<ActionResult<IEnumerable<KullaniciResponseDto>>> GetAktifKullanicilar()
        {
            var kullanicilar = await _kullaniciRepository.GetAktifKullanicilarAsync();
            var kullaniciDtos = kullanicilar.Select(k => new KullaniciResponseDto
            {
                Id = k.Id,
                Ad = k.Ad,
                Soyad = k.Soyad,
                Email = k.Email,
                Telefon = k.Telefon,
                DogumTarihi = k.DogumTarihi,
                ToplamOduncSayisi = k.ToplamOduncSayisi,
                AktifMi = k.AktifMi
            });
            return Ok(kullaniciDtos);
        }
    }
}