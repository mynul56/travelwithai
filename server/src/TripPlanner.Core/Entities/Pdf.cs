using System;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class Pdf : BaseEntity
    {
        public Guid EntityId { get; set; }
        public string EntityType { get; set; } = string.Empty;

        public string FileUrl { get; set; } = string.Empty;
        public long? FileSizeBytes { get; set; }
    }
}
