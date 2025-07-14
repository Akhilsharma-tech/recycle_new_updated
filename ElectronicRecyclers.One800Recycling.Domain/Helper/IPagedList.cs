namespace ElectronicRecyclers.One800Recycling.Domain.Common.Helper
{
    public interface IPagedList
    {
        long TotalCount { get; set; }

        int TotalPages { get; set; }

        int PageIndex { get; set; }

        int PageSize { get; set; }

        int FirstItem { get; }

        int LastItem { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }
    }
}
