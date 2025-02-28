using HallBookingAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HallBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardRepository _dashboardRepository;
        public DashboardController(DashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult GetDashboardData()
        {
            var data = _dashboardRepository.GetAdminDashboardData();
            return Ok(data);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("{OwnerID}")]
        public async Task<IActionResult> GetDashboardData(int OwnerID, DateTime? startDate = null, DateTime? endDate = null)
        {
            var result = await _dashboardRepository.GetDashboardDataAsync(OwnerID, startDate, endDate);
            if (result == null)
                return NotFound("No data found.");

            return Ok(result);
        }
    }
}
