using System.Web;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportPostalCodesFromFile : BaseImportProcess
    {
        public ImportPostalCodesFromFile(HttpPostedFileBase file) 
            : base(file) { }

        protected override void Initialize()
        {
            var session = NHSessionProvider.OpenStatelessSession();
            var postalCodes = session.QueryOver<PostalCode>().List();

            Register(new ReadFromFile<PostalCodeRecord>(FileStream));
            Register(new ConvertRadianToDegree("Latitude"));
            Register(new ConvertRadianToDegree("Longitude"));
            Register(new CheckPostalCodesForDuplicates(postalCodes));
            RegisterWithProgressReporting(new SavePostalCodes(session));
        }
    }
}