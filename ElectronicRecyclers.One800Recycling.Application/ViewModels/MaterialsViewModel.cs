using ElectronicRecyclers.One800Recycling.Domain.Common.Helper;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ElectronicRecyclers.One800Recycling.Web.ViewModels
{
    public class MaterialsViewModel : PagedListViewModel<Material>
    {
        [UIHint("SearchString")]
        [Display(Name = "Categories:")]
        public int[] SearchCategoryIds { get; set; }

        public IEnumerable<MaterialCategory> Categories { get; set; }
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
        public PagedList<Materialsummary> Items { get; set; }
        public IEnumerable<int> ItemIds
        {
            get
            {
                return Items == null
                    ? new List<int>()
                    : Items.Items.Select(i => i.Id);
            }
        }

        public long TotalCount
        {
            get { return Items == null ? 0 : Items.TotalCount; }
        }
        public class Materialsummary
        {
            public int Id { get; set; }

        }
        public IEnumerable<int> GetAllSelectedIds()
        {
            return new PagedListMultiRowSelectorMaterialsViewModel(this)
                .GetAllSelectedIds();
        }
    }
    public class PagedListMultiRowSelectorMaterialsViewModel
    {
        private readonly MaterialsViewModel model;

        public PagedListMultiRowSelectorMaterialsViewModel(MaterialsViewModel model)
        {
            this.model = model;
        }

        private static ICollection<string> Deserialize(string serializedIds)
        {
            return (serializedIds ?? string.Empty)
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }

        private static string Serialize(IEnumerable<string> ids)
        {
            return string.Join(",", ids);
        }

        private static void UpdateAllSelectedIds(
            ICollection<string> allSelectedIds,
            IEnumerable<KeyValuePair<string, bool>> selectedIds)
        {
            selectedIds.ForEach(id =>
            {
                var isInList = allSelectedIds.Contains(id.Key);
                if (isInList && id.Value == false)
                {
                    allSelectedIds.Remove(id.Key);
                }
                else if (isInList == false && id.Value)
                {
                    allSelectedIds.Add(id.Key);
                }

            });
        }

        private static void UpdateCurrentPageSelectedIds(
            IDictionary<string, bool> selectedIds,
            ICollection<string> allSelectedIds,
            IEnumerable<int> itemIds)
        {
            itemIds.ForEach(itemId =>
            {
                var id = itemId.ToString();
                if (allSelectedIds.Contains(id))
                    selectedIds.Add(id, true);
            });
        }

        public void SelectRows()
        {
            var selectedIds = model.CurrentPageSelectedIds ?? new Dictionary<string, bool>();
            var itemIds = model.ItemIds;

            if (model.IsSelectAllChecked)
            {
                selectedIds = new Dictionary<string, bool>();
                itemIds.ForEach(id => selectedIds.Add(id.ToString(), true));

                model.SelectedItemsTotalCount = model.TotalCount;
                model.SerializedSelectedIds = string.Empty;
            }
            else
            {
                var allSelectedIds = Deserialize(model.SerializedSelectedIds);

                UpdateAllSelectedIds(allSelectedIds, selectedIds);

                selectedIds = new Dictionary<string, bool>();
                UpdateCurrentPageSelectedIds(selectedIds, allSelectedIds, itemIds);

                model.SelectedItemsTotalCount = allSelectedIds.Count;
                model.SerializedSelectedIds = Serialize(allSelectedIds);
            }

            model.CurrentPageSelectedIds = selectedIds;
        }

        public IEnumerable<int> GetAllSelectedIds()
        {
            SelectRows();

            return Deserialize(model.SerializedSelectedIds)
                .Select(int.Parse);
        }
    }
}
