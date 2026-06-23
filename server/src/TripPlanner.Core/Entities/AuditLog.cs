using System;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class AuditLog : BaseEntity
    {
        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public string Action { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
        public Guid RecordId { get; set; }

        public string? OldValues { get; set; } // JSONB
        public string? NewValues { get; set; } // JSONB
        public string? IpAddress { get; set; }
    }
}
