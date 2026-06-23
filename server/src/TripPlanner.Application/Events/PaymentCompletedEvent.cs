using System;
using MediatR;

namespace TripPlanner.Application.Events
{
    public class PaymentCompletedEvent : INotification
    {
        public Guid TripRequestId { get; }
        public string StripeSessionId { get; }
        public decimal Amount { get; }

        public PaymentCompletedEvent(Guid tripRequestId, string stripeSessionId, decimal amount)
        {
            TripRequestId = tripRequestId;
            StripeSessionId = stripeSessionId;
            Amount = amount;
        }
    }
}
