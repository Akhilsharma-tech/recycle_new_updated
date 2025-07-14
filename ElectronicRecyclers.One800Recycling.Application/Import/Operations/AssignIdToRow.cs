using System.Collections.Generic;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class AssignIdToRow
    {
        public IEnumerable<Dictionary<string, object>> Execute(List<Dictionary<string, object>> rows)
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
