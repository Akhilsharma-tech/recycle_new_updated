using ElectronicRecyclers.One800Recycling.Domain.Common;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    public class ProductDismantlingProcess : DomainObject
    {
        public virtual string ProductName { get; set; }

        public virtual DismantlingProcess DismantlingProcess { get; set; }

        protected ProductDismantlingProcess() { }

        public ProductDismantlingProcess(string productName, DismantlingProcess process)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrWhiteSpace(productName), "Product name is null.");
            Guard.Against<ArgumentNullException>(process == null, "Dismantling process is null.");

            ProductName = productName;
            DismantlingProcess = process;
        }
    }
}