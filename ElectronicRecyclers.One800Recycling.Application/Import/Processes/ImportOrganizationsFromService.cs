using System;
using System.Collections.Generic;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ServiceObjects;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;


using ElectronicRecyclers.One800Recycling.Web.Models.ServiceObjects;


namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportOrganizationsFromService<TOrganization> : AbstractEtlProcess
        where TOrganization : EnvironmentalOrganization
    {
        private readonly string importName;
        private readonly Func<IEnumerable<IServiceEnvironmentalOrganization>> service;

        public ImportOrganizationsFromService(
            string importName,
            Func<IEnumerable<IServiceEnvironmentalOrganization>> service)
        {
            this.importName = importName;
            this.service = service;
        }

        protected override void Initialize()
        {
            //TODO: Refactor because the same list of operations used when reading from file
            Register(new ReadFromService(service));
            Register(new AssignIdToRow());
            Register(new SetRowValue("Type", typeof(TOrganization).Name));
            Register(new SetRowValue("ImportBatchName", importName));
            Register(new ValidateIfEmpty("Name", "IsNameValid"));
            Register(new ValidatePhone("Telephone", "Country", "IsTelephoneValid"));
            Register(new ValidatePhone("Fax", "Country", "IsFaxValid"));
            Register(new ValidateUrl("WebsiteUrl", "IsWebsiteUrlValid"));
            Register(new FormatHoursOfOperations());
            Register(new ValidateHoursOfOperation());
            Register(new CreateOrganizationImportAddress());
            Register(new VerifyAddress());
            Register(new CheckOrganizationImportsForDuplicatesInSource());
            
            Register(new CheckOrganizationsForDuplicates<EnvironmentalOrganizationImport>("IsDuplicate"));
            RegisterWithProgressReporting(new SaveOrganizationImports(NHSessionProvider.OpenStatelessSession()));
        }
    }
}