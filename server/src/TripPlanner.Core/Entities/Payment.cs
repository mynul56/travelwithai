using System;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class Payment : BaseEntity
    {
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }

        public Guid? TripPackageId { get; set; }
        public TripPackage? TripPackage { get; set; }

        public decimal Amount { get; set; }
        public string Provider { get; set; } = string.Empty;
        public string? ProviderTransactionId { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
