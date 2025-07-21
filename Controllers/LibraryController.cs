using Microsoft.AspNetCore.Mvc;
using Kutuphane.Models;
using Kutuphane.Models.DTOs;
using Kutuphane.Repositories.Interfaces;

namespace Kutuphane.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryRepository _libraryRepository;

        public LibraryController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryResponseDto>>> GetAllLibraries()
        {
            var libraries = await _libraryRepository.GetAllAsync();
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

        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryResponseDto>> GetLibrary(int id)
        {
            var library = await _libraryRepository.GetByIdAsync(id);
            if (library == null) return NotFound();
            
            var libraryDto = new LibraryResponseDto
            {
                Id = library.Id,
                İsim = library.İsim,
                Adres = library.Adres,
                Telefon = library.Telefon,
                KitapSayisi = library.KitapSayisi,
                UyeSayisi = library.UyeSayisi
            };
            return Ok(libraryDto);
        }

        [HttpPost]
        public async Task<ActionResult<LibraryResponseDto>> CreateLibrary(LibraryCreateDto libraryCreateDto)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLibrary(int id, LibraryCreateDto libraryUpdateDto)
        {
            var existingLibrary = await _libraryRepository.GetByIdAsync(id);
            if (existingLibrary == null) return NotFound();
            
            existingLibrary.İsim = libraryUpdateDto.İsim;
            existingLibrary.Adres = libraryUpdateDto.Adres;
            existingLibrary.Telefon = libraryUpdateDto.Telefon;
            existingLibrary.KitapSayisi = libraryUpdateDto.KitapSayisi;
            existingLibrary.UyeSayisi = libraryUpdateDto.UyeSayisi;
            
            await _libraryRepository.UpdateAsync(existingLibrary);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibrary(int id)
        {
            await _libraryRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("with-kitaplar")]
        public async Task<ActionResult<IEnumerable<LibraryResponseDto>>> GetLibrariesWithKitaplar()
        {
            var libraries = await _libraryRepository.GetLibrariesWithKitaplarAsync();
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

        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<LibraryResponseDto>>> GetPopularLibraries()
        {
            var libraries = await _libraryRepository.GetLibrariesWithEnCokKullanicilar();
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
    }
}