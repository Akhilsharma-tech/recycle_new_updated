using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ExportOrganizationMaterialsToFile<TOrganization> : AbstractEtlProcess
        where TOrganization : EnvironmentalOrganization
    {
        private readonly EnvironmentalOrganizationsViewModel viewModel;
        public string FilePath { get; protected set; }
        public ExportOrganizationMaterialsToFile(
            EnvironmentalOrganizationsViewModel viewModel,
            string filePath)
        {
            this.viewModel = viewModel;
            FilePath = filePath;
        }

        protected override void Initialize()
        {
            Register(new GetOrganizationMaterials<TOrganization>(viewModel));
            RegisterWithProgressReporting(new SaveOrganizationsMaterialsToFile(FilePath));
        }
    }
}