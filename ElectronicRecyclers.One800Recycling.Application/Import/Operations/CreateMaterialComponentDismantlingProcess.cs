using System.Collections.Generic;
using System.Linq;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CreateMaterialComponentDismantlingProcess 
    {
        private readonly IStatelessSession session;
        public CreateMaterialComponentDismantlingProcess(IStatelessSession session)
        {
            this.session = session;
        }

        public  IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            var processes = session
                .QueryOver<DismantlingProcess>()
                .List();

            foreach (var row in rows)
            {
                var virginProductionProcess = processes
                    .FirstOrDefault(p => p.Name == row["VirginProductionProcess"] as string);

                var recyclingProcess = processes
                    .FirstOrDefault(p => p.Name == row["RecyclingProcess"] as string);

                var landfillingProcess = processes
                    .FirstOrDefault(p => p.Name == row["LandfillingProcess"] as string);

                var incinerationProcess = processes
                    .FirstOrDefault(p => p.Name == row["IncinerationProcess"] as string);

                if (virginProductionProcess == null 
                    || recyclingProcess == null
                    || landfillingProcess == null
                    || incinerationProcess == null)
                    continue;

                row["DismantlingProcess"] = new MaterialComponentDismantlingProcess(
                    virginProductionProcess,
                    recyclingProcess,
                    landfillingProcess,
                    incinerationProcess);

                yield return row;
            }
        }
    }
}