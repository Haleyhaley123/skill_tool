using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NTS.Common.Helpers
{
    public static class DataTableHelper
    {
        /// <summary>
        /// Convert list to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">List cần convert</param>
        /// <returns>table convert</returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            PropertyDescriptor prop;
            for (int i = 0; i < props.Count; i++)
            {
                prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static DataTable ToDataTableExport<T>(this IList<T> data, List<string> columnExports)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            int isIndexExist = 0; PropertyDescriptor prop;
            bool isProExist = false;
            int coumnIndex = 0;
            int index = 0;
            foreach (var column in columnExports)
            {
                isProExist = false;
                for (int i = 0; i < props.Count; i++)
                {
                    prop = props[i];

                    if (column.Equals(prop.Name))
                    {
                        //table.Columns.Add(column, prop.PropertyType);
                        table.Columns.Add(column, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                        isProExist = true;

                    }
                }

                if (!isProExist)
                {
                    table.Columns.Add(column, typeof(string));
                }

                if (column.ToLower().Equals("index"))
                {
                    coumnIndex = index;
                    isIndexExist = isProExist ? 1 : 2;
                }

                index++;
            }

            object[] values;


            values = new object[columnExports.Count];
            index = 1;
            PropertyInfo? propertyInfo;
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    propertyInfo = item.GetType().GetProperty(columnExports[i]);
                    if (propertyInfo != null)
                    {
                        values[i] = propertyInfo.GetValue(item, null);
                    }
                }

                if (isIndexExist == 2)
                {
                    values[coumnIndex] = index;
                }

                table.Rows.Add(values);
                index++;
            }

            return table;
        }
    }
}
