using System;
using System.Collections.Generic;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class ItineraryDay : BaseEntity, ISoftDeletable
    {
        public Guid TripPackageId { get; set; }
        public TripPackage TripPackage { get; set; } = null!;

        public int DayNumber { get; set; }
        public DateTime Date { get; set; }
        public string? Summary { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
        public ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();
        public ICollection<Campsite> Campsites { get; set; } = new List<Campsite>();
        public ICollection<EmergencyService> EmergencyServices { get; set; } = new List<EmergencyService>();
        public ICollection<WeatherForecast> WeatherForecasts { get; set; } = new List<WeatherForecast>();

        public DateTime? DeletedAt { get; set; }
    }
}
