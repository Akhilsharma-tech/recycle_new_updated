using System.Web;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportDismantlingProcessesFromFile : BaseImportProcess
    {
        public ImportDismantlingProcessesFromFile(HttpPostedFileBase file) 
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