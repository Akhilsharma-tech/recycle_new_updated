using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using ElectronicRecyclers.One800Recycling.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public class EnvironmentalOrganizationComparisonResultsViewModel : ViewModel
    {
        public string ImportListImportBatch { get; set; }

        public string ActiveListImportBatch { get; set; }

        public bool IsAddTabActive { get; set; }

        public bool IsUpdateTabActive { get; set; }

        public bool IsDeleteTabActive { get; set; }

        public OrganizationsViewModel WillBeAddedOrganizationsModel { get; set; }

        public OrganizationsViewModel WillBeUpdatedOrganizationsModel { get; set; }

        public OrganizationsViewModel WillBeDisabledOrganizationsModel { get; set; }

        public class OrganizationsViewModel : PagedListMultiRowSelectViewModel
        {
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
        }

        public class OrganizationSummary
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public Address Address { get; set; }

            public Phone Telephone { get; set; }

            public Phone Fax { get; set; }

            public string WebsiteUrl { get; set; }

            public string ImportBatchName { get; set; }
        }
    }
}
