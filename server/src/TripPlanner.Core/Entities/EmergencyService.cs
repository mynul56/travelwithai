using System;
using NetTopologySuite.Geometries;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class EmergencyService : BaseEntity
    {
        public Guid ItineraryDayId { get; set; }
        public ItineraryDay ItineraryDay { get; set; } = null!;

        public string ServiceType { get; set; } = string.Empty; // Hospital, Police
        public string? Name { get; set; }
        public string? ContactNumber { get; set; }
        public Point? Location { get; set; }
    }
}
