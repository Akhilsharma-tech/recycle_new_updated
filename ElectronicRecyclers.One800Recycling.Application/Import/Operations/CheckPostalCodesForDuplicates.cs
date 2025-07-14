using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CheckPostalCodesForDuplicates 
    {

        private readonly IEnumerable<PostalCode> postalCodes = new List<PostalCode>();

        public CheckPostalCodesForDuplicates(IEnumerable<PostalCode> postalCodes)
        {
            this.postalCodes = postalCodes;
        }

        public  IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
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