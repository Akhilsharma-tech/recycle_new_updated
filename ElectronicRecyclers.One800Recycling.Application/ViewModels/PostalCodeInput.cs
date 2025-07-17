using ElectronicRecyclers.One800Recycling.Application.Helpers.Attributes;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
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
    public class PostalCodeInput : ViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [RequiredIfNot("SelectedCountryCode", "US", ErrorMessage = "The Region field is required.")]
        [StringLength(40)]
        [Display(Name = "Region")]
        public string Region { get; set; }

        [Display(Name = "State")]
        public IEnumerable<State> States { get; set; }

        [RequiredIf("SelectedCountryCode", "US", ErrorMessage = "The State field is required.")]
        public string SelectedStateCode { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public IEnumerable<Country> Countries { get; set; }

        [Required(ErrorMessage = "The Country field is required.")]
        public string SelectedCountryCode { get; set; }

        [Required]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        public bool IsNewPostalCode()
        {
            return Id == 0;
        }
    }
}
