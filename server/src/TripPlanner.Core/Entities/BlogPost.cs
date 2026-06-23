using System;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class BlogPost : BaseEntity, ISoftDeletable
    {
        public Guid AuthorId { get; set; }
        public User Author { get; set; } = null!;

        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        
        public bool IsPublished { get; set; } = false;
        public DateTime? PublishedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
