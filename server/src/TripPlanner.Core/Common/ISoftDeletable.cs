using System;

namespace TripPlanner.Core.Common
{
    public interface ISoftDeletable
    {
        DateTime? DeletedAt { get; set; }
    }
}
