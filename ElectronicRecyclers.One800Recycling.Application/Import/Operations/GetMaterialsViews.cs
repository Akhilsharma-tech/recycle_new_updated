


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Criterion;
using NHibernate;
using System.Collections;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Web.ViewModels;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class GetMaterialsViews 
    {
        private readonly MaterialsViewModel viewModel;

        public GetMaterialsViews(MaterialsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        protected IEnumerable<Material> GetMaterialsList(ISession session)
        {
            var criteria = session.CreateCriteria<Material>();

            IList<Material> materials;

            if (viewModel.CurrentPageSelectedIds.Values.All(x => x) && viewModel.IsSelectAllChecked)
            {
                materials = criteria
                    .SetResultTransformer(NHibernate.Transform.Transformers.DistinctRootEntity)
                    .Future<Material>()
                    .ToList();
            }
            else if (viewModel.IsSelectAllChecked)
            {
                materials = criteria
                    .SetResultTransformer(NHibernate.Transform.Transformers.DistinctRootEntity)
                    .Future<Material>()
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

                materials = criteria
                    .Add(Restrictions.In("Id", ids))
                    .SetResultTransformer(NHibernate.Transform.Transformers.DistinctRootEntity)
                    .Future<Material>()
                    .ToList();
            }

            return materials;
        }

        public  IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            using (var session = NHSessionProvider.OpenSession())
            {
                foreach (var material in GetMaterialsList(session))
                {
                    if (material == null)
                        continue;
                    
                    var row = new Dictionary<string,object>
                    {
                        ["MaterialId"] = material.Id.ToString(),
                        ["MaterialName"] = material.Name,
                        ["Description"] = material.Description,
                        ["SearchKeywords"] = material.GetSearchKeywords(),
                        ["Category"] = string.Join(",", material.GetCategories().Select(x => x.Name) ?? new List<string>()),
                        ["IsActive"] = material.IsEnabled,
                    };

                    yield return row;
                }
            }
        }
    }

}