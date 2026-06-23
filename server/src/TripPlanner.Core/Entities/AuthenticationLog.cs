using System;
using TripPlanner.Core.Common;

namespace TripPlanner.Core.Entities
{
    public class AuthenticationLog : BaseEntity
    {
        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public bool LoginSuccessful { get; set; }
    }
}
