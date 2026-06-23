using System;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class TripRequest : BaseEntity, ISoftDeletable
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public string Destination { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? BudgetTier { get; set; }
        public string? Preferences { get; set; } // JSONB mapping
        public string Status { get; set; } = "Pending";

        public TripPackage? TripPackage { get; set; }
        
        public DateTime? DeletedAt { get; set; }
    }
}
