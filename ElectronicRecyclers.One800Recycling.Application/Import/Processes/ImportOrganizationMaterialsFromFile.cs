using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;


using System.Web;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportOrganizationMaterialsFromFile<TOrganization> : BaseImportProcess
    where TOrganization : EnvironmentalOrganization
    {
        private readonly string importName;

        public ImportOrganizationMaterialsFromFile(string importName, HttpPostedFileBase file)
            : base(file)
        {
            this.importName = importName;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Register(new ReadFromFile<OrganizationMaterialsRecord>(FileStream));

            RegisterWithProgressReporting(new SaveOrganizationMaterials<TOrganization>());

        }
    }

}