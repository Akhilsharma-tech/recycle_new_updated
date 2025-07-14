using ElectronicRecyclers.One800Recycling.Domain.Common;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class MaterialDelivery : DomainObject
    {
        internal MaterialDelivery() { }

        public virtual EnvironmentalOrganizationMaterial Material { get; set; }

        public virtual MaterialDeliveryType DeliveryType { get; set; }

        public virtual bool IsBusinessDelivery { get; set; }
    }
}