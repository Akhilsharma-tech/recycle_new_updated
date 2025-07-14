using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ElectronicRecyclers.One800Recycling.Domain.HtmlCustomControlObjects
{
    public class Pager
    {
        public IList<PagerItem> Items { get; set; }

        public PagerItem PreviousPage { get; set; }

        public PagerItem NextPage { get; set; }

        public PagerItem FirstPage { get; set; }

        public PagerItem LastPage { get; set; }

        public long TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<int> PageSizes
        {
            get { return new List<int> {10, 25, 50, 100, 200, 300}; }
        } 

        public FormMethod FormMethod { get; set; }

        public int GetVisibleRowsStartIndex()
        {
            if (PageIndex <= 0 || TotalPages == 0)
                return 0;

            if (PageIndex > TotalPages)
                return 1;

            return ((PageIndex * PageSize) - PageSize) + 1;
        }

        private int CalculateVisibleRowsOnPage()
        {
            if (TotalCount <= 0)
                return 0;

            if (PageIndex < TotalPages)
                return PageSize;

            return (int)(TotalCount - ((PageIndex - 1) * PageSize));
        }

        public int GetVisibleRowsEndIndex()
        {
            if (PageIndex <= 0 || TotalPages == 0)
                return 0;

            if (PageIndex > TotalPages)
                return PageSize;

            return ((PageIndex - 1) * PageSize) + CalculateVisibleRowsOnPage();
        }

        private string action;
        public string ControllerAction
        {
            get { return string.IsNullOrEmpty(action) ? "Index" : action; }
            set { action = value; }
        }

        private string pageIndexHiddenFieldId;
        public string PageIndexHiddenFieldId
        {
            get
            {
                return string.IsNullOrEmpty(pageIndexHiddenFieldId) 
                    ? "PageIndex" 
                    : pageIndexHiddenFieldId;
            }
            set { pageIndexHiddenFieldId = value; }
        }
    }
}