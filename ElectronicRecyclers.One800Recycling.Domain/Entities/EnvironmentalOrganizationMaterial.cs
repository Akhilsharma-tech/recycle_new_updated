using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using NHibernate.Util;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class EnvironmentalOrganizationMaterial : DomainObject
    {
        public virtual EnvironmentalOrganization Organization { get; protected set; }

        public virtual Material Material { get; protected set; }

        protected EnvironmentalOrganizationMaterial() { }

        public  EnvironmentalOrganizationMaterial(Material material, EnvironmentalOrganization organization)
        {
            Guard.Against<ArgumentNullException>(material == null, "Material is required.");
            Guard.Against<ArgumentNullException>(organization == null, "Organization is required.");

            Material = material;
            Organization = organization;
        }

        private readonly ICollection<MaterialDelivery> materialDeliveries = new Collection<MaterialDelivery>();

        public virtual void AddMaterialDelivery(MaterialDeliveryType type, bool isBusinessDelivery)
        {
            if (materialDeliveries
                .FirstOrDefault(d => d.DeliveryType == type && d.IsBusinessDelivery == isBusinessDelivery) != null)
                return;

            materialDeliveries.Add(new MaterialDelivery 
                {
                    Material = this,
                    DeliveryType = type,
                    IsBusinessDelivery = isBusinessDelivery
                });
        }

        public virtual void AddMaterialDelivery(IEnumerable<MaterialDeliveryType> types, bool isBusinessDelivery)
        {
           types.ForEach(type => AddMaterialDelivery(type, isBusinessDelivery));
        }

        public virtual void AddMaterialDelivery(IEnumerable<string> types, bool isBusinessDelivery)
        {
            types.ForEach(type =>
            {
                MaterialDeliveryType deliveryType;
                if (Enum.TryParse(type, out deliveryType))
                    AddMaterialDelivery(deliveryType, isBusinessDelivery);
            });
        }

        public virtual bool RemoveMaterialDelivery(MaterialDelivery delivery)
        {
            return materialDeliveries.Contains(delivery) && materialDeliveries.Remove(delivery);
        }
        public virtual bool RemoveMaterialsDelivery(MaterialDeliveryType type, bool isBusinessDelivery)
        {
            var delivery = materialDeliveries.FirstOrDefault(d =>
                d.DeliveryType == type && d.IsBusinessDelivery == isBusinessDelivery);

            return delivery != null && materialDeliveries.Remove(delivery);
        }

        public virtual void RemoveMaterialsDelivery(IEnumerable<MaterialDeliveryType> types, bool isBusinessDelivery)
        {
            types.ForEach(type => RemoveMaterialsDelivery(type, isBusinessDelivery));
        }

        public virtual MaterialDelivery GetMaterialDelivery(int id)
        {
            return materialDeliveries.FirstOrDefault(m => m.Id == id);
        }

        public virtual bool RemoveMaterialDelivery(int id) 
        {
            return RemoveMaterialDelivery(GetMaterialDelivery(id));
        }

        public virtual IEnumerable<MaterialDelivery> GetMaterialDeliveries()
        {
            return materialDeliveries;
        }

        public virtual IEnumerable<MaterialDelivery> GetMaterialDeliveries(bool isBusinessDelivery)
        {
            return materialDeliveries
                .Where(d => d.IsBusinessDelivery == isBusinessDelivery)
                .ToList();
        }
    }
}