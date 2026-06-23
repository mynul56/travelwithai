using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using TripPlanner.Application.Events;
using TripPlanner.Application.Interfaces;

namespace TripPlanner.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentService paymentService, IMediator mediator, IConfiguration config, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _mediator = mediator;
            _config = config;
            _logger = logger;
        }

        [HttpPost("checkout-session")]
        [Authorize]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutRequest request)
        {
            // In a real scenario, fetch User Email from JWT Claims, and get Amount from DB based on Trip parameters
            var userEmail = User.Identity?.Name ?? "user@example.com";
            
            var checkoutUrl = await _paymentService.CreateCheckoutSessionAsync(request.TripRequestId, userEmail, 19.99m);
            return Ok(new { CheckoutUrl = checkoutUrl });
        }

        [HttpPost("webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var endpointSecret = _config["Stripe:WebhookSecret"];

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    endpointSecret
                );

                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    
                    if (session?.Metadata.TryGetValue("TripRequestId", out var tripIdStr) == true && Guid.TryParse(tripIdStr, out var tripRequestId))
                    {
                        var amount = (session.AmountTotal ?? 0) / 100m;
                        
                        // Fire Domain Event for background AI trigger
                        await _mediator.Publish(new PaymentCompletedEvent(tripRequestId, session.Id, amount));
                    }
                }

                return Ok();
            }
            catch (StripeException e)
            {
                _logger.LogError(e, "Stripe Webhook verification failed.");
                return BadRequest();
            }
        }
    }

    public record CreateCheckoutRequest(Guid TripRequestId);
}
