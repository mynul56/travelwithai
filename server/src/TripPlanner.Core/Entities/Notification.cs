using System;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public string? Type { get; set; }
    }
}
