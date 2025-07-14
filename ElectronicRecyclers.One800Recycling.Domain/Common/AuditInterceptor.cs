using System;
using System.Threading;
using NHibernate;
using NHibernate.Type;

namespace ElectronicRecyclers.One800Recycling.Domain.Common
{
    public class AuditInterceptor : EmptyInterceptor
    {
        private static void SetUserName(object[] state, object[] propertyNames, string propertyName)
        {
            var index = Array.IndexOf(propertyNames, propertyName);

            var userName = Thread.CurrentPrincipal.Identity.IsAuthenticated
                ? Thread.CurrentPrincipal.Identity.Name
                : "unknown";

            state[index] = userName;
        }

        private static void SetDate(object[] state, object[] propertyNames, string propertyName, DateTimeOffset date)
        {
            var index = Array.IndexOf(propertyNames, propertyName);

            state[index] = date;
        }

        public override bool OnFlushDirty(
            object entity,
            object id,
            object[] currentState,
            object[] previousState,
            string[] propertyNames,
            IType[] types)
        {
            if (!(entity is IAuditable))
                return false;

            SetUserName(currentState, propertyNames, "ModifiedBy");

            SetDate(currentState, propertyNames, "ModifiedOn", DateTimeOffset.Now);

            return true;
        }

        public override bool OnSave(
            object entity,
            object id,
            object[] state,
            string[] propertyNames,
            IType[] types)
        {
            if (!(entity is IAuditable))
                return false;

            SetUserName(state, propertyNames, "CreatedBy");

            SetUserName(state, propertyNames, "ModifiedBy");

            return true;
        }
    }

}