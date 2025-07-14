using System.Web;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;
using NHibernate;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportServiceConsumersFromFile : BaseImportProcess
    {
        private readonly ISession session;
        public ImportServiceConsumersFromFile(HttpPostedFileBase file, ISession session) 
            : base(file)
        {
            this.session = session;
        }

        protected override void Initialize()
        {
            Register(new ReadFromFile<ServiceConsumerRecord>(FileStream));
            RegisterWithProgressReporting(new SaveServiceConsumer(session));
        }
    }
}