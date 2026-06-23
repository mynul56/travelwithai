using System.Linq;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TripPlanner.Application.DTOs;
using TripPlanner.Application.Interfaces;

namespace TripPlanner.Infrastructure.Services
{
    public class QuestPdfGenerationService : IPdfGenerationService
    {
        public QuestPdfGenerationService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public Task<byte[]> GenerateTripPdfAsync(AiTripPackageSchema packageData, string tripDestination)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                    page.Header().Element(x => ComposeHeader(x, tripDestination));
                    page.Content().Element(x => ComposeContent(x, packageData));
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
            });

            return Task.FromResult(document.GeneratePdf());
        }

        private void ComposeHeader(IContainer container, string destination)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"Trip to {destination}").FontSize(24).SemiBold().FontColor(Colors.Blue.Darken2);
                    column.Item().Text("Custom AI-Generated Itinerary").FontSize(14).FontColor(Colors.Grey.Medium);
                });
            });
        }

        private void ComposeContent(IContainer container, AiTripPackageSchema package)
        {
            container.PaddingVertical(1, Unit.Centimetre).Column(column =>
            {
                column.Spacing(20);

                // Summary Section
                column.Item().Text("Overview").FontSize(18).SemiBold();
                column.Item().Text(package.TripSummary.Overview);
                column.Item().Text($"Estimated Cost: ${package.TripSummary.TotalEstimatedCost}").SemiBold();

                // Itinerary Section
                column.Item().Text("Day-by-Day Itinerary").FontSize(18).SemiBold();
                foreach (var day in package.DailyItineraries)
                {
                    column.Item().PaddingBottom(10).Column(dayCol =>
                    {
                        dayCol.Item().Text($"Day {day.DayNumber} - {day.Date} ({day.Theme})").FontSize(14).SemiBold().FontColor(Colors.Blue.Medium);
                        dayCol.Item().Text($"Weather: {day.WeatherForecast}").FontSize(10).FontColor(Colors.Grey.Medium);
                        
                        foreach (var activity in day.Activities)
                        {
                            dayCol.Item().PaddingLeft(10).Text($"{activity.Time} - {activity.Name} ({activity.Type})");
                            dayCol.Item().PaddingLeft(20).Text(activity.Description).FontSize(10).FontColor(Colors.Grey.Darken1);
                        }
                    });
                }

                // Emergency Info
                column.Item().Text("Emergency Information").FontSize(18).SemiBold().FontColor(Colors.Red.Medium);
                column.Item().Text($"Hospital: {package.EmergencyInformation.NearestHospital.Name} ({package.EmergencyInformation.NearestHospital.Phone})");
                column.Item().Text($"Police: {package.EmergencyInformation.LocalPolice.Phone}");
            });
        }
    }
}
