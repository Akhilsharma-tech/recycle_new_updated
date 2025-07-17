using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using System.Collections.Generic;
using System.Linq;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CheckOrganizationImportsForDuplicatesInSource : AbstractOperation
    {
        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            var results = new List<DynamicReader>();

            var duplicates = rows
                .GroupBy(r => new { Name = r["Name"], Address = r["Address"] })
                .Select(g => g);

            duplicates.ForEach(d => d.ForEach(r =>
            {
                r["IsDuplicate"] = false;

                if (d.Count() > 1)
                    r["IsDuplicate"] = true;

                results.Add(r);
            }));

            return results;
        }
    }
}