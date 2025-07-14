using System.Collections.Generic;
using System.Linq;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CountRows 
    {
        public  IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            var items = rows as IList<Dictionary<string, object>> ?? rows.ToList();
            var count = items.Count();
            foreach (var row in items)
            {
                row["%rowsCount%"] = count; //TODO: I might need to refactor rows count for progress reporting purposes implementation
            }

            return items;
        }
    }
}