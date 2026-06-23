using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Interfaces;

namespace TripPlanner.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("analytics")]
        public async Task<IActionResult> GetAnalytics()
        {
            var data = await _adminService.GetAnalyticsAsync();
            return Ok(data);
        }

        [HttpGet("trips")]
        public async Task<IActionResult> GetTrips([FromQuery] string? statusFilter = null)
        {
            var trips = await _adminService.GetTripRequestsAsync(statusFilter);
            return Ok(trips);
        }

        [HttpGet("trips/{id}")]
        public async Task<IActionResult> GetTripDetails(Guid id)
        {
            var trip = await _adminService.GetTripRequestDetailsAsync(id);
            if (trip == null) return NotFound();
            return Ok(trip);
        }

        [HttpPost("trips/{id}/approve")]
        public async Task<IActionResult> ApproveTrip(Guid id)
        {
            var adminId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _adminService.ApproveTripAsync(id, adminId);
            return Ok(new { message = "Trip approved successfully" });
        }

        [HttpPost("trips/{id}/reject")]
        public async Task<IActionResult> RejectTrip(Guid id, [FromBody] RejectRequest request)
        {
            var adminId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _adminService.RejectTripAsync(id, request.Reason, adminId);
            return Ok(new { message = "Trip rejected successfully" });
        }

        [HttpPost("trips/{id}/regenerate")]
        public async Task<IActionResult> RegenerateTrip(Guid id)
        {
            var adminId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _adminService.TriggerRegenerationAsync(id, adminId);
            return Ok(new { message = "Trip regeneration triggered" });
        }
    }

    public class RejectRequest
    {
        public string Reason { get; set; } = string.Empty;
    }
}
