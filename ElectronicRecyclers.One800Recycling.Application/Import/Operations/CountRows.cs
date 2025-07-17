using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using System.Collections.Generic;
using System.Linq;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CountRows : AbstractOperation
    {
        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            var items = rows as IList<DynamicReader> ?? rows.ToList();
            var count = items.Count();
            foreach (var row in items)
            {
                row["%rowsCount%"] = count; //TODO: I might need to refactor rows count for progress reporting purposes implementation
            }

            return items;
        }

    }
}