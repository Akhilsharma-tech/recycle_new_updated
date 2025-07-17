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
    public class DismantlingProcessInput : ViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Type")]
        public string[] Types
        {
            get
            {
                return Enum.GetNames(typeof(DismantlingProcessType));
            }
        }

        [Required]
        public string SelectedType { get; set; }

        [Display(Name = "Loss Percentage During Recylcing Process")]
        public double? LossPercentageDuringRecyclingProcess { get; set; }

        [Required(ErrorMessage = "Climate change impact is required.")]
        [Display(Name = "Climate Change Impact")]
        public decimal? ClimateChangeImpact { get; set; }

        [Required(ErrorMessage = "Resource depletion impact is required.")]
        [Display(Name = "Resource Depletion Impact")]
        public decimal? ResourceDepletionImpact { get; set; }

        [Required(ErrorMessage = "Water withdrawal impact is required.")]
        [Display(Name = "Water Withdrawal Impact")]
        public decimal? WaterWithdrawalImpact { get; set; }

        public bool IsNewDismantlingProcess()
        {
            return Id == 0;
        }
    }
}
