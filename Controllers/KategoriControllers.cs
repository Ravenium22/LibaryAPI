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
    public class KategoriController : ControllerBase
    {
        private readonly IKategoriRepository _kategoriRepository;
        private readonly ILogger<KategoriController> _logger;

        public KategoriController(IKategoriRepository kategoriRepository, ILogger<KategoriController> logger)
        {
            _kategoriRepository = kategoriRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KategoriResponseDto>>> GetAllKategoriler()
        {
            _logger.LogInformation("Tüm kategoriler listeleniyor");

            try
            {
                var kategoriler = await _kategoriRepository.GetAllAsync();
                _logger.LogInformation("{Count} kategori bulundu", kategoriler.Count());

                var kategoriDtos = kategoriler.Select(k => new KategoriResponseDto
                {
                    Id = k.Id,
                    Ad = k.Ad,
                    Aciklama = k.Aciklama
                });
                return Ok(kategoriDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategoriler listelenirken hata oluştu");
                return StatusCode(500, "Kategoriler listelenirken hata oluştu");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KategoriResponseDto>> GetKategori(int id)
        {
            _logger.LogInformation("Kategori getiriliyor: ID {Id}", id);

            try
            {
                var kategori = await _kategoriRepository.GetByIdAsync(id);
                if (kategori == null)
                {
                    _logger.LogWarning("Kategori bulunamadı: ID {Id}", id);
                    return NotFound();
                }

                var kategoriDto = new KategoriResponseDto
                {
                    Id = kategori.Id,
                    Ad = kategori.Ad,
                    Aciklama = kategori.Aciklama
                };

                _logger.LogInformation("Kategori başarıyla getirildi: ID {Id}, Ad: {Ad}", kategori.Id, kategori.Ad);
                return Ok(kategoriDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori getirilirken hata: ID {Id}", id);
                return StatusCode(500, "Kategori getirilirken hata oluştu");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<KategoriResponseDto>> CreateKategori(KategoriCreateDto kategoriCreateDto)
        {
            _logger.LogInformation("Yeni kategori ekleniyor: {KategoriAd}", kategoriCreateDto.Ad);

            try
            {
                var kategori = new Kategori
                {
                    Ad = kategoriCreateDto.Ad,
                    Aciklama = kategoriCreateDto.Aciklama
                };

                var createdKategori = await _kategoriRepository.AddAsync(kategori);
                _logger.LogInformation("Kategori başarıyla eklendi: ID {Id}, Ad: {Ad}", createdKategori.Id, createdKategori.Ad);

                var responseDto = new KategoriResponseDto
                {
                    Id = createdKategori.Id,
                    Ad = createdKategori.Ad,
                    Aciklama = createdKategori.Aciklama
                };

                return CreatedAtAction(nameof(GetKategori), new { id = createdKategori.Id }, responseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori eklenirken hata: {KategoriAd}", kategoriCreateDto.Ad);
                return StatusCode(500, "Kategori eklenirken hata oluştu");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateKategori(int id, KategoriCreateDto kategoriUpdateDto)
        {
            _logger.LogInformation("Kategori güncelleniyor: ID {Id}", id);

            try
            {
                var existingKategori = await _kategoriRepository.GetByIdAsync(id);
                if (existingKategori == null)
                {
                    _logger.LogWarning("Güncellenecek kategori bulunamadı: ID {Id}", id);
                    return NotFound();
                }

                existingKategori.Ad = kategoriUpdateDto.Ad;
                existingKategori.Aciklama = kategoriUpdateDto.Aciklama;

                await _kategoriRepository.UpdateAsync(existingKategori);
                _logger.LogInformation("Kategori başarıyla güncellendi: ID {Id}, Yeni Ad: {Ad}", id, kategoriUpdateDto.Ad);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori güncellenirken hata: ID {Id}", id);
                return StatusCode(500, "Kategori güncellenirken hata oluştu");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteKategori(int id)
        {
            _logger.LogInformation("Kategori siliniyor: ID {Id}", id);

            try
            {
                var kategori = await _kategoriRepository.GetByIdAsync(id);
                if (kategori == null)
                {
                    _logger.LogWarning("Silinecek kategori bulunamadı: ID {Id}", id);
                    return NotFound();
                }

                await _kategoriRepository.DeleteAsync(id);
                _logger.LogInformation("Kategori başarıyla silindi: ID {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori silinirken hata: ID {Id}", id);
                return StatusCode(500, "Kategori silinirken hata oluştu");
            }
        }

        [HttpGet("with-kitaplar")]
        public async Task<ActionResult<IEnumerable<KategoriResponseDto>>> GetKategorilerWithKitaplar()
        {
            _logger.LogInformation("Kategoriler kitaplarıyla birlikte getiriliyor");

            try
            {
                var kategoriler = await _kategoriRepository.GetKategorilerWithKitaplarAsync();
                _logger.LogInformation("Kitaplarıyla birlikte {Count} kategori bulundu", kategoriler.Count());

                var kategoriDtos = kategoriler.Select(k => new KategoriResponseDto
                {
                    Id = k.Id,
                    Ad = k.Ad,
                    Aciklama = k.Aciklama,
                    Kitaplar = k.Kitaplar.Select(kitap => new KitapResponseDto
                    {
                        Id = kitap.Id,
                        Baslik = kitap.Baslik,
                        ISBN = kitap.ISBN,
                        YayinTarihi = kitap.YayinTarihi,
                        SayfaSayisi = kitap.SayfaSayisi,
                        MusaitMi = kitap.MusaitMi,
                        YazarId = kitap.YazarId,
                        KategoriId = kitap.KategoriId,
                        Konum = kitap.Konum,
                        RafNo = kitap.RafNo

                    }).ToList()
                });
                return Ok(kategoriDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategoriler kitaplarıyla getirilemedi");
                return StatusCode(500, "Kategoriler kitaplarıyla getirilemedi");
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<KategoriResponseDto>> GetKategoriByName(string name)
        {
            _logger.LogInformation("İsme göre kategori getiriliyor: {Name}", name);

            try
            {
                var kategori = await _kategoriRepository.GetKategoriByNameAsync(name);
                if (kategori == null)
                {
                    _logger.LogWarning("İsimli kategori bulunamadı: {Name}", name);
                    return NotFound();
                }

                var kategoriDto = new KategoriResponseDto
                {
                    Id = kategori.Id,
                    Ad = kategori.Ad,
                    Aciklama = kategori.Aciklama
                };

                _logger.LogInformation("İsimli kategori bulundu: {Name}, ID: {Id}", name, kategori.Id);
                return Ok(kategoriDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İsimli kategori getirilemedi: {Name}", name);
                return StatusCode(500, "Kategori getirilemedi");
            }
        }
    }
}