

using Microsoft.AspNetCore.Mvc;
using Kutuphane.Repositories.Interfaces; 
using Kutuphane.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
namespace Kutuphane.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(
            IDashboardRepository dashboardRepository,
            ILogger<DashboardController> logger)
        {
            _dashboardRepository = dashboardRepository;
            _logger = logger;
        }

        [HttpGet("public/stats")]
        public async Task<ActionResult<PublicStatsResponseDto>> GetPublicStats()
        {
            _logger.LogInformation("Public İstatistikler alınıyor");

            try
            {
                var publicstats = await _dashboardRepository.GetPublicStatsAsync();
                _logger.LogInformation("Public İstatistikler başarıyla alındı");
                return Ok(publicstats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Public İstatistikler alınırken hata oluştu");
                return StatusCode(500, "Public İstatistikler alınırken hata oluştu");
            }
        }

        [HttpGet("public/popular-books")]
        public async Task<ActionResult<List<PopulerKitapInfo>>> GetPopulerKitaplar([FromQuery] int limit = 10)
        {
            _logger.LogInformation("Popüler kitaplar alınıyor, limit: {Limit}", limit);
            try
            {
                var popularbooks = await _dashboardRepository.GetPopulerKitaplarAsync(limit);
                _logger.LogInformation("{Count} popüler kitap bulundu", popularbooks.Count);
                return Ok(popularbooks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Popüler kitaplar alınırken hata oluştu");
                return StatusCode(500, "Popüler kitaplar alınırken hata oluştu");
            }
        }
        [HttpGet("admin/full")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DashboardResponseDto>> GetAdminDashboard()
        {
            _logger.LogInformation("Admin verileri isteniyor");
            try
            {
                var admindashboard = await _dashboardRepository.GetDashboardDataAsync();
                _logger.LogInformation("Admin verileri başarıyla alındı");
                return Ok(admindashboard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin verileri alınırken hata oluştu");
                return StatusCode(500, "Admin verileri alınırken hata oluştu");
            }
        }
        [HttpGet("admin/financial")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AdminFinancialResponseDto>> GetAdminFinancial()
        {
            _logger.LogInformation("Admin finansal verileri isteniyor");
            try
            {
                var adminfinans = await _dashboardRepository.GetAdminFinancialAsync();
                _logger.LogInformation("Admin finansal verileri başarıyla alındı");
                return Ok(adminfinans);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Finansal veriler alınırken hata oluştu");
                return StatusCode(500, "Finansal veriler alınırken hata oluştu");
            }
        }
        [HttpGet("public/trends")]
        public async Task<ActionResult<List<AylikTrend>>> GetAylikTrend()
        {
            _logger.LogInformation("Aylık trendler alınıyor");
            try
            {
                var aylikTrendler = await _dashboardRepository.GetAylikTrendlerAsync();
                _logger.LogInformation("Aylık trendler başarıyla alındı");
                return Ok(aylikTrendler);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Aylık trendler alınıırken hata oluştu");
                return StatusCode(500, "Aylık trendler alınırken hata oluştu");
            }
        }
    }
}