
using NHibernate;
using NHibernate.Criterion;

namespace ElectronicRecyclers.One800Recycling.Domain.Common.Helper
{
    public static class NHibernateExtensions
    {
        public static PagedList<T> PagedList<T>(this ICriteria criteria, int pageIndex, int pageSize) 
            where T : class
        {
            if (pageIndex < 0)
                pageIndex = 1;

            var items = CriteriaTransformer.Clone(criteria)
                .SetFirstResult((pageIndex - 1) * pageSize)
                .SetMaxResults(pageSize)
                .Future<T>();

            var countCriteria = CriteriaTransformer.Clone(criteria)
                .SetProjection(Projections.RowCountInt64());

            countCriteria.ClearOrders();

            var totalCount = countCriteria
                .FutureValue<long>()
                .Value;

            return new PagedList<T>(items, totalCount, pageIndex, pageSize);
        }
    }
}