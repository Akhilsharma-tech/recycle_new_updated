using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;


using NHibernate;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportRecyclingTipsFromFile : BaseImportProcess
    {
        public ImportRecyclingTipsFromFile(HttpPostedFileBase file)
            : this(file, NHSessionProvider.CurrentSession) { }

        private readonly ISession session;
        public ImportRecyclingTipsFromFile(HttpPostedFileBase file, ISession session) 
            : base(file)
        {
            this.session = session;
        }

        protected override void Initialize()
        {
            Register(new ReadFromFile<RecyclingTipRecord>(FileStream));
            RegisterWithProgressReporting(new SaveRecyclingTips(session));
        }
    }
}