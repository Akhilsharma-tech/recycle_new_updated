using System.Collections.Generic;
using System.Linq;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CheckOrganizationImportsForDuplicatesInSource 
    {
        public  IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            var results = new List<Dictionary<string, object>>();

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