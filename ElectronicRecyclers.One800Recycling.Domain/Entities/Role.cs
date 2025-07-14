using ElectronicRecyclers.One800Recycling.Domain.Common;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class Role : DomainObject
    {
        protected Role() { }

        public Role(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
    }
}