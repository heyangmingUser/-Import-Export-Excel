using DaoRu.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DaoRu
{
    public  class ImportExcelHelper
    {
 
        /// <summary>
        /// 从excel文件导入数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async static Task<DataTable> ImportExcelFile(string path)
        {
            return await Task.Run(async() =>
            {
                using (var file = File.OpenRead(path))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        using (var package = new ExcelPackage(ms))
                        {
                            var sheet = package.Workbook.Worksheets[0];
                            var dt =await GetDataTable(package);

                            var rowsNumbler = sheet.Dimension?.Rows;
                            var columnsNumbler = sheet.Dimension?.Columns;
                            for (int row = 2; row <= rowsNumbler; row++)
                            {
                                var newRow = dt.NewRow();
                                for (int col = 1; col <= columnsNumbler; col++)
                                {
                                    newRow[col - 1] = sheet.Cells[row, col].Value;
                                }
                                dt.Rows.Add(newRow);
                            }
                            return dt;
                        }
                    }
                }
            });
        }
        /// <summary>
        /// /它创建一个数据表，该数据表具有excel文件中相同克隆值
        /// </summary>
        /// <param name="excelPackage"></param>
        /// <returns></returns>
        private static async Task<DataTable> GetDataTable(ExcelPackage excelPackage)
        {
            return await Task.Run(() =>
            {
                int index = 0;
                var sheet = excelPackage.Workbook.Worksheets[index];
                int? columnsNumber = sheet.Dimension?.Columns;
                var dt = new DataTable(sheet.Name);
                for (int i = 1; i <= columnsNumber; i++)
                {
                    dt.Columns.Add(sheet.Cells[1, i].Value.ToString());
                }

                return dt;
            });
        }
        /// <summary>  
        /// 填充对象列表：用DataTable填充实体类
        /// </summary>  
        public static async Task<List<T>> FillModelOne<T>(DataTable dt) where T : class, new()
        {
            return await Task.Run(() =>
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    return null;
                }
                List<T> modelList = new List<T>();
                foreach (DataRow dr in dt.Rows)
                {
                    T model = new T();
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {

                        PropertyInfo[] pr = model.GetType().GetProperties();
                        foreach (var pro in pr)
                        {
                            var type = pro.PropertyType;
                            var name = pro.GetCustomAttribute<DisplayAttribute>()?.Name;
                            if (name == dr.Table.Columns[i].ColumnName)
                            {
                                object value;
                                string cellValue = dr[i].ToString();
                                PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                                if (pro.Name != null && dr[i] != DBNull.Value)

                                    type = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) ? type.GetGenericArguments()[0] : type;

                                if (type == typeof(int))
                                {
                                    value = int.Parse(cellValue);
                                }
                                else if (type == typeof(double))
                                {
                                    value = double.Parse(cellValue);
                                }
                                else if (type == typeof(bool))
                                {
                                    value = bool.Parse(cellValue);
                                }
                                else if (type == typeof(string))
                                {
                                    value = cellValue;
                                }
                                else if (type == typeof(DateTime))
                                {
                                    value = DateTime.ParseExact(cellValue, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                                }
                                else if (type == typeof(Genders)) 
                                {
                                    if (!Enum.TryParse(cellValue, out Genders gender))
                                    {
                                       
                                    }
                                    value = gender;
                                }
                                else
                                {
                                    continue;
                                }

                                if (value?.ToString() == name)
                                {

                                }
                                pro.SetValue(model, value, null);
                            }
                        }
                    }
                    modelList.Add(model);
                }
                return modelList;
            });
           
        }
        /// <summary>  
        /// 填充对象列表：用DataTable填充实体类
        /// </summary>  
        public async static Task<List<T>> FillModel<T>(DataTable dt) where T : class, new()
        {
            return await Task.Run(() =>
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    return null;
                }
                List<T> modelList = new List<T>();
                foreach (DataRow dr in dt.Rows)
                {
                    //T model = (T)Activator.CreateInstance(typeof(T));  
                    T model = new T();
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                        if (propertyInfo != null && dr[i] != DBNull.Value)
                            propertyInfo.SetValue(model, dr[i], null);
                    }
                    modelList.Add(model);
                }
                return modelList;
            });

        }

    }

}
