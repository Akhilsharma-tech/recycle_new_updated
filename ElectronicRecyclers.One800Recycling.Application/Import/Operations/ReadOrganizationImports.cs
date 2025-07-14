using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System.Configuration;
using System.Data;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ReadOrganizationImports<TOrganization> : InputCommandOperation where TOrganization : EnvironmentalOrganization
    {

        public ReadOrganizationImports() : base("1800RecyclingDb") { }

        public ReadOrganizationImports(ConnectionStringSettings connectionStringSettings)
            : base(connectionStringSettings.ConnectionString) { }

        protected override DynamicReader CreateRowFromReader(IDataReader reader)
        {
            return DynamicReader.FromReader(reader);
        }


        protected override void PrepareCommand(IDbCommand cmd)

        {

            cmd.CommandText = "SELECT * FROM EnvironmentalOrganizationImport WHERE IsNameValid = @IsNameValid AND " +

                              "IsTelephoneValid = @IsTelephoneValid AND IsFaxValid = @IsFaxValid AND " +

                              "IsWebsiteUrlValid = @IsWebsiteUrlValid AND IsAddressValid = @IsAddressValid AND " +

                              "IsHoursOfOperationValid = @IsHoursOfOperationValid AND IsDuplicate = @IsDuplicate AND " +

                              "IsDuplicateOrganizationFoundDuringMoveOperation = @IsDuplicateOrganizationFoundDuringMoveOperation AND " +

                              "Type = @Type";

            AddParameter("IsNameValid", true);

            AddParameter("IsTelephoneValid", true);

            AddParameter("IsFaxValid", true);

            AddParameter("IsWebsiteUrlValid", true);

            AddParameter("IsAddressValid", true);

            AddParameter("IsHoursOfOperationValid", true);

            AddParameter("IsDuplicate", false);

            AddParameter("IsDuplicateOrganizationFoundDuringMoveOperation", false);

            AddParameter("Type", typeof(TOrganization).Name);

            //var name = viewModel.SearchName;

            //if (string.IsNullOrWhiteSpace(name) == false)

            //    criteria.Add(Restrictions.InsensitiveLike("Name", name, MatchMode.Anywhere));

            //var import = viewModel.SearchImportName;

            //if (string.IsNullOrWhiteSpace(import) == false)

            //    criteria.Add(Restrictions.InsensitiveLike("ImportBatchName", import, MatchMode.Exact));

            //var city = viewModel.SearchCity;

            //if (string.IsNullOrWhiteSpace(city) == false)

            //    criteria.Add(Restrictions.InsensitiveLike("Address.City", city, MatchMode.Exact));

            //var state = viewModel.SearchState;

            //if (string.IsNullOrWhiteSpace(state) == false)

            //    criteria.Add(Restrictions.InsensitiveLike("Address.State", state, MatchMode.Exact));

            //var zip = viewModel.SearchZip;

            //if (string.IsNullOrWhiteSpace(zip) == false)

            //    criteria.Add(Restrictions.InsensitiveLike("Address.PostalCode", zip, MatchMode.Exact));

        }
    }
}