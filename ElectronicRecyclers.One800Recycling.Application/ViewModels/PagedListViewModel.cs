using ElectronicRecyclers.One800Recycling.Domain.Common.Helper;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicRecyclers.One800Recycling.Web.ViewModels
{
    public class PagedListViewModel<T> : ViewModel
    {
        public PagedList<T> PagedList { get; set; }

        [HiddenInput]
        public int PageIndex { get; set; }

        [HiddenInput]
        public int PageSize { get; set; }

        [HiddenInput]
        public string SortOrderField { get; set; }

        [HiddenInput]
        public bool SortOrderAscending { get; set; }
    }
}
