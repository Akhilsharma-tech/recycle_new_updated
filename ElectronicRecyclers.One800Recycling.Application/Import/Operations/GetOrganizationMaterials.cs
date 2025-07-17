


using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Application.Import.Processes;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Web.ViewModels;
using NHibernate;
using NHibernate.Criterion;


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class GetOrganizationMaterials<TOrganization> : AbstractOperation
            where TOrganization : DomainObject
    {
        private readonly EnvironmentalOrganizationsViewModel viewModel;
        public GetOrganizationMaterials(EnvironmentalOrganizationsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public virtual void ApplyRestrictions(ICriteria criteria)
        {
            var name = viewModel.SearchName;
            if (string.IsNullOrWhiteSpace(name) == false)
                criteria.Add(Restrictions.InsensitiveLike("Name", name, MatchMode.Anywhere));

            var import = viewModel.SearchImportName;
            if (string.IsNullOrWhiteSpace(import) == false)
                criteria.Add(Restrictions.InsensitiveLike("ImportBatchName", import, MatchMode.Exact));

            var city = viewModel.SearchCity;
            if (string.IsNullOrWhiteSpace(city) == false)
                criteria.Add(Restrictions.InsensitiveLike("Address.City", city, MatchMode.Exact));

            var state = viewModel.SearchState;
            if (string.IsNullOrWhiteSpace(state) == false)
                criteria.Add(Restrictions.InsensitiveLike("Address.State", state, MatchMode.Exact));

            var zip = viewModel.SearchPostalCode;
            if (string.IsNullOrWhiteSpace(zip) == false)
                criteria.Add(Restrictions.InsensitiveLike("Address.PostalCode", zip, MatchMode.Exact));


            if (viewModel.SearchGroupId > 0)
            {
                criteria
                    .CreateAlias("verificationGroups", "groups")
                    .Add(Restrictions.Eq("groups.Id", viewModel.SearchGroupId));
            }
        }

        protected IEnumerable<TOrganization> GetOrganizationsMaterials(ISession session)
        {
            var criteria = session.CreateCriteria<TOrganization>();

            ApplyRestrictions(criteria);

            IList<TOrganization> organizations;
            if (viewModel.SearchGroupId > 0 && viewModel.CurrentPageSelectedIds.Values.All(x => x == true) && viewModel.IsSelectAllChecked)
            {
                organizations = criteria
                    .SetFetchMode("materials", FetchMode.Join)
                    .SetResultTransformer(NHibernate.Transform.Transformers.DistinctRootEntity)
                    .Future<TOrganization>()
                    .ToList();
            }
            else if (viewModel.IsSelectAllChecked)
            {
                organizations = criteria
                    .SetFetchMode("materials", FetchMode.Join)
                    .SetResultTransformer(NHibernate.Transform.Transformers.DistinctRootEntity)
                    .Future<TOrganization>()
                    .ToList();
            }
            else
            {
                var currentPageIds = viewModel
                    .CurrentPageSelectedIds
                    .Where(k => k.Value)
                    .Select(k => int.Parse(k.Key))
                    .ToList();

                ICollection ids = viewModel
                    .GetAllSelectedIds()
                    .Union(currentPageIds)
                    .ToList();

                organizations = criteria
                    .Add(Restrictions.In("Id", ids))
                    .SetFetchMode("materials", FetchMode.Join)
                    .SetResultTransformer(NHibernate.Transform.Transformers.DistinctRootEntity)
                    .Future<TOrganization>()
                    .ToList();
            }
            return organizations;

        }
        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            using (var session = NHSessionProvider.OpenSession())
            {
                foreach (var organization in GetOrganizationsMaterials(session))
                {
                    if (organization is EnvironmentalOrganization envOrg)
                    {
                        var materials = envOrg.GetMaterials().ToList();

                        foreach (var materialOrg in materials)
                        {
                            if (materialOrg.Material == null)
                                continue;

                            var row = new DynamicReader();

                            var material = materialOrg.Material;
                            var deliveries = materialOrg.GetMaterialDeliveries();

                            var businessDeliveries = deliveries
                                .Where(d => d.IsBusinessDelivery)
                                .Select(d => d.DeliveryType)
                                .Distinct();

                            var residentialDeliveries = deliveries
                                .Where(d => !d.IsBusinessDelivery)
                                .Select(d => d.DeliveryType)
                                .Distinct();

                            row["OrganizationId"] = organization.Id;
                            row["MaterialId"] = material.Id.ToString();
                            row["MaterialName"] = material.Name;
                            row["MaterialBusinessDeliveryOption"] = string.Join(",", businessDeliveries);
                            row["MaterialResidentialDeliveryOption"] = string.Join(",", residentialDeliveries);

                            yield return row;
                        }
                    }
                }
            }
        }


    }
}