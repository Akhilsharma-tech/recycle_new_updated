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
    public class ProductDismantlingProcessInput : ViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Dismantling Processes")]
        public IEnumerable<DismantlingProcess> DismantlingProcesses { get; set; }

        [Required]
        public int SelectedDismantlingProcessId { get; set; }

        public bool IsNewProcess()
        {
            return Id == 0;
        }
    }
}
