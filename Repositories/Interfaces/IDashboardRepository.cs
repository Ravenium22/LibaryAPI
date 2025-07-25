using Kutuphane.Models.DTOs;

namespace Kutuphane.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardResponseDto> GetDashboardDataAsync();
        Task<PublicStatsResponseDto> GetPublicStatsAsync();
        Task<AdminFinancialResponseDto> GetAdminFinancialAsync();
        Task<List<PopulerKitapInfo>> GetPopulerKitaplarAsync(int limit = 10);
        Task<List<AylikTrend>> GetAylikTrendlerAsync();
    }
}