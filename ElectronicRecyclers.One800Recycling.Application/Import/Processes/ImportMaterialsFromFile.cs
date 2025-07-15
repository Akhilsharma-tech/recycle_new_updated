using System;
using Microsoft.AspNetCore.Http;
using NHibernate;
using NHibernate.Criterion;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.Common;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportMaterialsFromFile : BaseImportProcess
    {
        private readonly NHibernate.ISession session;

        public ImportMaterialsFromFile(IFormFile file)
            : this(file, NHSessionProvider.CurrentSession) { }

        public ImportMaterialsFromFile(IFormFile file, NHibernate.ISession session)
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
