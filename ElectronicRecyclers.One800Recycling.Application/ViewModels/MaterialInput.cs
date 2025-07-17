using ElectronicRecyclers.One800Recycling.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public class MaterialInput : ViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Search Keywords")]
        public IList<string> SearchKeywords { get; set; }

        [Display(Name = "Enabled?")]
        public bool IsEnabled { get; set; }

        [Display(Name = "Categories")]
        public IList<MaterialCategoryViewModel> Categories { get; set; }

        [Display(Name = "Product Dismantling Processes")]
        public IList<ProcessViewModel> Processes { get; set; }

        [Display(Name = "Material Compositions")]
        public IList<MaterialCompositionViewModel> Compositions { get; set; }
        [Display(Name = "Sub Materials")]
        public IList<SubMaterialInputViewModel> SubMaterials { get; set; }

        public bool IsNewMaterial()
        {
            return Id == 0;
        }

        public class ProcessViewModel
        {
            public int Id { get; set; }

            public string ProductName { get; set; }

            public string ProcessName { get; set; }

            public string CompositionPercentage { get; set; }
        }

        public class MaterialCompositionViewModel
        {
            public int Id { get; set; }

            public string MaterialComponentName { get; set; }

            public decimal CompositionPercentage { get; set; }
        }
    }
}
