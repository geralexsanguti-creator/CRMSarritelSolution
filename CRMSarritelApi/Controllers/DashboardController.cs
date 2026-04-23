using CRMSarritelApi.DTOs;
using CRMSarritelApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMSarritelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires valid JWT token
    public class DashboardController(IDashboardRepository repository) : ControllerBase
    {
        private readonly IDashboardRepository _repository = repository;

        [HttpGet]
        public async Task<ActionResult<DashboardDto>> GetDashboard()
        {
            try
            {
                var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(userIdString, out int userId))
                    return Unauthorized("Missing or invalid user id in token");

                var dashboardData = await _repository.GetDashboardMetricsAsync(userId);
                return Ok(dashboardData);
            }
            catch (Exception ex)
            {
                // In production, log the exception
                return StatusCode(500, "Error generating dashboard metrics: " + ex.Message);
            }
        }
    }
}
