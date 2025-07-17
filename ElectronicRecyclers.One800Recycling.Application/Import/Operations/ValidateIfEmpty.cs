using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using System.Collections.Generic;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ValidateIfEmpty :AbstractOperation
    {
        private readonly string column;
        private readonly string validationColumn;
       
        public ValidateIfEmpty(string column, string validationColumn)
        {
            this.column = column;
            this.validationColumn = validationColumn;
        }

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
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