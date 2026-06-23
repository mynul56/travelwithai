using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class Role : IdentityRole<Guid>, ISoftDeletable
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
