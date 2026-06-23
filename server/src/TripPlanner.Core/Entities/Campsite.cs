using System;
using NetTopologySuite.Geometries;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class Campsite : BaseEntity, ISoftDeletable
    {
        public Guid ItineraryDayId { get; set; }
        public ItineraryDay ItineraryDay { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public string? Amenities { get; set; } // JSONB
        public bool IsReserved { get; set; } = false;
        public Point? Location { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
