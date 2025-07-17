using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public class SubMaterialInputViewModel
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
