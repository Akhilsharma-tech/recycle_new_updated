using System.Collections.Generic;




using ElectronicRecyclers.One800Recycling.Application.Import.Records;
using System;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveOrganizationsToFile 
    {
        private readonly string filePath;

        public SaveOrganizationsToFile(string filePath)
        {
            this.filePath = filePath;
        }

        private static string GetPhoneNumber(object obj)
        {
            var phone = obj as Phone;
            if (phone == null || phone.Number == 0)
                return string.Empty;

            return phone.Number.ToString();
        }

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            var engine = FluentFile.For<EnvironmentalOrganizationUpdateRecord>();
            engine.HeaderText = "Id\tName\tDescription\tAddressLine1\tAddressLine2\tCity\tRegion\tState\t";
            engine.HeaderText += "PostalCode\tCountry\tTelephone\tFax\tWebsiteUrl\tHoursOfOperation\tIsEnabled\tPrivateNote\tPublicNote";

            using (var file = engine.To(filePath))
            {
                foreach (var row in rows)
                {
                    var record = new EnvironmentalOrganizationUpdateRecord
                    {
                        Id = long.TryParse(row["Id"]?.ToString(), out var idVal) ? idVal : 0,
                        Name = NormalizeText(row["Name"] as string),
                        Description = NormalizeText(row["Description"] as string),
                        WebsiteUrl = NormalizeText(row["WebsiteUrl"] as string),
                        Telephone = GetPhoneNumber(row["Telephone"]),
                        Fax = GetPhoneNumber(row["Fax"])
                    };

                    var address = (Address)row["Address"];
                    record.AddressLine1 = NormalizeText(address.AddressLine1);
                    record.AddressLine2 = NormalizeText(address.AddressLine2);
                    record.City = NormalizeText(address.City);
                    record.Region = NormalizeText(address.Region);
                    record.State = NormalizeText(address.State);
                    record.PostalCode = NormalizeText(address.PostalCode);
                    record.Country = NormalizeText(address.Country);
                    record.HoursOfOperation = row["HoursOfOperation"]?.ToString();
                    record.IsEnabled = row["IsEnabled"]?.ToString();
                    record.PrivateNote = row["PrivateNote"]?.ToString();
                    record.PublicNote = row["PublicNote"]?.ToString();

                    file.Write(record);
                }

                yield break;
            }
        }
        private string NormalizeText(string input)
        {
            return string.IsNullOrWhiteSpace(input)
                ? input
                : input.Replace("\r", " ").Replace("\n", " ").Trim();
        }


    }
}