//using System.ComponentModel.DataAnnotations;
//using System.Text.Json.Serialization;

//namespace ElectronicRecyclers.One800Recycling.Web.ViewModels
//{
//    public class OrganizationChangeWrapper
//    {
//        public int OrganizationId { get; set; }
//        public Changes Changes { get; set; }
//    }

//    public class Changes
//    {
//        [JsonPropertyName("organizationChanges")]
//        public OrganizationChanges OrganizationChanges { get; set; }

//        public List<MaterialSummary> Materials { get; set; }

//        [JsonPropertyName("HoursOfOperations")]
//        public List<HoursOfOperationSummary> HoursOfOperations { get; set; }

//        public List<NoteSummary> Notes { get; set; }

//        public enum MaterialEditModes
//        {
//            Append = 1,
//            Overwrite = 2,
//            Remove = 3
//        }

//        [Display(Name = "Material Edit Mode?")]
//        public MaterialEditModes MaterialEditMode { get; set; }


//    }

//    public class OrganizationChanges
//    {
//        public string Name { get; set; }
//        public string Description { get; set; }
//        public string WebsiteUrl { get; set; }
//        [JsonPropertyName("Telephone")]
//        public PhoneSummary PhoneNumber { get; set; }
//        public int? SelectedPhoneCountryCode { get; set; }
//        [JsonPropertyName("Fax")]
//        public PhoneSummary FaxNumber { get; set; }
//        public int? SelectedFaxCountryCode { get; set; }
//        public bool? IsEnabled { get; set; }
//        public bool? IsNameSelected { get; set; }
//        public bool? IsDescriptionSelected { get; set; }
//        public bool? IsLogoImageSelected { get; set; }
//        public bool? IsWebsiteUrlSelected { get; set; }
//        public bool? IsPhoneSelected { get; set; }
//        public bool? IsFaxSelected { get; set; }
//        public bool? IsEnabledSelected { get; set; }

//    }
//    public class PhoneSummary
//    {
//        public string CountryCodeSource { get; set; }
//        public int CountryCode { get; set; }
//        public long Number { get; set; }
//        public string Extension { get; set; }
//    }
//}
