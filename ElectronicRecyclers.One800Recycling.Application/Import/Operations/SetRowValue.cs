using System.Collections.Generic;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SetRowValue 
    {
        private readonly string column;
        private readonly object value;

        public SetRowValue(string column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            foreach (var row in rows)
            {
                row[column] = value;
                yield return row;
            }
        }
    }
}