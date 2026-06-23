using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TripPlanner.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    // [Authorize] // Temporarily disabled for scaffolding
    public class TripsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateTrip([FromBody] CreateTripRequest request)
        {
            return Created("", new { TripRequestId = Guid.NewGuid(), Status = "Pending" });
        }

        [HttpPut("{tripRequestId:guid}")]
        public IActionResult UpdateTrip(Guid tripRequestId, [FromBody] CreateTripRequest request)
        {
            return Ok(new { TripRequestId = tripRequestId, Status = "Pending" });
        }

        [HttpPost("{tripRequestId:guid}/submit")]
        public IActionResult SubmitTrip(Guid tripRequestId)
        {
            return Accepted();
        }

        [HttpGet("{tripRequestId:guid}")]
        public IActionResult GetTrip(Guid tripRequestId)
        {
            return Ok(new { TripRequestId = tripRequestId, Destination = "Paris", Status = "Submitted" });
        }
    }

    public record CreateTripRequest(string Destination, DateTime StartDate, DateTime EndDate, string BudgetTier, string Preferences);
}
