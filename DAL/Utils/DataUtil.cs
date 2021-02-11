using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DAL.Utils
{
    public static class DataUtil
    {
        public static List<T> DataTableToList<T>(this DataTable table) where T : new()
        {
            List<T> list = new List<T>();
            var typeProperties = typeof(T).GetProperties().Select(propertyInfo => new
            {
                PropertyInfo = propertyInfo,
                Type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType
            }).ToList();

            if (table != null)
            {
                foreach (var row in table.Rows.Cast<DataRow>())
                {
                    T obj = new T();
                    foreach (var typeProperty in typeProperties)
                    {
                        object value = row[typeProperty.PropertyInfo.Name];
                        object safeValue = value == null || DBNull.Value.Equals(value)
                            ? null
                            : Convert.ChangeType(value, typeProperty.Type);

                        typeProperty.PropertyInfo.SetValue(obj, safeValue, null);
                    }
                    list.Add(obj);
                }
            }
            return list;
        }

    }
}
