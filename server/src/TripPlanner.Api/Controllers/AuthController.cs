using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Interfaces;
using System.Threading.Tasks;

namespace TripPlanner.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var (success, errors) = await _authService.RegisterAsync(request.Email, request.Password, request.FirstName, request.LastName);
            if (!success) return BadRequest(new { Errors = errors });

            // Automatically login after registration or require email verification depending on flow
            return Created("", new { request.Email, Message = "Registration successful." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var tokens = await _authService.LoginAsync(request.Email, request.Password);
            if (tokens == null) return Unauthorized(new { Error = "Invalid email or password." });

            // Set RefreshToken in HttpOnly Cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = System.DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", tokens.Value.RefreshToken, cookieOptions);

            return Ok(new { AccessToken = tokens.Value.AccessToken, ExpiresIn = 3600 });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken)) return Unauthorized();

            // Refresh token logic
            return Ok(new { AccessToken = "sample-new-jwt", ExpiresIn = 3600 });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("refreshToken");
            return Ok(new { Message = "Logged out" });
        }
    }

    public record RegisterRequest(string Email, string Password, string FirstName, string LastName);
    public record LoginRequest(string Email, string Password);
}
