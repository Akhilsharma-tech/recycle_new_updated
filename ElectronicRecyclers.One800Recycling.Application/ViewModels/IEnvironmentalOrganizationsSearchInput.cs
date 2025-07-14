namespace ElectronicRecyclers.One800Recycling.Web.ViewModels
{
    public interface IEnvironmentalOrganizationsSearchInput
    {
        string SearchName { get; set; }

        string SearchImportName { get; set; }

        string SearchCity { get; set; }

        string SearchState { get; set; }

        string SearchPostalCode { get; set; }

        string SearchCountry { get; set; }

        bool IsSelectAllChecked { get; set; }

        IDictionary<string, bool> CurrentPageSelectedIds { get; set; }

        IEnumerable<int> GetAllSelectedIds();
    }
}
