using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CheckDismantlingProcessesForDuplicates : AbstractOperation
    {
        private readonly IEnumerable<DismantlingProcess> processes = 
             new List<DismantlingProcess>();

        public CheckDismantlingProcessesForDuplicates(IEnumerable<DismantlingProcess> processes)
        {
            this.processes = processes;
        }

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
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