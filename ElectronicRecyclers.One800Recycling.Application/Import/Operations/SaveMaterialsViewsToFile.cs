using ElectronicRecyclers.One800Recycling.Application.Import.Records;



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveMaterialsViewsToFile 
    {
        private readonly string filePath;

        public SaveMaterialsViewsToFile(string filePath)
        {
            this.filePath = filePath;
        }

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            var engine = FluentFile.For<MaterialRecord>();
            engine.HeaderText = "Material_Id\tName\tDescription\tSearchKeywords\tCategories\tIsActive";

            using (var file = engine.To(filePath))
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

                    file.Write(record);
                }

                yield break;
            }
        }
    }

}