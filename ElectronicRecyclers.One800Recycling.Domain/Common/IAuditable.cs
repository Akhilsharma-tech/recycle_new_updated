using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Common
{
    public interface IAuditable
    {
        DateTimeOffset CreatedOn { get; }
        DateTimeOffset ModifiedOn { get; }
        string CreatedBy { get; }
        string ModifiedBy { get; }
    }
}
