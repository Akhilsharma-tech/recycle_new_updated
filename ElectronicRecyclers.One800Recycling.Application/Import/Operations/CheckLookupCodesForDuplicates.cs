using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CheckLookupCodesForDuplicates<T> : AbstractOperation where T : LookupCode
    {
        private readonly IEnumerable<T> lookups = new List<T>();
 
        public CheckLookupCodesForDuplicates(IEnumerable<T> lookups)
        {
            this.lookups = lookups;
        }

        public override  IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            foreach (var row in rows)
            {
                row["IsDuplicate"] = lookups.Any(c => c.Code == (string)row["Code"]);
                yield return row;
            }
        }
    }
}