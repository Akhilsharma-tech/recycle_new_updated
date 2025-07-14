
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;


namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ExportOrganizationsToFile<TOrganization> : AbstractEtlProcess
        where TOrganization : EnvironmentalOrganization
    {
        private readonly EnvironmentalOrganizationsViewModel viewModel;

        public string FilePath { get; protected set; }

        public ExportOrganizationsToFile(
            EnvironmentalOrganizationsViewModel viewModel,
            string filePath)
        {
            this.viewModel = viewModel;
            FilePath = filePath;
        }

        protected override void Initialize()
        {
            Register(new GetOrganizations<TOrganization>(viewModel));
            RegisterWithProgressReporting(new SaveOrganizationsToFile(FilePath));
        }
    }
}