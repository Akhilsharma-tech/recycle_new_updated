using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveMaterialsViewsToFile : AbstractOperation
    {
        private readonly string filePath;

        public SaveMaterialsViewsToFile(string filePath)
        {
            this.filePath = filePath;
        }

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            var engine = new FileHelperEngine<MaterialRecord>();
            engine.HeaderText = "Material_Id\tName\tDescription\tSearchKeywords\tCategories\tIsActive";

            using (var file = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var row in rows)
                {
                    var record = new MaterialRecord
                    {
                        MaterialId = row["MaterialId"]?.ToString()??"",
                        Name = row["MaterialName"]?.ToString() ?? "",
                        Description = row["Description"]?.ToString()?.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ") ?? "",
                        SearchKeywords = string.Join(",", (row["SearchKeywords"] as IEnumerable<string>) ?? new List<string>()),
                        Categories= row["Category"]?.ToString() ?? "",
                        IsActive = row["IsActive"]?.ToString() ?? "",
                    };

                    string line = engine.WriteString(new[] { record });
                    file.Write(line);
                }

                yield break;
            }
        }
    }

}