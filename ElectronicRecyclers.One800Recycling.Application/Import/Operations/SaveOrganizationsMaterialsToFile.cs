using ElectronicRecyclers.One800Recycling.Application.Import.Records;




using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveOrganizationsMaterialsToFile 
    {
        private readonly string filePath;
        public SaveOrganizationsMaterialsToFile(string filePath)
        {
            this.filePath = filePath;
        }

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            var engine = FluentFile.For<OrganizationMaterialsRecord>();
            engine.HeaderText = "OrganizationId\tMaterialId\tMaterialName\tMaterialBusinessDeliveryOption\tMaterialResidentialDeliveryOption";


            using (var file = engine.To(filePath))
            {
                foreach (var row in rows)
                {
                    var record = new OrganizationMaterialsRecord
                    {
                        OrganizationId = (int)row["OrganizationId"],
                        MaterialId = row["MaterialId"]?.ToString(),
                        MaterialName = row["MaterialName"]?.ToString(),
                        MaterialBusinessDeliveryOption = row["MaterialBusinessDeliveryOption"]?.ToString(),
                        MaterialResidentialDeliveryOption = row["MaterialResidentialDeliveryOption"]?.ToString()

                    };

                    file.Write(record);
                }

                yield break;
            }
        }
    }
}