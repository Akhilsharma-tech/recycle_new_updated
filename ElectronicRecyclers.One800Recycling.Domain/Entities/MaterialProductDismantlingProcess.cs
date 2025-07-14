using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Exceptions;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class MaterialProductDismantlingProcess : DomainObject
    {
        public virtual Material Material { get; set; }

        public virtual ProductDismantlingProcess ProductDismantlingProcess { get; set; }

        public virtual decimal CompositionPercentage { get; set; }

        protected MaterialProductDismantlingProcess() { }

        public MaterialProductDismantlingProcess(
             Material material, 
             ProductDismantlingProcess process, 
             decimal compositionPercentage)
        {
            Guard.Against<ArgumentNullException>(material == null, "Material is null.");
            Guard.Against<ArgumentNullException>(process == null, "Product dismantling process is null.");
            Guard.Against<BusinessException>(compositionPercentage <= 0, "Composition percentage is invalid.");

            Material = material;
            ProductDismantlingProcess = process;
            CompositionPercentage = compositionPercentage;
        }
    }
}