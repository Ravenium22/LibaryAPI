using Microsoft.AspNetCore.Mvc;
using Kutuphane.Models;
using Kutuphane.Models.DTOs;
using Kutuphane.Repositories.Interfaces;

namespace Kutuphane.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OduncController : ControllerBase
    {
        private readonly IOduncRepository _oduncRepository;

        public OduncController(IOduncRepository oduncRepository)
        {
            _oduncRepository = oduncRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OduncResponseDto>>> GetAllOduncler()
        {
            var oduncler = await _oduncRepository.GetAllAsync();
            var oduncDtos = oduncler.Select(o => new OduncResponseDto
            {
                Id = o.Id,
                OduncTarihi = o.OduncTarihi,
                TeslimTarihi = o.TeslimTarihi,
                IadeTarihi = o.IadeTarihi,
                IadeEdildiMi = o.IadeEdildiMi,
                KitapId = o.KitapId,
                KullaniciId = o.KullaniciId
            });
            return Ok(oduncDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OduncResponseDto>> GetOdunc(int id)
        {
            var odunc = await _oduncRepository.GetByIdAsync(id);
            if (odunc == null) return NotFound();
            
            var oduncDto = new OduncResponseDto
            {
                Id = odunc.Id,
                OduncTarihi = odunc.OduncTarihi,
                TeslimTarihi = odunc.TeslimTarihi,
                IadeTarihi = odunc.IadeTarihi,
                IadeEdildiMi = odunc.IadeEdildiMi,
                KitapId = odunc.KitapId,
                KullaniciId = odunc.KullaniciId
            };
            return Ok(oduncDto);
        }

        [HttpPost]
        public async Task<ActionResult<OduncResponseDto>> CreateOdunc(OduncCreateDto oduncCreateDto)
        {
            var odunc = new Odunc
            {
                OduncTarihi = oduncCreateDto.OduncTarihi,
                TeslimTarihi = oduncCreateDto.TeslimTarihi,
                IadeTarihi = oduncCreateDto.IadeTarihi,
                IadeEdildiMi = oduncCreateDto.IadeEdildiMi,
                KitapId = oduncCreateDto.KitapId,
                KullaniciId = oduncCreateDto.KullaniciId
            };
            
            var createdOdunc = await _oduncRepository.AddAsync(odunc);
            
            var responseDto = new OduncResponseDto
            {
                Id = createdOdunc.Id,
                OduncTarihi = createdOdunc.OduncTarihi,
                TeslimTarihi = createdOdunc.TeslimTarihi,
                IadeTarihi = createdOdunc.IadeTarihi,
                IadeEdildiMi = createdOdunc.IadeEdildiMi,
                KitapId = createdOdunc.KitapId,
                KullaniciId = createdOdunc.KullaniciId
            };
            
            return CreatedAtAction(nameof(GetOdunc), new { id = createdOdunc.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOdunc(int id, OduncCreateDto oduncUpdateDto)
        {
            var existingOdunc = await _oduncRepository.GetByIdAsync(id);
            if (existingOdunc == null) return NotFound();
            
            existingOdunc.OduncTarihi = oduncUpdateDto.OduncTarihi;
            existingOdunc.TeslimTarihi = oduncUpdateDto.TeslimTarihi;
            existingOdunc.IadeTarihi = oduncUpdateDto.IadeTarihi;
            existingOdunc.IadeEdildiMi = oduncUpdateDto.IadeEdildiMi;
            existingOdunc.KitapId = oduncUpdateDto.KitapId;
            existingOdunc.KullaniciId = oduncUpdateDto.KullaniciId;
            
            await _oduncRepository.UpdateAsync(existingOdunc);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOdunc(int id)
        {
            await _oduncRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("aktif")]
        public async Task<ActionResult<IEnumerable<OduncResponseDto>>> GetAktifOduncler()
        {
            var oduncler = await _oduncRepository.GetAktifOdunclerAsync();
            var oduncDtos = oduncler.Select(o => new OduncResponseDto
            {
                Id = o.Id,
                OduncTarihi = o.OduncTarihi,
                TeslimTarihi = o.TeslimTarihi,
                IadeTarihi = o.IadeTarihi,
                IadeEdildiMi = o.IadeEdildiMi,
                KitapId = o.KitapId,
                KullaniciId = o.KullaniciId
            });
            return Ok(oduncDtos);
        }

        [HttpGet("suresi-dolan")]
        public async Task<ActionResult<IEnumerable<OduncResponseDto>>> GetSuresiDolanOduncler()
        {
            var oduncler = await _oduncRepository.GetSuresiDolanOdunclerAsync();
            var oduncDtos = oduncler.Select(o => new OduncResponseDto
            {
                Id = o.Id,
                OduncTarihi = o.OduncTarihi,
                TeslimTarihi = o.TeslimTarihi,
                IadeTarihi = o.IadeTarihi,
                IadeEdildiMi = o.IadeEdildiMi,
                KitapId = o.KitapId,
                KullaniciId = o.KullaniciId
            });
            return Ok(oduncDtos);
        }

        [HttpGet("kullanici/{kullaniciId}")]
        public async Task<ActionResult<IEnumerable<OduncResponseDto>>> GetKullaniciOduncler(int kullaniciId)
        {
            var oduncler = await _oduncRepository.GetKullaniciOdunclerAsync(kullaniciId);
            var oduncDtos = oduncler.Select(o => new OduncResponseDto
            {
                Id = o.Id,
                OduncTarihi = o.OduncTarihi,
                TeslimTarihi = o.TeslimTarihi,
                IadeTarihi = o.IadeTarihi,
                IadeEdildiMi = o.IadeEdildiMi,
                KitapId = o.KitapId,
                KullaniciId = o.KullaniciId
            });
            return Ok(oduncDtos);
        }

        [HttpGet("kitap/{kitapId}/gecmis")]
        public async Task<ActionResult<IEnumerable<OduncResponseDto>>> GetKitapOduncGecmisi(int kitapId)
        {
            var oduncler = await _oduncRepository.GetKitapOduncGecmisiAsync(kitapId);
            var oduncDtos = oduncler.Select(o => new OduncResponseDto
            {
                Id = o.Id,
                OduncTarihi = o.OduncTarihi,
                TeslimTarihi = o.TeslimTarihi,
                IadeTarihi = o.IadeTarihi,
                IadeEdildiMi = o.IadeEdildiMi,
                KitapId = o.KitapId,
                KullaniciId = o.KullaniciId
            });
            return Ok(oduncDtos);
        }
    }
}