using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using NHibernate;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ExportMaterialsToFile : AbstractEtlProcess
    {
        public string FilePath { get; protected set; }

        private readonly ISession session;

        public ExportMaterialsToFile(ISession session, string filePath)
        {
            FilePath = filePath;
            this.session = session;
        }

        protected override void Initialize()
        {
            Register(new GetMaterials(session));
            RegisterWithProgressReporting(new SaveMaterialsToFile(FilePath));
        }
    }
}