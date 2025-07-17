using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using System.Collections.Generic;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ValidateHoursOfOperation : AbstractOperation
    {
        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            const string validationColumn = "IsHoursOfOperationValid";
            
            foreach (var row in rows)
            {
                row[validationColumn] = true;
                var hours = row["HoursOfOperation"] as string;

                if (string.IsNullOrWhiteSpace(hours) == false &&
                    HoursOfOperationValidator.Validate(hours) == false)
                {
                    row[validationColumn] = false;  
                }

                yield return row;
            }
        }
    }
}