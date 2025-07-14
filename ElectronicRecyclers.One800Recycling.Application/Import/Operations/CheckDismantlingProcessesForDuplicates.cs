using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CheckDismantlingProcessesForDuplicates 
    {
        private readonly IEnumerable<DismantlingProcess> processes = 
             new List<DismantlingProcess>();

        public CheckDismantlingProcessesForDuplicates(IEnumerable<DismantlingProcess> processes)
        {
            this.processes = processes;
        }

        public IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string, object>> rows)
        {
            foreach (var row in rows)
            {
                row["IsDuplicate"] = processes.Any(c => 
                    c.Name.Equals((string)row["Name"], StringComparison.OrdinalIgnoreCase));

                yield return row;
            }
        }
    }
}