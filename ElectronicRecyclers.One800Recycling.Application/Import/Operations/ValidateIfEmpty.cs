using System.Collections.Generic;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ValidateIfEmpty 
    {
        private readonly string column;
        private readonly string validationColumn;
       
        public ValidateIfEmpty(string column, string validationColumn)
        {
            this.column = column;
            this.validationColumn = validationColumn;
        }

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            foreach (var row in rows)
            {
                row[validationColumn] = true;

                if (row[column] == null || string.IsNullOrWhiteSpace(row[column].ToString()))
                    row[validationColumn] = false;

                yield return row;
            }
        }
    }
}