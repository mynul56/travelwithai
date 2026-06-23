using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenAI.Chat;
using TripPlanner.Application.DTOs;
using TripPlanner.Application.Interfaces;

namespace TripPlanner.Infrastructure.Services
{
    public class AiGenerationService : IAiGenerationService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AiGenerationService> _logger;

        public AiGenerationService(IConfiguration config, ILogger<AiGenerationService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<AiTripPackageSchema> GenerateTripPackageAsync(Guid tripRequestId)
        {
            _logger.LogInformation("Starting RAG aggregation and AI generation for TripRequestId: {Id}", tripRequestId);

            // 1. RAG Aggregation Phase (Mocked for MVP)
            // In a real implementation, we would query the Database for TripRequest details,
            // then call Google Places API, Weather API, etc.
            var destination = "Paris, France";
            var dates = "Oct 1 - Oct 5, 2026";
            var budget = "Moderate";
            
            var aggregatedContext = $$"""
            [GROUND_TRUTH_DATA]
            Weather: Avg 65°F, partly cloudy.
            Restaurants: Le Jules Verne (PlaceId: ChIJ123), Bistrot Paul Bert (PlaceId: ChIJ456).
            Attractions: Eiffel Tower (PlaceId: ChIJ789), Louvre Museum (PlaceId: ChIJabc).
            Emergency: American Hospital of Paris (Phone: +33 1 46 41 25 25, Address: 63 Blvd Victor Hugo), Police (Phone: 112).
            """;

            // 2. OpenAI API Setup
            var apiKey = _config["OpenAI:ApiKey"] ?? "sk-mock-key";
            var client = new ChatClient("gpt-4o", apiKey);

            var systemPrompt = """
            You are an elite, highly-accurate travel concierge. Construct a logical day-by-day itinerary.
            CRITICAL RULE: You must strictly use ONLY the verified locations, restaurants, and attractions provided in the [GROUND_TRUTH_DATA].
            Do not invent or guess any places. 
            Output strictly in JSON matching the exact provided schema.
            """;

            var userPrompt = $"""
            Trip Profile:
            Destination: {destination}
            Dates: {dates}
            Budget: {budget}
            
            {aggregatedContext}
            """;

            // 3. OpenAI Structured Output Call
            try 
            {
                var response = await client.CompleteChatAsync(
                    new ChatMessage[] {
                        new SystemChatMessage(systemPrompt),
                        new UserChatMessage(userPrompt)
                    },
                    new ChatCompletionOptions {
                        ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat()
                    }
                );

                var jsonOutput = response.Value.Content[0].Text;
                
                // 4. Validation Layer (Deserialization)
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var parsedOutput = JsonSerializer.Deserialize<AiTripPackageSchema>(jsonOutput, options);

                if (parsedOutput == null)
                    throw new Exception("AI returned invalid JSON structure.");

                _logger.LogInformation("Successfully generated and validated trip package.");
                return parsedOutput;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate AI trip package.");
                throw;
            }
        }
    }
}
