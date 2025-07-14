using NHibernate.Criterion;
using NHibernate;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Web.ViewModels;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class GetOrganizationImports<T> : GetOrganizations<EnvironmentalOrganizationImport>
    {
        public GetOrganizationImports(EnvironmentalOrganizationsViewModel viewModel) 
            : base(viewModel)
        {
        }

        public override void ApplyRestrictions(ICriteria criteria)
        {
            base.ApplyRestrictions(criteria);

            criteria.Add(Restrictions.Eq("Type", typeof (T).Name));
            criteria.Add(Restrictions.Eq("IsNameValid", true));
            criteria.Add(Restrictions.Eq("IsTelephoneValid", true));
            criteria.Add(Restrictions.Eq("IsFaxValid", true));
            criteria.Add(Restrictions.Eq("IsWebsiteUrlValid", true));
            criteria.Add(Restrictions.Eq("IsAddressValid", true));
            criteria.Add(Restrictions.Eq("IsHoursOfOperationValid", true));
            criteria.Add(Restrictions.Eq("IsDuplicate", false));
            criteria.Add(Restrictions.Eq("IsDuplicateOrganizationFoundDuringMoveOperation", false));
        }
    }
}