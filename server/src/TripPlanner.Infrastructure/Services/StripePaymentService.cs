using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using TripPlanner.Application.Interfaces;

namespace TripPlanner.Infrastructure.Services
{
    public class StripePaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;

        public StripePaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        public async Task<string> CreateCheckoutSessionAsync(Guid tripRequestId, string userEmail, decimal amount, string currency = "usd")
        {
            var domain = _configuration["App:FrontendUrl"] ?? "http://localhost:3000";

            var options = new SessionCreateOptions
            {
                CustomerEmail = userEmail,
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(amount * 100), // Stripe expects amounts in cents
                            Currency = currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "AI Adventure Trip Generation",
                                Description = "Full customized itinerary generation including POIs, daily plans, and map links."
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = $"{domain}/plan/success?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{domain}/plan/cancel",
                Metadata = new Dictionary<string, string>
                {
                    { "TripRequestId", tripRequestId.ToString() }
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;
        }
    }
}
