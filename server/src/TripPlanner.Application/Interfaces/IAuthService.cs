using System;
using System.Threading.Tasks;

namespace TripPlanner.Application.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string[] Errors)> RegisterAsync(string email, string password, string firstName, string lastName);
        Task<(string AccessToken, string RefreshToken)?> LoginAsync(string email, string password);
        Task<(string AccessToken, string RefreshToken)?> RefreshTokenAsync(string token);
    }
}
