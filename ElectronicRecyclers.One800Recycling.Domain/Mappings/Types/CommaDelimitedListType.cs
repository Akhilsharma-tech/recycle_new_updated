using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings.Types
{
    public class CommaDelimitedListType : IUserType
    {
        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object DeepCopy(object value)
        {
            return ((IList<string>) value).ToList();
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public new bool Equals(object x, object y)
        {
            return ((IList<string>) x).SequenceEqual((IList<string>) y);
        }

        public int GetHashCode(object x)
        {
            return ((IList<string>) x)
                .Aggregate(0, (current, s) => (current*377) ^ s.GetHashCode());
        }

        public bool IsMutable
        {
            get { return true; }
        }

        public object Replace(object original, object target, object owner)
        {
            return ((IList<string>) original).ToList();
        }

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var value = rs[names[0]];
            if (value == DBNull.Value || value == null)
                return new List<string>();

            var str = (string)value;
            return string.IsNullOrWhiteSpace(str)
                ? new List<string>()
                : str.Split(',').Select(s => s.Trim()).ToList();
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var parameter = cmd.Parameters[index];
            if (value == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = string.Join(",", (IList<string>)value);
            }
        }


        public Type ReturnedType
        {
            get { return typeof (IList<string>); }
        }

        public SqlType[] SqlTypes
        {
            get { return new SqlType[] { SqlTypeFactory.GetString(500) }; }
        }
    }
}