using System;
using System.Collections.Generic;
using System.Linq;





namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ValidateMaterial 
    {
        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            var dbMaterialNames = NHSessionProvider.OpenStatelessSession()
                .QueryOver<Material>()
                .List()
                .Select(m => m.Name);

            const string validationColumn = "IsMaterialValid";

            foreach (var row in rows)
            {
                row[validationColumn] = true;

                var rowValue = row["Material"] as string;

                if (string.IsNullOrWhiteSpace(rowValue) == false)
                {
                    var materialNames = rowValue.Split(new[]{','}, StringSplitOptions.RemoveEmptyEntries);

                    row[validationColumn] = materialNames.Any();
                        
                    foreach (var isValid in materialNames.Select(materialName => dbMaterialNames
                        .FirstOrDefault(dbMaterialName => dbMaterialName.Equals(
                                                    materialName, 
                                                    StringComparison.InvariantCultureIgnoreCase)) != null))
                    {
                        row[validationColumn] = isValid;

                        if (isValid == false) 
                            break;
                    }
                }

                yield return row;
            }
        }
    }
}