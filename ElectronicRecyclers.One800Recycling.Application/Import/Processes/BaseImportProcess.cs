using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Web;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public abstract class BaseImportProcess : AbstractEtlProcess
    {
        protected  string FileName { get; set; }

        protected Stream FileStream { get; set; }

        protected BaseImportProcess() { }

        protected BaseImportProcess(IFormFile file)
        {
            FileName = file.FileName;
            FileStream = file.OpenReadStream();
        }

        protected override void PostProcessing()
        {
            base.PostProcessing();

            using (var session = NHSessionProvider.OpenSession())
            {
                session.Save(new ImportHistory {FileName = FileName});
                session.Flush();
            }
        }
    }
}