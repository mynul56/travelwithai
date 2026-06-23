using Microsoft.AspNetCore.Mvc;

namespace TripPlanner.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            // TODO: Dispatch RegisterCommand via MediatR
            return Created("", new { UserId = Guid.NewGuid(), request.Email, AccessToken = "sample-jwt" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // TODO: Dispatch LoginQuery via MediatR
            // Set RefreshToken in HttpOnly Cookie
            return Ok(new { AccessToken = "sample-jwt", ExpiresIn = 3600 });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh()
        {
            return Ok(new { AccessToken = "sample-jwt", ExpiresIn = 3600 });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Clear RefreshToken cookie
            return Ok(new { Message = "Logged out" });
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            return Accepted();
        }

        [HttpPost("verify-email")]
        public IActionResult VerifyEmail([FromBody] VerifyEmailRequest request)
        {
            return Ok();
        }
    }

    // DTOs for contract definition
    public record RegisterRequest(string Email, string Password, string FirstName, string LastName);
    public record LoginRequest(string Email, string Password);
    public record ForgotPasswordRequest(string Email);
    public record VerifyEmailRequest(string Email, string Token);
}
