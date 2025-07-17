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
    public abstract class LookupCodeInput : ViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(160)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public bool IsNewLookup()
        {
            return Id == 0;
        }
    }
}
