using System;
using System.Collections.Generic;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ConvertRadianToDegree 
    {
        private readonly string column;

        public ConvertRadianToDegree(string column)
        {
            this.column = column;
        }

        public  IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            foreach (var row in rows)
            {
                var value = row[column] as string;
                double radians;

                if (!string.IsNullOrEmpty(value) && double.TryParse(value, out radians))
                    row[column] = Math.Round(radians * (180.0 / Math.PI), 6);

                yield return row;
            }
        }
    }
}