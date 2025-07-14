using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using System;
using System.Data;
using System.Data.Common;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings.Types
{
    public class ULongAsLongType : IUserType
    {
        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public new bool Equals(object x, object y)
        {
            return object.Equals(x, y);
        }

        public int GetHashCode(object x)
        {
            return (x == null) ? 0 : x.GetHashCode();
        }

        public bool IsMutable
        {
            get { return false; }
        }

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var obj = NHibernateUtil.UInt64.NullSafeGet(rs, names, session);
            return obj == null ? null : (ulong)obj;
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            if (value == null)
            {
                ((DbParameter)cmd.Parameters[index]).Value = DBNull.Value;
            }
            else
            {
                ((DbParameter)cmd.Parameters[index]).Value = value;
            }
        }
        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public Type ReturnedType
        {
            get { return typeof (ulong); }
        }

        public SqlType[] SqlTypes
        {
            get { return new[] {SqlTypeFactory.Int64}; }
        }
    }
}