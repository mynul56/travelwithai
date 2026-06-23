using System;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class Invoice : BaseEntity, ISoftDeletable
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = "Unpaid";

        public DateTime? DeletedAt { get; set; }
    }
}
