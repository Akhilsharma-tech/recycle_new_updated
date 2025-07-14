using System.Web;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;


namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportMaterialComponentsFromFile : BaseImportProcess
    {
        public ImportMaterialComponentsFromFile(HttpPostedFileBase file) 
            : base(file) { }

        protected override void Initialize()
        {
            var session = NHSessionProvider.OpenStatelessSession();
            Register(new ReadFromFile<MaterialComponentRecord>(FileStream));
            Register(new CreateMaterialComponentDismantlingProcess(session));
            RegisterWithProgressReporting(new SaveMaterialComponents(session));
        }
    }
}