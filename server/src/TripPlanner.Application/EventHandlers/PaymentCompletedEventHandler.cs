using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TripPlanner.Application.Events;
// using TripPlanner.Application.Interfaces; // We will use IServiceProvider to resolve scoped services in a background task

namespace TripPlanner.Application.EventHandlers
{
    public class PaymentCompletedEventHandler : INotificationHandler<PaymentCompletedEvent>
    {
        private readonly ILogger<PaymentCompletedEventHandler> _logger;

        public PaymentCompletedEventHandler(ILogger<PaymentCompletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(PaymentCompletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("PaymentCompletedEvent triggered for TripRequest: {TripRequestId}", notification.TripRequestId);

            // TODO: In a production app, we would resolve a scoped IServiceScopeFactory here 
            // and trigger the AI Generation service asynchronously, so the Stripe Webhook can return 200 OK immediately.
            
            // For MVP: We mock the background generation start.
            Task.Run(() => 
            {
                _logger.LogInformation("Starting AI Generation Background Task for {TripRequestId}...", notification.TripRequestId);
                // 1. Fetch TripRequest from DB
                // 2. Call OpenAI API via IAiGenerationService
                // 3. Save TripPackage to DB
                // 4. Send Email Notification
            });

            return Task.CompletedTask;
        }
    }
}
