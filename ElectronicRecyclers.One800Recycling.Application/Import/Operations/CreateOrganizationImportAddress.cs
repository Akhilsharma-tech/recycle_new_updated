using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System.Collections.Generic;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CreateOrganizationImportAddress 
    {
        private static double ParseDouble(object value)
        {
            if (value == null)
                return 0;

            double result;
            return double.TryParse(value.ToString(), out result) ? result : 0;
        } 

        public IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            foreach (var row in rows)
            {
                row["Address"] = new Address(
                                row["AddressLine1"] as string,
                                row["AddressLine2"] as string,
                                row["City"] as string,
                                row["Region"] as string,
                                row["State"] as string,
                                row["PostalCode"] as string,
                                row["Country"] as string,
                                ParseDouble(row["Latitude"]),
                                ParseDouble(row["Longitude"]));

                yield return row;
            }
        }
    }
}