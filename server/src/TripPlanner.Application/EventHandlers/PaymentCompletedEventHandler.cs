using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TripPlanner.Application.Events;
using TripPlanner.Application.Interfaces;

namespace TripPlanner.Application.EventHandlers
{
    public class PaymentCompletedEventHandler : INotificationHandler<PaymentCompletedEvent>
    {
        private readonly ILogger<PaymentCompletedEventHandler> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public PaymentCompletedEventHandler(ILogger<PaymentCompletedEventHandler> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public Task Handle(PaymentCompletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("PaymentCompletedEvent triggered for TripRequest: {TripRequestId}", notification.TripRequestId);

            // Fire and forget background task to generate the trip package
            Task.Run(async () => 
            {
                try
                {
                    _logger.LogInformation("Starting AI Generation Background Task for {TripRequestId}...", notification.TripRequestId);
                    
                    using var scope = _scopeFactory.CreateScope();
                    var aiService = scope.ServiceProvider.GetRequiredService<IAiGenerationService>();
                    
                    var generatedPackage = await aiService.GenerateTripPackageAsync(notification.TripRequestId);
                    
                    _logger.LogInformation("AI Generation successful for {TripRequestId}. Saving to DB...", notification.TripRequestId);
                    
                    // Here we would use DbContext or a Repository to save generatedPackage to PostgreSQL.
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Background AI Generation failed for {TripRequestId}", notification.TripRequestId);
                }
            });

            return Task.CompletedTask;
        }
    }
}
