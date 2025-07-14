using System.Web;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ImportMaterialCategoriesFromFile : BaseImportProcess
    {
        public ImportMaterialCategoriesFromFile(HttpPostedFileBase file) : base(file)
        {
        }

        protected override void Initialize()
        {
            var session = NHSessionProvider.OpenSession();
            var categories = session
                .QueryOver<MaterialCategory>()
                .List<MaterialCategory>();

            Register(new ReadFromFile<MaterialCategoryRecord>(FileStream));
            Register(new CheckMaterialCategoriesForDuplicates(categories));
            RegisterWithProgressReporting(new SaveMaterialCategories(session));
        }
    }
}