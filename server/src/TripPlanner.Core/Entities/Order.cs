using System;
using System.Collections.Generic;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class Order : BaseEntity, ISoftDeletable
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid? CouponId { get; set; }
        public Coupon? Coupon { get; set; }

        public decimal Subtotal { get; set; }
        public decimal DiscountApplied { get; set; } = 0;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }
        public Invoice? Invoice { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
