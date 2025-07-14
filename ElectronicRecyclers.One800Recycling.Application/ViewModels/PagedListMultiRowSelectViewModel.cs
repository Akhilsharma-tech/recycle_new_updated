using Microsoft.AspNetCore.Mvc;
using ElectronicRecyclers.One800Recycling.Application.Helpers;
namespace ElectronicRecyclers.One800Recycling.Web.ViewModels
{
    public abstract class PagedListMultiRowSelectViewModel
    {
        public abstract IEnumerable<int> ItemIds { get; }

        public abstract long TotalCount { get; }

        public bool IsSelectAllChecked { get; set; }

        private IDictionary<string, bool> currentPageSelectedIds = new Dictionary<string, bool>();
        public IDictionary<string, bool> CurrentPageSelectedIds
        {
            get { return currentPageSelectedIds; }
            set { currentPageSelectedIds = value ?? new Dictionary<string, bool>(); }
        }

        [HiddenInput]
        public string SerializedSelectedIds { get; set; }

        [HiddenInput]
        public int PageIndex { get; set; }

        public long SelectedItemsTotalCount { get; set; }

        public IEnumerable<int> GetAllSelectedIds()
        {
            return new PagedListMultiRowSelector(this)
                .GetAllSelectedIds();
        }
    }
}
