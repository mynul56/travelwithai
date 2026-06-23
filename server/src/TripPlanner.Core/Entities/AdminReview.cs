using System;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class AdminReview : BaseEntity
    {
        public Guid AdminId { get; set; }
        public User Admin { get; set; } = null!;

        public Guid TargetEntityId { get; set; }
        public string TargetEntityType { get; set; } = string.Empty;

        public string? ReviewNotes { get; set; }
        public string? ActionTaken { get; set; }
    }
}
