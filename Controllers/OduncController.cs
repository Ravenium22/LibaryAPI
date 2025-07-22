using Microsoft.AspNetCore.Mvc;
using Kutuphane.Models;
using Kutuphane.Models.DTOs;
using Kutuphane.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace Kutuphane.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OduncController : ControllerBase
    {
        private readonly IOduncRepository _oduncRepository;
        private readonly ILogger<OduncController> _logger;

        public OduncController(IOduncRepository oduncRepository, ILogger<OduncController> logger)
        {
            _oduncRepository = oduncRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OduncResponseDto>>> GetAllOduncler()
        {
            _logger.LogInformation("Tüm ödünçler listeleniyor");

            try
            {
                var oduncler = await _oduncRepository.GetAllAsync();
                _logger.LogInformation("{Count} ödünç bulundu", oduncler.Count());

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ödünçler listelenirken hata oluştu");
                return StatusCode(500, "Ödünçler listelenirken hata oluştu");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OduncResponseDto>> GetOdunc(int id)
        {
            _logger.LogInformation("Ödünç getiriliyor: ID {Id}", id);

            try
            {
                var odunc = await _oduncRepository.GetByIdAsync(id);
                if (odunc == null)
                {
                    _logger.LogWarning("Ödünç bulunamadı: ID {Id}", id);
                    return NotFound();
                }

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

                _logger.LogInformation("Ödünç başarıyla getirildi: ID {Id}, Kullanıcı: {KullaniciId}, Kitap: {KitapId}", odunc.Id, odunc.KullaniciId, odunc.KitapId);
                return Ok(oduncDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ödünç getirilirken hata: ID {Id}", id);
                return StatusCode(500, "Ödünç getirilirken hata oluştu");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OduncResponseDto>> CreateOdunc(OduncCreateDto oduncCreateDto)
        {
            _logger.LogInformation("Yeni ödünç ekleniyor: Kullanıcı {KullaniciId}, Kitap {KitapId}", oduncCreateDto.KullaniciId, oduncCreateDto.KitapId);

            try
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
                _logger.LogInformation("Ödünç başarıyla eklendi: ID {Id}, Kullanıcı: {KullaniciId}, Kitap: {KitapId}", createdOdunc.Id, createdOdunc.KullaniciId, createdOdunc.KitapId);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ödünç eklenirken hata: Kullanıcı {KullaniciId}, Kitap {KitapId}", oduncCreateDto.KullaniciId, oduncCreateDto.KitapId);
                return StatusCode(500, "Ödünç eklenirken hata oluştu");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOdunc(int id, OduncCreateDto oduncUpdateDto)
        {
            _logger.LogInformation("Ödünç güncelleniyor: ID {Id}", id);

            try
            {
                var existingOdunc = await _oduncRepository.GetByIdAsync(id);
                if (existingOdunc == null)
                {
                    _logger.LogWarning("Güncellenecek ödünç bulunamadı: ID {Id}", id);
                    return NotFound();
                }

                existingOdunc.OduncTarihi = oduncUpdateDto.OduncTarihi;
                existingOdunc.TeslimTarihi = oduncUpdateDto.TeslimTarihi;
                existingOdunc.IadeTarihi = oduncUpdateDto.IadeTarihi;
                existingOdunc.IadeEdildiMi = oduncUpdateDto.IadeEdildiMi;
                existingOdunc.KitapId = oduncUpdateDto.KitapId;
                existingOdunc.KullaniciId = oduncUpdateDto.KullaniciId;

                await _oduncRepository.UpdateAsync(existingOdunc);
                _logger.LogInformation("Ödünç başarıyla güncellendi: ID {Id}, İade Edildi: {IadeEdildiMi}", id, oduncUpdateDto.IadeEdildiMi);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ödünç güncellenirken hata: ID {Id}", id);
                return StatusCode(500, "Ödünç güncellenirken hata oluştu");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOdunc(int id)
        {
            _logger.LogInformation("Ödünç siliniyor: ID {Id}", id);

            try
            {
                var odunc = await _oduncRepository.GetByIdAsync(id);
                if (odunc == null)
                {
                    _logger.LogWarning("Silinecek ödünç bulunamadı: ID {Id}", id);
                    return NotFound();
                }

                await _oduncRepository.DeleteAsync(id);
                _logger.LogInformation("Ödünç başarıyla silindi: ID {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ödünç silinirken hata: ID {Id}", id);
                return StatusCode(500, "Ödünç silinirken hata oluştu");
            }
        }

        [HttpGet("aktif")]
        public async Task<ActionResult<IEnumerable<OduncResponseDto>>> GetAktifOduncler()
        {
            _logger.LogInformation("Aktif ödünçler getiriliyor");

            try
            {
                var oduncler = await _oduncRepository.GetAktifOdunclerAsync();
                _logger.LogInformation("{Count} aktif ödünç bulundu", oduncler.Count());

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Aktif ödünçler getirilemedi");
                return StatusCode(500, "Aktif ödünçler getirilemedi");
            }
        }

        [HttpGet("suresi-dolan")]
        public async Task<ActionResult<IEnumerable<OduncResponseDto>>> GetSuresiDolanOduncler()
        {
            _logger.LogInformation("Süresi dolan ödünçler getiriliyor");

            try
            {
                var oduncler = await _oduncRepository.GetSuresiDolanOdunclerAsync();
                _logger.LogInformation("{Count} süresi dolan ödünç bulundu", oduncler.Count());

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Süresi dolan ödünçler getirilemedi");
                return StatusCode(500, "Süresi dolan ödünçler getirilemedi");
            }
        }

        [HttpGet("kullanici/{kullaniciId}")]
        public async Task<ActionResult<IEnumerable<OduncResponseDto>>> GetKullaniciOduncler(int kullaniciId)
        {
            _logger.LogInformation("Kullanıcı ödünçleri getiriliyor: Kullanıcı ID {KullaniciId}", kullaniciId);

            try
            {
                var oduncler = await _oduncRepository.GetKullaniciOdunclerAsync(kullaniciId);
                _logger.LogInformation("Kullanıcı {KullaniciId} için {Count} ödünç bulundu", kullaniciId, oduncler.Count());

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı ödünçleri getirilemedi: Kullanıcı ID {KullaniciId}", kullaniciId);
                return StatusCode(500, "Kullanıcı ödünçleri getirilemedi");
            }
        }

        [HttpGet("kitap/{kitapId}/gecmis")]
        public async Task<ActionResult<IEnumerable<OduncResponseDto>>> GetKitapOduncGecmisi(int kitapId)
        {
            _logger.LogInformation("Kitap ödünç geçmişi getiriliyor: Kitap ID {KitapId}", kitapId);

            try
            {
                var oduncler = await _oduncRepository.GetKitapOduncGecmisiAsync(kitapId);
                _logger.LogInformation("Kitap {KitapId} için {Count} ödünç geçmişi bulundu", kitapId, oduncler.Count());

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kitap ödünç geçmişi getirilemedi: Kitap ID {KitapId}", kitapId);
                return StatusCode(500, "Kitap ödünç geçmişi getirilemedi");
            }
        }
    }
}