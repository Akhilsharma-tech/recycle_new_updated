using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ValidateMaterial 
    {
        public  IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
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