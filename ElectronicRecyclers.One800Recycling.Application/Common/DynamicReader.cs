using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    }
}
