using System.Web;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using Microsoft.AspNetCore.Http;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportDismantlingProcessesFromFile : BaseImportProcess
    {
        public ImportDismantlingProcessesFromFile(IFormFile file) 
            : base(file) { }

        protected override void Initialize()
        {
            var session = NHSessionProvider.OpenStatelessSession();
            var processes = session
                .QueryOver<DismantlingProcess>()
                .List();

            Register(new ReadFromFile<DismantlingProcessRecord>(FileStream));
            Register(new CheckDismantlingProcessesForDuplicates(processes));
            RegisterWithProgressReporting(new SaveDismantlingProcesses(session));
        }
    }
}