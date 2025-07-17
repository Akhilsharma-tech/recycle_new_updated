using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Application.Import.Processes;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveOrganizationsMaterialsToFile : AbstractOperation
    {
        private readonly string filePath;
        public SaveOrganizationsMaterialsToFile(string filePath)
        {
            this.filePath = filePath;
        }

        public override  IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            var engine = new FileHelperEngine<OrganizationMaterialsRecord>();
            engine.HeaderText = "OrganizationId\tMaterialId\tMaterialName\tMaterialBusinessDeliveryOption\tMaterialResidentialDeliveryOption";

            using (var file = new StreamWriter(filePath, false, Encoding.UTF8))
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
                    string line = engine.WriteString(new[] { record });
                    file.Write(record);
                }

                yield break;
            }
        }
    }
}