using System;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class MaterialComponent : DomainObject
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual MaterialComponentDismantlingProcess DismantlingProcess { get; set; }

        protected MaterialComponent() { }

        public MaterialComponent(string name, string description, MaterialComponentDismantlingProcess process)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrWhiteSpace(name), "Name is null.");
            Guard.Against<ArgumentNullException>(string.IsNullOrWhiteSpace(description), "Description is null.");
            Guard.Against<ArgumentNullException>(process == null, "Dismantling process is null.");

            Name = name;
            Description = description;
            DismantlingProcess = process;
        }
    }
}