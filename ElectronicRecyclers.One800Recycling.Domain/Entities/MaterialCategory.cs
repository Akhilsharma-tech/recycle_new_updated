using ElectronicRecyclers.One800Recycling.Domain.Common;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class MaterialCategory : DomainObject
    {
        protected MaterialCategory() { }

        public MaterialCategory(string name, string description)
        {
            Name = name;
            Description = description;
            IsEnabled = true;
        }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsEnabled { get; set; }
    }
}