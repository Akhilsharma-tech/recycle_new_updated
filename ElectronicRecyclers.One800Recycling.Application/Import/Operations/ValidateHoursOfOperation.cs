using System.Collections.Generic;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ValidateHoursOfOperation 
    {
        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
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