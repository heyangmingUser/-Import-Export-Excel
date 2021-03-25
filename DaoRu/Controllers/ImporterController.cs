using ClosedXML.Excel;
using DaoRu.Models;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace DaoRu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("导入管理")]
    public class ImporterController : ControllerBase
    {
        private readonly ILogger<ImporterController> _logger;
        private readonly ApiContext db;
        HttpClient client;
        public ImporterController(ILogger<ImporterController> logger, ApiContext apiContext, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            db = apiContext;
            client = clientFactory.CreateClient();
        }

        [HttpPost()]
        public async Task<object> StudentInfoImporter_Test(Genders genders)
        {
            string path = @"C:\Users\heyangming\Desktop\test.xlsx";
            var import = await ImportExcelHelper.ImportExcelFile(path);
            var student = await ImportExcelHelper.FillModelOne<ImportStudentDto>(import);
            await db.ImportStudentDtos.AddRangeAsync(student);
            if (await db.SaveChangesAsync() > 0)
            {
                return BadRequest("提交失败");
            }
            return null;
        }
        [HttpPost("s")]
        public async Task<object> Add([FromBody] ImportStudentDto model)
        {
            var student = new ImportStudentDto
            {
                Gender = Genders.男,
                IdCard = "421087199211027016",
                Name = "贺扬明",
                SerialNumber = 0,
                StudentCode = "2222",
            };
            await db.ImportStudentDtos.AddAsync(student);
            if (await db.SaveChangesAsync() > 0)
            {
                return student.SerialNumber;
            }
            return BadRequest("坏的");
        }
        [HttpPost("export")]
        public async Task<object> Export([FromBody] ImportStudentDto model)
        {
            Dictionary<string, string> columnNames = new Dictionary<string, string>();
            List<string> lists = new List<string>();
            lists.Add("IdCard");

            var properties = typeof(ImportStudentDto).GetProperties();
            foreach (var property in properties)
            {
                var name = property.GetCustomAttribute<DisplayAttribute>()?.Name;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    foreach (var list in lists)
                    {
                        if (list == property.Name)
                        {
                            columnNames.Add(property.Name, name);
                        }
                    }
                }
            }
            var students = await db.ImportStudentDtos.ToListAsync();
            var sc = await ExportExcelHelper.GetByteToExportExcel(students, columnNames, lists, "sheet1", "测试");
            return File(sc, "application/octet-stream", "测试.xlsx");
            //columnNames.Add("SerialNumber", "序号");
            //columnNames.Add("StudentCode", "学籍号");
            //columnNames.Add("Name", "姓名");
            //columnNames.Add("Gender", "性别");
            //columnNames.Add("IdCard", "身份证号");

        }


        /// <summary>
        /// 自定义导出
        /// </summary>
        /// <param name="needproperties">>需要导出的表头属性</param>
        /// <returns></returns>
        [HttpPost("DebtStatistics")]
        public async Task<object> GetDebtStatistics(IEnumerable<string> needproperties)
        {
            //获取表头匹配
            Dictionary<string, string> columnNames = new Dictionary<string, string>();
            var properties = typeof(ImportStudentDto).GetProperties();
            foreach (var property in properties)
            {
                var name = property.GetCustomAttribute<DisplayAttribute>()?.Name;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    foreach (var n in needproperties)
                    {
                        if (n == property.Name)
                        {
                            columnNames.Add(property.Name, name);
                        }
                    }
                }
            }
            var query = (from r in db.ImportStudentDtos
                         select r);
            var count = columnNames.Count;
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("sheet1");
                int i = 1;
                //excel表头获取完成
                foreach (var s in columnNames.Values)
                {
                    worksheet.Cell(1, i).Value = s;
                    worksheet.Cell(1, i).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Column(i).Width = 15;
                    i++;
                }
                ExportExcelHelper.ExportData<ImportStudentDto>(columnNames, query, worksheet);
                //Type myType = typeof(ImportStudentDto);
                //List<PropertyInfo> myPro = new List<PropertyInfo>();
                //foreach (var key in columnNames.Keys)
                //{
                //    PropertyInfo p = myType.GetProperty(key);
                //    myPro.Add(p);
                //}
                //var currentRow = 1;
                //foreach (var q in query)
                //{
                //    currentRow++;
                //    int column = 1;

                //    foreach (var p in myPro)
                //    {
                //        worksheet.Cell(currentRow, column).Value = p.GetValue(q, null);
                //        column++;
                //    }
                //}
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/octet-stream", $"市级债权统计表.xlsx");
                }
            }

        }
    }
}
