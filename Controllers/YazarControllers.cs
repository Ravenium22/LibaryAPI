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
    public class YazarController : ControllerBase
    {
        private readonly IYazarRepository _yazarRepository;
        private readonly ILogger<YazarController> _logger;

        public YazarController(IYazarRepository yazarRepository, ILogger<YazarController> logger)
        {
            _yazarRepository = yazarRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<YazarResponseDto>>> GetAllYazarlar()
        {
            _logger.LogInformation("Tüm yazarlar listeleniyor");

            try
            {
                var yazarlar = await _yazarRepository.GetAllAsync();
                _logger.LogInformation("{Count} yazar bulundu", yazarlar.Count());

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yazarlar listelenirken hata oluştu");
                return StatusCode(500, "Yazarlar listelenirken hata oluştu");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<YazarResponseDto>> GetYazar(int id)
        {
            _logger.LogInformation("Yazar getiriliyor: ID {Id}", id);

            try
            {
                var yazar = await _yazarRepository.GetByIdAsync(id);
                if (yazar == null)
                {
                    _logger.LogWarning("Yazar bulunamadı: ID {Id}", id);
                    return NotFound();
                }

                var yazarDto = new YazarResponseDto
                {
                    Id = yazar.Id,
                    Ad = yazar.Ad,
                    Soyad = yazar.Soyad,
                    DogumTarihi = yazar.DogumTarihi,
                    Ulke = yazar.Ulke
                };

                _logger.LogInformation("Yazar başarıyla getirildi: ID {Id}, Ad: {Ad} {Soyad}", yazar.Id, yazar.Ad, yazar.Soyad);
                return Ok(yazarDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yazar getirilirken hata: ID {Id}", id);
                return StatusCode(500, "Yazar getirilirken hata oluştu");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<YazarResponseDto>> CreateYazar(YazarCreateDto yazarCreateDto)
        {
            _logger.LogInformation("Yeni yazar ekleniyor: {YazarAd} {YazarSoyad}", yazarCreateDto.Ad, yazarCreateDto.Soyad);

            try
            {
                var yazar = new Yazar
                {
                    Ad = yazarCreateDto.Ad,
                    Soyad = yazarCreateDto.Soyad,
                    DogumTarihi = yazarCreateDto.DogumTarihi,
                    Ulke = yazarCreateDto.Ulke
                };

                var createdYazar = await _yazarRepository.AddAsync(yazar);
                _logger.LogInformation("Yazar başarıyla eklendi: ID {Id}, Ad: {Ad} {Soyad}", createdYazar.Id, createdYazar.Ad, createdYazar.Soyad);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yazar eklenirken hata: {YazarAd} {YazarSoyad}", yazarCreateDto.Ad, yazarCreateDto.Soyad);
                return StatusCode(500, "Yazar eklenirken hata oluştu");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateYazar(int id, YazarCreateDto yazarUpdateDto)
        {
            _logger.LogInformation("Yazar güncelleniyor: ID {Id}", id);

            try
            {
                var existingYazar = await _yazarRepository.GetByIdAsync(id);
                if (existingYazar == null)
                {
                    _logger.LogWarning("Güncellenecek yazar bulunamadı: ID {Id}", id);
                    return NotFound();
                }

                existingYazar.Ad = yazarUpdateDto.Ad;
                existingYazar.Soyad = yazarUpdateDto.Soyad;
                existingYazar.DogumTarihi = yazarUpdateDto.DogumTarihi;
                existingYazar.Ulke = yazarUpdateDto.Ulke;

                await _yazarRepository.UpdateAsync(existingYazar);
                _logger.LogInformation("Yazar başarıyla güncellendi: ID {Id}, Yeni Ad: {Ad} {Soyad}", id, yazarUpdateDto.Ad, yazarUpdateDto.Soyad);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yazar güncellenirken hata: ID {Id}", id);
                return StatusCode(500, "Yazar güncellenirken hata oluştu");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteYazar(int id)
        {
            _logger.LogInformation("Yazar siliniyor: ID {Id}", id);

            try
            {
                var yazar = await _yazarRepository.GetByIdAsync(id);
                if (yazar == null)
                {
                    _logger.LogWarning("Silinecek yazar bulunamadı: ID {Id}", id);
                    return NotFound();
                }

                await _yazarRepository.DeleteAsync(id);
                _logger.LogInformation("Yazar başarıyla silindi: ID {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yazar silinirken hata: ID {Id}", id);
                return StatusCode(500, "Yazar silinirken hata oluştu");
            }
        }

        [HttpGet("with-kitaplar")]
        public async Task<ActionResult<IEnumerable<YazarWithKitaplarDto>>> GetYazarlarWithKitaplar()
        {
            _logger.LogInformation("Yazarlar kitaplarıyla birlikte getiriliyor");

            try
            {
                var yazarlar = await _yazarRepository.GetYazarlarWithKitaplarAsync();
                _logger.LogInformation("Kitaplarıyla birlikte {Count} yazar bulundu", yazarlar.Count());

                var yazarDtos = yazarlar.Select(y => new YazarWithKitaplarDto
                {
                    Id = y.Id,
                    Ad = y.Ad,
                    Soyad = y.Soyad,
                    DogumTarihi = y.DogumTarihi,
                    Ulke = y.Ulke,
                    Kitaplar = y.Kitaplar.Select(k => new KitapResponseDto
                    {
                        Id = k.Id,
                        Baslik = k.Baslik,
                        ISBN = k.ISBN,
                        YayinTarihi = k.YayinTarihi,
                        SayfaSayisi = k.SayfaSayisi,
                        MusaitMi = k.MusaitMi,
                        YazarId = k.YazarId,
                        KategoriId = k.KategoriId,
                        Konum = k.Konum,
                        RafNo = k.RafNo
                    }).ToList()
                });
                return Ok(yazarDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yazarlar kitaplarıyla getirilemedi");
                return StatusCode(500, "Yazarlar kitaplarıyla getirilemedi");
            }
        }

        [HttpGet("ulke/{ulke}")]
        public async Task<ActionResult<IEnumerable<YazarResponseDto>>> GetYazarlarByUlke(string ulke)
        {
            _logger.LogInformation("Ülkeye göre yazarlar getiriliyor: {Ulke}", ulke);

            try
            {
                var yazarlar = await _yazarRepository.GetYazarlarByUlkeAsync(ulke);
                _logger.LogInformation("{Ulke} ülkesinden {Count} yazar bulundu", ulke, yazarlar.Count());

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ülkeye göre yazarlar getirilemedi: {Ulke}", ulke);
                return StatusCode(500, "Ülkeye göre yazarlar getirilemedi");
            }
        }
        [HttpGet("search")]
public async Task<ActionResult<IEnumerable<YazarResponseDto>>> SearchYazar(string query)
{
    _logger.LogInformation("Yazar arama : {Query}", query);
    
    try
    {
        var yazarlar = await _yazarRepository.YazarSearchAsync(query);
        
        if (yazarlar == null || !yazarlar.Any())
        {
            _logger.LogWarning("Arama sonucu bulunamadı: {Query}", query);
            return NotFound("Arama sonucu bulunamadı");
        }

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
    catch (Exception ex)
    {
        _logger.LogError(ex, "Yazar arama sırasında hata oluştu: {Query}", query);
        return StatusCode(500, "Yazar arama sırasında hata oluştu");
    }
}
                
    }
}