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
    public class MaterialCategoryInput : ViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Is Enabled?")]
        public bool IsEnabled { get; set; }

        public bool IsNewMaterialCategory()
        {
            return Id == 0;
        }
    }
}
