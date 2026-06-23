using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TripPlanner.Application.Events;
using TripPlanner.Application.Interfaces;
using TripPlanner.Core.Entities;
using TripPlanner.Infrastructure.Data;

namespace TripPlanner.Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public AdminService(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<AdminAnalyticsDto> GetAnalyticsAsync()
        {
            var totalRevenue = await _context.Payments.Where(p => p.Status == "Succeeded").SumAsync(p => p.Amount);
            var totalUsers = await _context.Users.CountAsync();
            var totalTrips = await _context.TripRequests.CountAsync();
            var totalOrders = await _context.Orders.CountAsync();
            var pendingApprovals = await _context.TripRequests.CountAsync(t => t.Status == "PendingApproval");

            return new AdminAnalyticsDto(totalRevenue, totalUsers, totalTrips, totalOrders, pendingApprovals);
        }

        public async Task<List<TripRequest>> GetTripRequestsAsync(string? statusFilter = null)
        {
            var query = _context.TripRequests.AsQueryable();
            
            if (!string.IsNullOrEmpty(statusFilter))
            {
                query = query.Where(t => t.Status == statusFilter);
            }

            return await query.OrderByDescending(t => t.CreatedAt).ToListAsync();
        }

        public async Task<TripRequest> GetTripRequestDetailsAsync(Guid id)
        {
            return await _context.TripRequests
                .Include(t => t.User)
                .Include(t => t.TripPackage)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task ApproveTripAsync(Guid id, Guid adminUserId)
        {
            var trip = await _context.TripRequests.FindAsync(id);
            if (trip == null) throw new Exception("Trip not found");

            trip.Status = "Approved";
            trip.UpdatedAt = DateTime.UtcNow;

            // Audit
            _context.AdminReviews.Add(new AdminReview
            {
                Id = Guid.NewGuid(),
                TargetEntityId = id,
                TargetEntityType = "TripRequest",
                AdminId = adminUserId,
                ActionTaken = "Approve",
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

        public async Task RejectTripAsync(Guid id, string reason, Guid adminUserId)
        {
            var trip = await _context.TripRequests.FindAsync(id);
            if (trip == null) throw new Exception("Trip not found");

            trip.Status = "Rejected";
            trip.UpdatedAt = DateTime.UtcNow;

            // Audit
            _context.AdminReviews.Add(new AdminReview
            {
                Id = Guid.NewGuid(),
                TargetEntityId = id,
                TargetEntityType = "TripRequest",
                AdminId = adminUserId,
                ActionTaken = "Reject",
                ReviewNotes = reason,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

        public async Task TriggerRegenerationAsync(Guid id, Guid adminUserId)
        {
            var trip = await _context.TripRequests.FindAsync(id);
            if (trip == null) throw new Exception("Trip not found");

            trip.Status = "Generating";
            trip.UpdatedAt = DateTime.UtcNow;

            _context.AdminReviews.Add(new AdminReview
            {
                Id = Guid.NewGuid(),
                TargetEntityId = id,
                TargetEntityType = "TripRequest",
                AdminId = adminUserId,
                ActionTaken = "Regenerate",
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            // Mock firing MediatR domain event to trigger AI engine again
            // In a real app, you might re-use PaymentCompletedEvent or make a specific one.
            // await _mediator.Publish(new PaymentCompletedEvent(id, "manual-regen", 0));
        }
    }
}
