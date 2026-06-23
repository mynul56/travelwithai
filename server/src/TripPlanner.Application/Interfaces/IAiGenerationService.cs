using System;
using System.Threading.Tasks;
using TripPlanner.Application.DTOs;

namespace TripPlanner.Application.Interfaces
{
    public interface IAiGenerationService
    {
        Task<AiTripPackageSchema> GenerateTripPackageAsync(Guid tripRequestId);
    }
}
