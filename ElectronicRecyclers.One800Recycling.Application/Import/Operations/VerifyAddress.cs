using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using ElectronicRecyclers.One800Recycling.Web.Services;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class VerifyAddress 
    {
        public  IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            var addresses = new Dictionary<int, Address>();
            var resultRows = new Dictionary<int, Row>();
            const string validationColumn = "IsAddressValid";
            var key = 1;

            foreach (var row in rows)
            {
                row[validationColumn] = true;

                if (row["Country"].ToString() != "US")
                    continue;

                addresses.Add(key, (Address)row["Address"]);
                resultRows.Add(key++, row);
            }

            const int batchSize = 100;
            for (var i = 0; i < addresses.Count; i += batchSize)
            {
                var batch = addresses.Skip(i).Take(batchSize).ToDictionary(x => x.Key, x => x.Value);

                var smartyResults = SmartyStreetsService.VerifyAddress(batch)
                    .ToDictionary(x => x.Key, x => x.Value);

                AddressVerified?.Invoke(this, i + batchSize);

                foreach (var kvp in batch)
                {
                    int rowKey = kvp.Key;
                    Address originalAddress = kvp.Value;
                    Row row = resultRows[rowKey];

                    Address verifiedAddress = null;

                    if (smartyResults.TryGetValue(rowKey, out var ssAddress) && ssAddress != null)
                    {
                        verifiedAddress = ssAddress;
                    }
                    else
                    {
                        verifiedAddress = GoogleGeocodingService.GeocodeAddress(
                            originalAddress.AddressLine1,
                            originalAddress.City,
                            originalAddress.State,
                            originalAddress.PostalCode,
                            originalAddress.Country
                        );
                    }

                    row[validationColumn] = verifiedAddress != null;

                    if (verifiedAddress != null)
                        row["Address"] = verifiedAddress;
                }
            }

            return resultRows.Values;
        }



        public event EventHandler<int> AddressVerified;
    }
}