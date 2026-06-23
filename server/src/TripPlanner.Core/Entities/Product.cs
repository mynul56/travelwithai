using System;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class Product : BaseEntity, ISoftDeletable
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; } = 0;

        public DateTime? DeletedAt { get; set; }
    }
}
