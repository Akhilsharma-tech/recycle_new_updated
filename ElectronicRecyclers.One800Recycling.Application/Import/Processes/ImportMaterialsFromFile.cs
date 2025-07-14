using System.Web;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;


using NHibernate;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportMaterialsFromFile : BaseImportProcess
    {
        public ImportMaterialsFromFile(HttpPostedFileBase file)
            : this(file, NHSessionProvider.CurrentSession) { }

        private readonly ISession session;
        public ImportMaterialsFromFile(HttpPostedFileBase file, ISession session) 
            : base(file)
        {
            this.session = session;
        }

        protected override void Initialize()
        {
            Register(new ReadFromFile<MaterialRecord>(FileStream));
            Register(new CheckMaterialsForDuplicates(session.QueryOver<Material>().List()));
            RegisterWithProgressReporting(new SaveMaterials(session));
        }
    }
}