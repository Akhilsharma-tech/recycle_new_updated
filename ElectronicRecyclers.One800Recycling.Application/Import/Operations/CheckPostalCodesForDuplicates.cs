using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CheckPostalCodesForDuplicates : AbstractOperation
    {

        private readonly IEnumerable<PostalCode> postalCodes = new List<PostalCode>();

        public CheckPostalCodesForDuplicates(IEnumerable<PostalCode> postalCodes)
        {
            this.postalCodes = postalCodes;
        }

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            foreach (var row in rows)
            {
                row["IsDuplicate"] = postalCodes.Any(c => 
                    c.Code == (string)row["PostalCode"]);

                yield return row;
            }
        }
    }
}