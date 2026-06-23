using System;
using System.Collections.Generic;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class Category : BaseEntity, ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
        public Category? Parent { get; set; }

        public ICollection<Category> SubCategories { get; set; } = new List<Category>();
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public DateTime? DeletedAt { get; set; }
    }
}
