using System;
using System.Collections.Generic;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class Role : BaseEntity, ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
