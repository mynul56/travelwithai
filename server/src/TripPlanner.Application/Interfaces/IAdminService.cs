using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripPlanner.Core.Entities;

namespace TripPlanner.Application.Interfaces
{
    public interface IAdminService
    {
        Task<AdminAnalyticsDto> GetAnalyticsAsync();
        Task<List<TripRequest>> GetTripRequestsAsync(string? statusFilter = null);
        Task<TripRequest?> GetTripRequestDetailsAsync(Guid id);
        Task ApproveTripAsync(Guid id, Guid adminUserId);
        Task RejectTripAsync(Guid id, string reason, Guid adminUserId);
        Task TriggerRegenerationAsync(Guid id, Guid adminUserId);
    }

    public record AdminAnalyticsDto(
        decimal TotalRevenue,
        int TotalUsers,
        int TotalTrips,
        int TotalOrders,
        int PendingApprovals
    );
}
