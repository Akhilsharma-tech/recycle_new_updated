using ElectronicRecyclers.One800Recycling.Domain.Entities;
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
    public class MaterialComponentInput : ViewModel
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

        [Display(Name = "Virgin Production Processes")]
        public IEnumerable<DismantlingProcess> VirginProductionProcesses { get; set; }

        [Required]
        public int SelectedVirginProductionProcessId { get; set; }

        [Display(Name = "Recycling Processes")]
        public IEnumerable<DismantlingProcess> RecyclingProcesses { get; set; }

        [Required]
        public int SelectedRecyclingProcessId { get; set; }

        [Display(Name = "Landfilling Processes")]
        public IEnumerable<DismantlingProcess> LandfillingProcesses { get; set; }

        [Required]
        public int SelectedLandfillingProcessId { get; set; }

        [Display(Name = "Incineration Processes")]
        public IEnumerable<DismantlingProcess> IncinerationProcesses { get; set; }

        [Required]
        public int SelectedIncinerationProcessId { get; set; }

        public bool IsNewMaterialConmponent()
        {
            return Id == 0;
        }
    }
}
