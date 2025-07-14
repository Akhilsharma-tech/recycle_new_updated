using ElectronicRecyclers.One800Recycling.Domain.Common;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class InformationVerificationGroup : DomainObject
    {
        public virtual string Name { get; set; }
    }
}