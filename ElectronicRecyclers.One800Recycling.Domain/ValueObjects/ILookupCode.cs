using ElectronicRecyclers.One800Recycling.Domain.Common;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    public interface ILookupCode : IDomainObject
    {
        string Name { get; }

        string Code { get; }

        string Description { get; }
    }
}
