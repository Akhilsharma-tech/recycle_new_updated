using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System.Collections.Generic;
using System.Linq;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CheckMaterialCategoriesForDuplicates 
    {
        private readonly IEnumerable<MaterialCategory> categories = new List<MaterialCategory>();

        public CheckMaterialCategoriesForDuplicates(IEnumerable<MaterialCategory> categories)
        {
            this.categories = categories;
        }
 
        public  IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            foreach (var row in rows)
            {
                row["IsDuplicate"] = categories.Any(c => c.Name == (string)row["Name"]);
                yield return row;
            }
        }
    }
}