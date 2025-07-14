using System.Collections.Generic;
using System.Text.RegularExpressions;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class RemoveSpecialCharacters 
    {
        private readonly string column;
        public RemoveSpecialCharacters(string column)
        {
            this.column = column;
        }

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            foreach (var row in rows)
            {
                var value = row[column] as string;

                if (string.IsNullOrEmpty(value) == false)
                    row[column] = Regex.Replace(value.Trim(), @"[^\u0000-\u007F]", "");

                yield return row;
            }
        }
    }
}