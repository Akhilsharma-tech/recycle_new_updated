using System.Web;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;






namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportOrganizationsFromFile<TOrganization> : BaseImportProcess
        where TOrganization : EnvironmentalOrganization
    {
        private readonly string importName;

        public ImportOrganizationsFromFile(string importName, HttpPostedFileBase file) 
            : base(file)
        {
            this.importName = importName;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Register(new ReadFromFile<EnvironmentalOrganizationRecord>(FileStream));
            Register(new CheckIsEnabledForOrganizationImport("IsEnabled", "IsEnabled"));
            Register(new AssignIdToRow());
            Register(new SetRowValue("Type", typeof (TOrganization).Name));
            Register(new SetRowValue("ImportBatchName", importName));
            Register(new ValidateIfEmpty("Name", "IsNameValid"));
            Register(new ValidatePhone("Telephone", "Country", "IsTelephoneValid"));
            Register(new ValidatePhone("Fax", "Country", "IsFaxValid"));
            Register(new ValidateUrl("WebsiteUrl", "IsWebsiteUrlValid"));
            Register(new ValidateHoursOfOperation());
            Register(new CreateOrganizationImportAddress());
            Register(new VerifyAddress());
            Register(new CheckOrganizationImportsForDuplicatesInSource());
            Register(new CheckOrganizationsForDuplicates<EnvironmentalOrganizationImport>("IsDuplicate"));
            RegisterWithProgressReporting(new SaveOrganizationImports(NHSessionProvider.OpenStatelessSession()));
        }
    }
}