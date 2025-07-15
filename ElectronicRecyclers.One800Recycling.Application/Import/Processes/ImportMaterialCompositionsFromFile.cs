using System.Web;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using Microsoft.AspNetCore.Http;


namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportMaterialCompositionsFromFile : BaseImportProcess
    {
         public ImportMaterialCompositionsFromFile(IFormFile file)
            : this(file, NHSessionProvider.CurrentSession) { }

        private readonly NHibernate.ISession session;
        public ImportMaterialCompositionsFromFile(IFormFile file, NHibernate.ISession session) 
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