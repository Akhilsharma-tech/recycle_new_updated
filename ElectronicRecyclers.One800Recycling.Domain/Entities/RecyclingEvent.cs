using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class RecyclingEvent : DomainObject
    {
        public virtual string Subject { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTimeOffset StartOn { get; set; }

        public virtual DateTimeOffset EndOn { get; set; }

        public virtual string WebsiteUrl { get; set; }

        public virtual Address Address { get; set; }

        public virtual bool IsMailingAddress { get; set; }

        public virtual bool Isvalid
        {
            get
            {
                return DateTime.Now <= EndOn;
            }
        }

        private readonly ICollection<Material> materials = new Collection<Material>();

        public virtual void AddMaterial(Material material)
        {
            if(material != null && materials.Contains(material) == false)
                materials.Add(material);
        }

        public virtual bool RemoveMaterial(Material material)
        {
            return material != null 
                && materials.Contains(material) 
                && materials.Remove(material);
        }

        public virtual Material GetMaterial(int id)
        {
            return materials.FirstOrDefault(m => m.Id == id);
        }

        public virtual bool RemoveMaterial(int id)
        {
            return RemoveMaterial(GetMaterial(id));
        }

        public virtual IEnumerable<Material> GetMaterials()
        {
            return materials.OrderBy(m => m.Name);
        } 
    }
}