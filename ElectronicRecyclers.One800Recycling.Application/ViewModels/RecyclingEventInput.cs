using ElectronicRecyclers.One800Recycling.Application.Helpers.Attributes;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
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
    public class RecyclingEventInput : ViewModel, IAddressInput
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [StringLength(300)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy h:mm tt}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTimeOffset? StartOn { get; set; }

        [Required]
        [Display(Name = "End Time")]
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy h:mm tt}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTimeOffset? EndOn { get; set; }

        [Url]
        [StringLength(255)]
        [Display(Name = "Website Address")]
        [DataType(DataType.Url)]
        public string WebsiteUrl { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [StringLength(60)]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "City")]
        public string City { get; set; }

        [RequiredIfNot("SelectedCountryCode", "US", ErrorMessage = "The Region field is required.")]
        [StringLength(40)]
        [Display(Name = "Region")]
        public string Region { get; set; }

        [Display(Name = "State")]
        public IEnumerable<State> States { get; set; }

        [RequiredIf("SelectedCountryCode", "US", ErrorMessage = "The State field is required.")]
        public string SelectedStateCode { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

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

        public IList<MaterialSummary> Materials { get; set; }

        public bool IsNewEvent()
        {
            return Id == 0;
        }

        public class MaterialSummary
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class MaterialInput
        {
            [HiddenInput]
            public int RecyclingEventId { get; set; }

            [Display(Name = "Material")]
            public IEnumerable<Material> Materials { get; set; }

            [Required(ErrorMessage = "Material is required.")]
            public int[] SelectedMaterialIds { get; set; }
        }
    }
}
