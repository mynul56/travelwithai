using System;
using NetTopologySuite.Geometries;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class Restaurant : BaseEntity, ISoftDeletable
    {
        public Guid ItineraryDayId { get; set; }
        public ItineraryDay ItineraryDay { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public string? CuisineType { get; set; }
        public decimal? Rating { get; set; }
        public Point? Location { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
