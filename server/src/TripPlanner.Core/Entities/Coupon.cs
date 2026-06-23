using System;
using System.Collections.Generic;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class Coupon : BaseEntity, ISoftDeletable
    {
        public string Code { get; set; } = string.Empty;
        public int? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
