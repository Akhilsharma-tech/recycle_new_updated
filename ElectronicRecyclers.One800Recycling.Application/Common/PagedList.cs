using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElectronicRecyclers.One800Recycling.Application.Common
{
    [Serializable]
    public class PagedList<T> : IPagedList
    {
        [JsonProperty("items")]
        public IList<T> Items { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("page_index")]
        public int PageIndex { get; set; }

        [JsonProperty("page_size")]
        public int PageSize { get; set; }

        public PagedList()
        {
            Items = new List<T>();
        }

        public PagedList(IEnumerable<T> source, long totalCount, int pageIndex, int pageSize)
        {
            Items = new List<T>(source);
            TotalCount = totalCount;
            PageIndex = pageIndex <= 0  ? 1 : pageIndex;
            PageSize = pageSize <= 0 ? 10 : pageSize;
            TotalPages = (int)(TotalCount / PageSize);

            if (TotalCount % PageSize > 0)
                TotalPages++;
        }

        [JsonProperty("has_previous_page")]
        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 1;
            }
        }

        [JsonProperty("has_next_page")]
        public bool HasNextPage
        {
            get
            {
                return PageIndex < TotalPages;
            }
        }

        [JsonProperty("first_item")]
        public int FirstItem
        {
            get 
            {
                return (PageIndex * PageSize);
            }
        }

        [JsonProperty("last_item")]
        public int LastItem
        {
            get 
            {
                return FirstItem + PageSize;
            }
        }
    }

    public static class Pagination
    {
        public static PagedList<T> ToPageList<T>(
            this IEnumerable<T> source, 
            int totalCount, 
            int pageIndex,
            int pageSize)
        {
            return new PagedList<T>(source, totalCount, pageIndex, pageSize);
        }
    }
}