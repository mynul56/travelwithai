using System;
using System.Collections.Generic;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class TripPackage : BaseEntity, ISoftDeletable
    {
        public Guid TripRequestId { get; set; }
        public TripRequest TripRequest { get; set; } = null!;

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? TotalEstimatedCost { get; set; }
        public string? AiGenerationMetadata { get; set; } // JSONB mapping
        public string? PdfUrl { get; set; }

        public ICollection<ItineraryDay> ItineraryDays { get; set; } = new List<ItineraryDay>();

        public DateTime? DeletedAt { get; set; }
    }
}
