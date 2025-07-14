using System.Web;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;

using NHibernate;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportMaterialCompositionsFromFile : BaseImportProcess
    {
         public ImportMaterialCompositionsFromFile(HttpPostedFileBase file)
            : this(file, NHSessionProvider.CurrentSession) { }

        private readonly ISession session;
        public ImportMaterialCompositionsFromFile(HttpPostedFileBase file, ISession session) 
            : base(file)
        {
            this.session = session;
        }

        protected override void Initialize()
        {
            Register(new ReadFromFile<MaterialCompositionRecord>(FileStream));
            Register(new CreateMaterialCompositionPropertyToMaterialComponentMap(session));
            RegisterWithProgressReporting(new CreateMaterialComposition(NHSessionProvider.OpenSession()));
        }
    }
}