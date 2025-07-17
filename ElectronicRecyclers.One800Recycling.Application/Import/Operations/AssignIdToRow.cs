using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using System.Collections.Generic;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class AssignIdToRow : AbstractOperation
    {
        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            int count = 1;
            foreach (var row in rows)
            {
                row["Id"] = count++;
                yield return row;
            }
        }
    }
}
