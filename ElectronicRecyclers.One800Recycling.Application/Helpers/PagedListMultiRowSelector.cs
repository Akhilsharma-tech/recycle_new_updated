using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Web.ViewModels;

namespace ElectronicRecyclers.One800Recycling.Application.Helpers
{
    public class PagedListMultiRowSelector
    {
        private readonly PagedListMultiRowSelectViewModel model;

        public PagedListMultiRowSelector(PagedListMultiRowSelectViewModel model)
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