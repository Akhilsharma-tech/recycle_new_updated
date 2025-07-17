using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.Helpers.Attributes;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using ElectronicRecyclers.One800Recycling.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ElectronicRecyclers.One800Recycling.Application.ViewModels.SystemUserInput;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public abstract class EnvironmentalOrganizationInput : ViewModel, IAddressInput
    {
        private static IConfiguration _configuration;
        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private static string GetBlobStorageUrl()
        {
            return _configuration["RecyclerStorageLogoDirUrl"];
        }

        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(600)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Logo")]
        [DataType(DataType.Upload)]
        public IFormFile LogoFile { get; set; }

        private string logoImageUrl;
        [HiddenInput]
        public string LogoImageUrl
        {
            get
            {
                return string.IsNullOrEmpty(logoImageUrl)
                    ? string.Empty
                    : string.Format("{0}{1}", GetBlobStorageUrl(), logoImageUrl);
            }
            set
            {
                logoImageUrl = value;
            }
        }

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

        [Required]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Display(Name = "Country")]
        public IEnumerable<Country> Countries { get; set; }

        [Required(ErrorMessage = "The Country field is required.")]
        public string SelectedCountryCode { get; set; }

        [Display(Name = "Is Enabled?")]
        public bool IsEnabled { get; set; }

        public bool IsNewOrganization()
        {
            return Id == 0;
        }

        public IList<NoteSummary> Notes { get; set; }

        public IList<MaterialSummary> Materials { get; set; }

        public IList<HoursOfOperationSummary> HoursOfOperations { get; set; }

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

        public class MaterialSummary
        {
            public int Id { get; set; }

            public int MaterialId { get; set; }

            public string Name { get; set; }

            public IList<string> ResidentialDeliveryOptions { get; set; }

            public IList<string> BusinessDeliveryOptions { get; set; }
        }

        public class MaterialInput
        {
            [HiddenInput]
            public int OrganizationId { get; set; }

            [Display(Name = "Material")]
            public IEnumerable<Material> Materials { get; set; }

            public IList<MaterialDeliveryType> DeliveryOptions
            {
                get
                {
                    return Enum
                        .GetValues(typeof(MaterialDeliveryType))
                        .OfType<MaterialDeliveryType>()
                        .ToList();
                }
            }

            [Required(ErrorMessage = "Material is required.")]
            public int[] SelectedMaterialIds { get; set; }

            [Display(Name = "Business Delivery Options")]
            public IDictionary<string, bool> SelectedBusinessDeliveryOptions { get; set; }

            [Display(Name = "Residential Delivery Options")]
            public IDictionary<string, bool> SelectedResidentialDeliveryOptions { get; set; }
        }

        public class HoursOfOperationSummary
        {
            public int Id { get; set; }

            public string WeekDay { get; set; }

            public DateTime? OpenTime { get; set; }

            public DateTime? CloseTime { get; set; }

            public DateTime? AfterBreakOpenTime { get; set; }

            public DateTime? AfterBreakCloseTime { get; set; }

            public bool IsClosed { get; set; }

            public Int16 Rank { get; set; }
        }
    }
}
