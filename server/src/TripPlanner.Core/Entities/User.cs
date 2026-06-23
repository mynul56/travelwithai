using System;
using System.Collections.Generic;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class User : BaseEntity, ISoftDeletable
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
