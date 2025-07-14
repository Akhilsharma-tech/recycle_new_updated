using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Exceptions;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class MaterialComposition : DomainObject
    {
        public virtual Material Material { get; set; }

        public virtual MaterialComponent MaterialComponent { get; set; }

        public virtual decimal CompositionPercentage { get; set; }

        protected MaterialComposition() { }

        public MaterialComposition(Material material, MaterialComponent component, decimal compositionPercentage)
        {
            Guard.Against<ArgumentNullException>(material == null, "Material is null.");
            Guard.Against<ArgumentNullException>(component == null, "Material component is null.");
            Guard.Against<BusinessException>(compositionPercentage <= 0, "Composition percentage is invalid.");

            Material = material;
            MaterialComponent = component;
            CompositionPercentage = compositionPercentage;
        }
    }
}