using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicRecyclers.One800Recycling.Domain.HtmlCustomControlObjects
{
    public class GroupedSelectList 
    {
        public IEnumerable Items { get; private set; }
 
        public string DataValueField { get; private set; }

        public string DataTextField { get; private set; }

        public string DataGroupField { get; private set; }

        public GroupedSelectList(
            IEnumerable items,
            string dataValueField,
            string dataTextField,
            string dataGroupField)
        {
            Items = items;
            DataValueField = dataValueField;
            DataTextField = dataTextField;
            DataGroupField = dataGroupField;
        }

        //public IEnumerator<GroupedSelectListItem> GetEnumerator()
        //{
        //    var items = new List<GroupedSelectListItem>();
        //    foreach (var item in Items)
        //    {
        //       yield return new GroupedSelectListItem
        //       {
        //           Group = item
        //       }
        //    }
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return Items.GetEnumerator();
        //}
    }
}