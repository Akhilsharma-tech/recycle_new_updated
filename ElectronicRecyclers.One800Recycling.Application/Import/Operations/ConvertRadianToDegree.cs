using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using System;
using System.Collections.Generic;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ConvertRadianToDegree : AbstractOperation
    {
        private readonly string column;

        public ConvertRadianToDegree(string column)
        {
            this.column = column;
        }

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
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