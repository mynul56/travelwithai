using System;
using System.Threading.Tasks;

namespace TripPlanner.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreateCheckoutSessionAsync(Guid tripRequestId, string userEmail, decimal amount, string currency = "usd");
    }
}
