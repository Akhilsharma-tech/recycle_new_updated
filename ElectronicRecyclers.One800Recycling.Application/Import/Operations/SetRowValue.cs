using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using System.Collections.Generic;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SetRowValue : AbstractOperation
    {
        private readonly string column;
        private readonly object value;

        public SetRowValue(string column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            foreach (var row in rows)
            {
                row[column] = value;
                yield return row;
            }
        }
    }
}