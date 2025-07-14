using System.Web;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportLookupCodeFromFile<T> : BaseImportProcess where T : LookupCode
    {
        public ImportLookupCodeFromFile(HttpPostedFileBase file) : base(file)
        {
        }

        protected override void Initialize()
        {
            var session = NHSessionProvider.OpenSession();
            var lookups = session
                .QueryOver<T>()
                .List<T>();

            Register(new ReadFromFile<LookupCodeRecord>(FileStream));
            Register(new FormatLookupCodes());
            Register(new CheckLookupCodesForDuplicates<T>(lookups));
            RegisterWithProgressReporting(new SaveLookupCodes<T>(session));  
        }
    }
}