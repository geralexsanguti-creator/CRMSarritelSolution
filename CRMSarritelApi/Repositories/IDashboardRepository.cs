using CRMSarritelApi.DTOs;

namespace CRMSarritelApi.Repositories
{
    public interface IDashboardRepository
    {
        Task<DashboardDto> GetDashboardMetricsAsync(int userId);
    }
}
