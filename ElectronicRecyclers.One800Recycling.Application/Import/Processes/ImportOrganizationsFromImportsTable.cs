using System.Collections.Generic;

using ElectronicRecyclers.One800Recycling.Web.Infrastructure.Logging;

using ElectronicRecyclers.One800Recycling.Application.Import.Operations;

using ElectronicRecyclers.One800Recycling.Domain.Entities;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportOrganizationsFromImportsTable<T> : AbstractEtlProcess
        where T : EnvironmentalOrganization
    {
        private readonly EnvironmentalOrganizationsViewModel viewModel;

        public ImportOrganizationsFromImportsTable(EnvironmentalOrganizationsViewModel viewModel) 
        {
            this.viewModel = viewModel;
        }

        public ImportOrganizationsFromImportsTable(IEnumerable<int> ids)
        {
            var selectedIds = new Dictionary<string, bool>();
            ids.ForEach(id => selectedIds.Add(id.ToString(), true));

            viewModel = new EnvironmentalOrganizationsViewModel
            {
                CurrentPageSelectedIds = selectedIds
            };
        }

        protected override void Initialize()
        {
            LogManager.GetLogger().Info("Start moving organizations from import list.");
            Register(new GetOrganizationImports<T>(viewModel));
            Register(new CheckIsEnabledForOrganizationImport("IsEnabled", "IsEnabled"));
            
            Register(new CheckOrganizationsForDuplicates<T>("IsDuplicateOrganizationFoundDuringMoveOperation"));
            
            Register(new MarkOrganizationImportAsDuplicateIfOrganizationExist());
          
            RegisterWithProgressReporting(new SaveOrganizations<T>());
        }
    }
}