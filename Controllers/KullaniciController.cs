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
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly ILogger<KullaniciController> _logger;

        public KullaniciController(IKullaniciRepository kullaniciRepository, ILogger<KullaniciController> logger)
        {
            _kullaniciRepository = kullaniciRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KullaniciResponseDto>>> GetAllKullanicilar()
        {
            _logger.LogInformation("Tüm kullanıcılar listeleniyor");

            try
            {
                var kullanicilar = await _kullaniciRepository.GetAllAsync();
                _logger.LogInformation("{Count} kullanıcı bulundu", kullanicilar.Count());

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcılar listelenirken hata oluştu");
                return StatusCode(500, "Kullanıcılar listelenirken hata oluştu");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KullaniciResponseDto>> GetKullanici(int id)
        {
            _logger.LogInformation("Kullanıcı getiriliyor: ID {Id}", id);

            try
            {
                var kullanici = await _kullaniciRepository.GetByIdAsync(id);
                if (kullanici == null)
                {
                    _logger.LogWarning("Kullanıcı bulunamadı: ID {Id}", id);
                    return NotFound();
                }

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

                _logger.LogInformation("Kullanıcı başarıyla getirildi: ID {Id}, Email: {Email}", kullanici.Id, kullanici.Email);
                return Ok(kullaniciDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı getirilirken hata: ID {Id}", id);
                return StatusCode(500, "Kullanıcı getirilirken hata oluştu");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<KullaniciResponseDto>> CreateKullanici(KullaniciCreateDto kullaniciCreateDto)
        {
            _logger.LogInformation("Yeni kullanıcı ekleniyor: {Email}", kullaniciCreateDto.Email);

            try
            {
                var kullanici = new Kullanici
                {
                    Ad = kullaniciCreateDto.Ad,
                    Soyad = kullaniciCreateDto.Soyad,
                    Email = kullaniciCreateDto.Email,
                    Telefon = kullaniciCreateDto.Telefon,
                    DogumTarihi = kullaniciCreateDto.DogumTarihi,
                    AktifMi = true,
                    ToplamOduncSayisi = 0
                };

                var createdKullanici = await _kullaniciRepository.AddAsync(kullanici);
                _logger.LogInformation("Kullanıcı başarıyla eklendi: ID {Id}, Email: {Email}", createdKullanici.Id, createdKullanici.Email);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı eklenirken hata: {Email}", kullaniciCreateDto.Email);
                return StatusCode(500, "Kullanıcı eklenirken hata oluştu");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateKullanici(int id, KullaniciCreateDto kullaniciUpdateDto)
        {
            _logger.LogInformation("Kullanıcı güncelleniyor: ID {Id}", id);

            try
            {
                var existingKullanici = await _kullaniciRepository.GetByIdAsync(id);
                if (existingKullanici == null)
                {
                    _logger.LogWarning("Güncellenecek kullanıcı bulunamadı: ID {Id}", id);
                    return NotFound();
                }

                existingKullanici.Ad = kullaniciUpdateDto.Ad;
                existingKullanici.Soyad = kullaniciUpdateDto.Soyad;
                existingKullanici.Email = kullaniciUpdateDto.Email;
                existingKullanici.Telefon = kullaniciUpdateDto.Telefon;
                existingKullanici.DogumTarihi = kullaniciUpdateDto.DogumTarihi;



                await _kullaniciRepository.UpdateAsync(existingKullanici);
                _logger.LogInformation("Kullanıcı başarıyla güncellendi: ID {Id}, Email: {Email}", id, kullaniciUpdateDto.Email);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı güncellenirken hata: ID {Id}", id);
                return StatusCode(500, "Kullanıcı güncellenirken hata oluştu");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteKullanici(int id)
        {
            _logger.LogInformation("Kullanıcı siliniyor: ID {Id}", id);

            try
            {
                var kullanici = await _kullaniciRepository.GetByIdAsync(id);
                if (kullanici == null)
                {
                    _logger.LogWarning("Silinecek kullanıcı bulunamadı: ID {Id}", id);
                    return NotFound();
                }

                await _kullaniciRepository.DeleteAsync(id);
                _logger.LogInformation("Kullanıcı başarıyla silindi: ID {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı silinirken hata: ID {Id}", id);
                return StatusCode(500, "Kullanıcı silinirken hata oluştu");
            }
        }

        [HttpGet("with-oduncler")]
        public async Task<ActionResult<IEnumerable<KullaniciResponseDto>>> GetKullanicilarWithOduncler()
        {
            _logger.LogInformation("Kullanıcılar ödünçleriyle birlikte getiriliyor");

            try
            {
                var kullanicilar = await _kullaniciRepository.GetKullanicilarWithOdunclerAsync();
                _logger.LogInformation("Ödünçleriyle birlikte {Count} kullanıcı bulundu", kullanicilar.Count());

                var kullaniciDtos = kullanicilar.Select(k => new KullaniciResponseDto
                {
                    Id = k.Id,
                    Ad = k.Ad,
                    Soyad = k.Soyad,
                    Email = k.Email,
                    Telefon = k.Telefon,
                    DogumTarihi = k.DogumTarihi,
                    ToplamOduncSayisi = k.ToplamOduncSayisi,
                    AktifMi = k.AktifMi,
                    Oduncler = k.Oduncler.Select(odunc => new OduncResponseDto
                    {
                        Id = odunc.Id,
                        OduncAlinmaTarihi = odunc.OduncAlinmaTarihi,
                        GeriVerilmesiGerekenTarih = odunc.GeriVerilmesiGerekenTarih,  
                        GeriVerilisTarihi = odunc.GeriVerilisTarihi,        
                        IadeEdildiMi = odunc.IadeEdildiMi,
                        KitapId = odunc.KitapId,
                        KullaniciId = odunc.KullaniciId,
                        KitapBaslik = odunc.Kitap.Baslik,
                        YazarAdSoyad = $"{odunc.Kitap.Yazar.Ad} {odunc.Kitap.Yazar.Soyad}",
                        KullaniciAdSoyad = $"{odunc.Kullanici.Ad} {odunc.Kullanici.Soyad}",
                        GecikmeGunSayisi = odunc.GecikmeGunSayisi,
                        GecikmeCezasi = odunc.GecikmeCezasi,
                        Durumu = odunc.Durumu
                        
                        
                    }).ToList()

                });
                return Ok(kullaniciDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcılar ödünçleriyle getirilemedi");
                return StatusCode(500, "Kullanıcılar ödünçleriyle getirilemedi");
            }
        }

        [HttpGet("aktif")]
        public async Task<ActionResult<IEnumerable<KullaniciResponseDto>>> GetAktifKullanicilar()
        {
            _logger.LogInformation("Aktif kullanıcılar getiriliyor");

            try
            {
                var kullanicilar = await _kullaniciRepository.GetAktifKullanicilarAsync();
                _logger.LogInformation("{Count} aktif kullanıcı bulundu", kullanicilar.Count());

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Aktif kullanıcılar getirilemedi");
                return StatusCode(500, "Aktif kullanıcılar getirilemedi");
            }
        }
    }
}