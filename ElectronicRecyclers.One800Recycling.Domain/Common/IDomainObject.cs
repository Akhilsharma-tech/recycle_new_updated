using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Common
{
    public interface IDomainObject
    {
        int Id { get; }

        DateTimeOffset CreatedOn { get; }

        DateTimeOffset ModifiedOn { get; }

        string CreatedBy { get; }

        string ModifiedBy { get; }

        int Version { get; } 
    }
}
