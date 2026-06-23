using System;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class WeatherForecast : BaseEntity
    {
        public Guid ItineraryDayId { get; set; }
        public ItineraryDay ItineraryDay { get; set; } = null!;

        public string? Condition { get; set; }
        public decimal? HighTemp { get; set; }
        public decimal? LowTemp { get; set; }
        public int? PrecipitationChance { get; set; }
    }
}
