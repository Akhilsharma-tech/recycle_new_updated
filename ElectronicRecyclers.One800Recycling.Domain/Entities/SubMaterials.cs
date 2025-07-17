using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    public class SubMaterials : DomainObject
    {

        [Required]
        [StringLength(255)]
        public virtual string Name { get; set; }

        public virtual Material Material { get; set; }
        protected SubMaterials() { }

        public SubMaterials(Material material, string name)
        {
            Guard.Against<ArgumentNullException>(material == null, "Material is null.");
            Guard.Against<BusinessException>(string.IsNullOrWhiteSpace(name), "Sub-material name is invalid.");

            this.Material = material;
            this.Name = name;
        }

    }
}
