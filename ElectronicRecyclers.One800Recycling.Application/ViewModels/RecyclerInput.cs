using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public class RecyclerInput : EnvironmentalOrganizationInput
    {
        [Display(Name = "Is Dedicated Recyler")]
        public bool IsDedicatedRecycler { get; set; }
    }
}
