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
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly ILogger<LibraryController> _logger;

        public LibraryController(ILibraryRepository libraryRepository, ILogger<LibraryController> logger)
        {
            _libraryRepository = libraryRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryResponseDto>>> GetAllLibraries()
        {
            _logger.LogInformation("Tüm kütüphaneler listeleniyor");

            try
            {
                var libraries = await _libraryRepository.GetAllAsync();
                _logger.LogInformation("{Count} kütüphane bulundu", libraries.Count());

                var libraryDtos = libraries.Select(l => new LibraryResponseDto
                {
                    Id = l.Id,
                    İsim = l.İsim,
                    Adres = l.Adres,
                    Telefon = l.Telefon,
                    KitapSayisi = l.KitapSayisi,
                    UyeSayisi = l.UyeSayisi
                });
                return Ok(libraryDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kütüphaneler listelenirken hata oluştu");
                return StatusCode(500, "Kütüphaneler listelenirken hata oluştu");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryResponseDto>> GetLibrary(int id)
        {
            _logger.LogInformation("Kütüphane getiriliyor: ID {Id}", id);

            try
            {
                var library = await _libraryRepository.GetByIdAsync(id);
                if (library == null)
                {
                    _logger.LogWarning("Kütüphane bulunamadı: ID {Id}", id);
                    return NotFound();
                }

                var libraryDto = new LibraryResponseDto
                {
                    Id = library.Id,
                    İsim = library.İsim,
                    Adres = library.Adres,
                    Telefon = library.Telefon,
                    KitapSayisi = library.KitapSayisi,
                    UyeSayisi = library.UyeSayisi
                };

                _logger.LogInformation("Kütüphane başarıyla getirildi: ID {Id}, İsim: {Isim}", library.Id, library.İsim);
                return Ok(libraryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kütüphane getirilirken hata: ID {Id}", id);
                return StatusCode(500, "Kütüphane getirilirken hata oluştu");
            }
        }

        [HttpPost]
        public async Task<ActionResult<LibraryResponseDto>> CreateLibrary(LibraryCreateDto libraryCreateDto)
        {
            _logger.LogInformation("Yeni kütüphane ekleniyor: {LibraryIsim}", libraryCreateDto.İsim);

            try
            {
                var library = new Library
                {
                    İsim = libraryCreateDto.İsim,
                    Adres = libraryCreateDto.Adres,
                    Telefon = libraryCreateDto.Telefon,
                    KitapSayisi = libraryCreateDto.KitapSayisi,
                    UyeSayisi = libraryCreateDto.UyeSayisi
                };

                var createdLibrary = await _libraryRepository.AddAsync(library);
                _logger.LogInformation("Kütüphane başarıyla eklendi: ID {Id}, İsim: {Isim}", createdLibrary.Id, createdLibrary.İsim);

                var responseDto = new LibraryResponseDto
                {
                    Id = createdLibrary.Id,
                    İsim = createdLibrary.İsim,
                    Adres = createdLibrary.Adres,
                    Telefon = createdLibrary.Telefon,
                    KitapSayisi = createdLibrary.KitapSayisi,
                    UyeSayisi = createdLibrary.UyeSayisi
                };

                return CreatedAtAction(nameof(GetLibrary), new { id = createdLibrary.Id }, responseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kütüphane eklenirken hata: {LibraryIsim}", libraryCreateDto.İsim);
                return StatusCode(500, "Kütüphane eklenirken hata oluştu");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLibrary(int id, LibraryCreateDto libraryUpdateDto)
        {
            _logger.LogInformation("Kütüphane güncelleniyor: ID {Id}", id);

            try
            {
                var existingLibrary = await _libraryRepository.GetByIdAsync(id);
                if (existingLibrary == null)
                {
                    _logger.LogWarning("Güncellenecek kütüphane bulunamadı: ID {Id}", id);
                    return NotFound();
                }

                existingLibrary.İsim = libraryUpdateDto.İsim;
                existingLibrary.Adres = libraryUpdateDto.Adres;
                existingLibrary.Telefon = libraryUpdateDto.Telefon;
                existingLibrary.KitapSayisi = libraryUpdateDto.KitapSayisi;
                existingLibrary.UyeSayisi = libraryUpdateDto.UyeSayisi;

                await _libraryRepository.UpdateAsync(existingLibrary);
                _logger.LogInformation("Kütüphane başarıyla güncellendi: ID {Id}, Yeni İsim: {Isim}", id, libraryUpdateDto.İsim);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kütüphane güncellenirken hata: ID {Id}", id);
                return StatusCode(500, "Kütüphane güncellenirken hata oluştu");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibrary(int id)
        {
            _logger.LogInformation("Kütüphane siliniyor: ID {Id}", id);

            try
            {
                var library = await _libraryRepository.GetByIdAsync(id);
                if (library == null)
                {
                    _logger.LogWarning("Silinecek kütüphane bulunamadı: ID {Id}", id);
                    return NotFound();
                }

                await _libraryRepository.DeleteAsync(id);
                _logger.LogInformation("Kütüphane başarıyla silindi: ID {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kütüphane silinirken hata: ID {Id}", id);
                return StatusCode(500, "Kütüphane silinirken hata oluştu");
            }
        }

        [HttpGet("with-kitaplar")]
        public async Task<ActionResult<IEnumerable<LibraryResponseDto>>> GetLibrariesWithKitaplar()
        {
            _logger.LogInformation("Kütüphaneler kitaplarıyla birlikte getiriliyor");

            try
            {
                var libraries = await _libraryRepository.GetLibrariesWithKitaplarAsync();
                _logger.LogInformation("Kitaplarıyla birlikte {Count} kütüphane bulundu", libraries.Count());

                var libraryDtos = libraries.Select(l => new LibraryResponseDto
                {
                    Id = l.Id,
                    İsim = l.İsim,
                    Adres = l.Adres,
                    Telefon = l.Telefon,
                    KitapSayisi = l.KitapSayisi,
                    UyeSayisi = l.UyeSayisi,
                    Kitaplar = l.Kitaplar.Select(kitap => new KitapResponseDto
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
                    }).ToList()
                });
                return Ok(libraryDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kütüphaneler kitaplarıyla getirilemedi");
                return StatusCode(500, "Kütüphaneler kitaplarıyla getirilemedi");
            }
        }

        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<LibraryResponseDto>>> GetPopularLibraries()
        {
            _logger.LogInformation("Popüler kütüphaneler getiriliyor");

            try
            {
                var libraries = await _libraryRepository.GetLibrariesWithEnCokKullanicilar();
                _logger.LogInformation("{Count} popüler kütüphane bulundu", libraries.Count());

                var libraryDtos = libraries
                    .OrderByDescending(l => l.UyeSayisi)
                    .Select(l => new LibraryResponseDto
                    {
                        Id = l.Id,
                        İsim = l.İsim,
                        Adres = l.Adres,
                        Telefon = l.Telefon,
                        KitapSayisi = l.KitapSayisi,
                        UyeSayisi = l.UyeSayisi
                    });
                return Ok(libraryDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Popüler kütüphaneler getirilemedi");
                return StatusCode(500, "Popüler kütüphaneler getirilemedi");
            }
        }
    }
}