using ElectronicRecyclers.One800Recycling.Domain.Common.Helper;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ElectronicRecyclers.One800Recycling.Web.ViewModels
{
    public class EnvironmentalOrganizationsViewModel
        : PagedListMultiRowSelectViewModel,
        IEnvironmentalOrganizationsSearchInput
    {
        public EnvironmentalOrganizationsViewModel()
        {
            GroupName = "DUMMY THINGY"; //It is done to prevent validation message from displaying before submit.
        }

        public PagedList<OrganizationSummary> Items { get; set; }

        public override IEnumerable<int> ItemIds
        {
            get
            {
                return Items == null
                    ? new List<int>()
                    : Items.Items.Select(i => i.Id);
            }
        }

        public override long TotalCount
        {
            get { return Items == null ? 0 : Items.TotalCount; }
        }

        public string OrganizationType { get; set; }

        [HiddenInput]
        public string SortOrderField { get; set; }

        [HiddenInput]
        public bool SortOrderAscending { get; set; }

        [UIHint("SearchString")]
        [Display(Name = "Name:")]
        public string SearchName { get; set; }

        [UIHint("SearchString")]
        [Display(Name = "Import:")]
        public string SearchImportName { get; set; }

        public IEnumerable<string> ImportNames { get; set; }

        [UIHint("SearchString")]
        [Display(Name = "Street:")]
        public string SearchStreet { get; set; }

        [UIHint("SearchString")]
        [Display(Name = "City:")]
        public string SearchCity { get; set; }

        public IEnumerable<State> States { get; set; }

        [UIHint("SearchString")]
        [Display(Name = "State:")]
        public string SearchState { get; set; }

        [UIHint("SearchString")]
        [Display(Name = "Postal Code:")]
        public string SearchPostalCode { get; set; }

        [UIHint("SearchString")]
        [Display(Name = "Country:")]
        public string SearchCountry { get; set; }

        public IEnumerable<Country> Countries { get; set; }

        [UIHint("SearchString")]
        [Display(Name = "Group:")]
        public int SearchGroupId { get; set; }

        public IEnumerable<InformationVerificationGroup> Groups { get; set; }

        [UIHint("SearchString")]
        [Display(Name = "Materials:")]
        public int[] SearchMaterialIds { get; set; }

        public IEnumerable<Material> Materials { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string GroupName { get; set; }

        [UIHint("SearchBoolean")]
        [Display(Name = "Enabled:")]
        public bool? SearchIsEnabled { get; set; }

        public class OrganizationSummary
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public Phone Phone { get; set; }

            public Phone Fax { get; set; }

            public string WebsiteUrl { get; set; }

            public string ImportBatchName { get; set; }

            public IEnumerable<string> Groups { get; set; }

            public Address Address { get; set; }

            public bool IsEnabled { get; set; }

            public IEnumerable<HoursOfOperation> HoursOfOperations { get; set; }

            public IEnumerable<Material> Materials { get; set; }
        }
    }
}
