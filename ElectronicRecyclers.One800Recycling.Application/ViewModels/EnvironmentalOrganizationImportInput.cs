using ElectronicRecyclers.One800Recycling.Application.Helpers.Attributes;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using ElectronicRecyclers.One800Recycling.Web.Helpers.Attributes;
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
    public class EnvironmentalOrganizationImportInput : ViewModel, IAddressInput
    {
        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public string OrganizationType { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(600)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Url]
        [StringLength(255)]
        [Display(Name = "Website Address")]
        [DataType(DataType.Url)]
        public string WebsiteUrl { get; set; }

        public IEnumerable<PhoneCountryCode> PhoneCountryCoodes { get; set; }

        [RequiredIfNot("PhoneNumber", "", ErrorMessage = "Phone country code is required.")]
        [Display(Name = "Phone Country Code")]
        public string SelectedPhoneCountryCode { get; set; }

        private string phoneNumber;

        [Phone]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = (value == "0") ? string.Empty : value; }
        }

        [RequiredIfNot("FaxNumber", "", ErrorMessage = "Fax country code is required.")]
        [Display(Name = "Fax Country Code")]
        public string SelectedFaxCountryCode { get; set; }

        private string faxNumber;

        [Phone]
        [Display(Name = "Fax")]
        [DataType(DataType.PhoneNumber)]
        public string FaxNumber
        {
            get { return faxNumber; }
            set { faxNumber = (value == "0") ? string.Empty : value; }
        }

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

        [HoursOfOperation]
        [StringLength(300)]
        [Display(Name = "Business Hours")]
        public string HoursOfOperation { get; set; }

        [Display(Name = "Materials")]
        public IList<string> Materials { get; set; }

        [Display(Name = "Private Note")]
        [DataType(DataType.MultilineText)]
        public string PrivateNote { get; set; }

        [Display(Name = "Public Note")]
        [DataType(DataType.MultilineText)]
        public string PublicNote { get; set; }

        public Address GetAddress()
        {
            return new Address(
                AddressLine1,
                AddressLine2,
                City,
                Region,
                SelectedStateCode,
                PostalCode,
                SelectedCountryCode,
                Latitude,
                Longitude);
        }
    }
}
