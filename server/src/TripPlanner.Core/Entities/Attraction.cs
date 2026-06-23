using System;
using NetTopologySuite.Geometries;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class Attraction : BaseEntity, ISoftDeletable
    {
        public Guid ItineraryDayId { get; set; }
        public ItineraryDay ItineraryDay { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public int? DurationMinutes { get; set; }
        public decimal? Cost { get; set; }
        public Point? Location { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
