using System.Threading.Tasks;
using TripPlanner.Application.DTOs;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Interfaces
{
    public interface IPdfGenerationService
    {
        Task<byte[]> GenerateTripPdfAsync(AiTripPackageSchema packageData, string tripDestination);
    }
}
