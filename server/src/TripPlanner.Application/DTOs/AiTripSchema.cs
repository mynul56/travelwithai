using System;
using System.Collections.Generic;

namespace TripPlanner.Application.DTOs
{
    // These DTOs map exactly to the JSON Schema expected from the OpenAI Structured Outputs
    public record AiTripPackageSchema(
        AiTripSummary TripSummary,
        List<AiDailyItinerary> DailyItineraries,
        List<AiPackingCategory> PackingChecklist,
        AiBudgetBreakdown BudgetBreakdown,
        AiEmergencyInformation EmergencyInformation
    );

    public record AiTripSummary(
        string Title,
        string Overview,
        decimal TotalEstimatedCost
    );

    public record AiDailyItinerary(
        int DayNumber,
        string Date,
        string Theme,
        string WeatherForecast,
        List<AiActivity> Activities
    );

    public record AiActivity(
        string Time,
        string Type,
        string Name,
        string GooglePlaceId,
        string Description,
        decimal EstimatedCost
    );

    public record AiPackingCategory(
        string Category,
        List<string> Items
    );

    public record AiBudgetBreakdown(
        decimal Accommodation,
        decimal Food,
        decimal Activities,
        decimal Transit
    );

    public record AiEmergencyInformation(
        AiHospital NearestHospital,
        AiPolice LocalPolice,
        string EmbassyContact
    );

    public record AiHospital(
        string Name,
        string Phone,
        string Address
    );

    public record AiPolice(
        string Phone
    );
}
