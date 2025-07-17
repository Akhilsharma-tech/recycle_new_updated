using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System.Collections.Generic;
using System.Linq;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CheckMaterialCategoriesForDuplicates : AbstractOperation
    {
        private readonly IEnumerable<MaterialCategory> categories = new List<MaterialCategory>();

        public CheckMaterialCategoriesForDuplicates(IEnumerable<MaterialCategory> categories)
        {
            this.categories = categories;
        }
 
        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            foreach (var row in rows)
            {
                row["IsDuplicate"] = categories.Any(c => c.Name == (string)row["Name"]);
                yield return row;
            }
        }
    }
}