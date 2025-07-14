using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System.Collections.Generic;
using System.Linq;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CheckMaterialsForDuplicates 
    {
        private readonly IEnumerable<Material> materials = new List<Material>();

        public CheckMaterialsForDuplicates(IEnumerable<Material> materials)
        {
            this.materials = materials;
        }
 
        public IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            foreach (var row in rows)
            {
                row["IsDuplicate"] = materials.Any(c => c.Name == (string)row["Name"]);
                yield return row;
            }
        }
    }
}