using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.Common
{
    public class DynamicReader : Dictionary<string, object>
    {
        public static DynamicReader FromReader(IDataReader reader)
        {
            var row = new DynamicReader();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                string name = reader.GetName(i);
                object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                row[name] = value;
            }

            return row;
        }
        public T ToObject<T>()
        {
            return (T)ToObject(typeof(T));
        }

        public object ToObject(Type type)
        {
            if (type.IsAbstract || type.IsInterface)
                throw new InvalidOperationException($"Cannot create instance of abstract class or interface: {type.FullName}");

            object obj = Activator.CreateInstance(type)!;

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (this.ContainsKey(property.Name) && property.CanWrite && this[property.Name] != null)
                {
                    try
                    {
                        var value = Convert.ChangeType(this[property.Name], property.PropertyType);
                        property.SetValue(obj, value);
                    }
                    catch
                    {
                        // Optional: log or ignore conversion failure
                    }
                }
            }

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (this.ContainsKey(field.Name) && this[field.Name] != null)
                {
                    try
                    {
                        var value = Convert.ChangeType(this[field.Name], field.FieldType);
                        field.SetValue(obj, value);
                    }
                    catch
                    {
                        // Optional: log or ignore conversion failure
                    }
                }
            }

            return obj;
        }
        public static DynamicReader FromObject(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            DynamicReader row = new DynamicReader();

            foreach (PropertyInfo property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                row[property.Name] = property.GetValue(obj);
            }

            foreach (FieldInfo field in obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                row[field.Name] = field.GetValue(obj);
            }

            return row;
        }

    }
}

