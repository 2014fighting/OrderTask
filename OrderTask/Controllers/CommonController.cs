using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using OfficeOpenXml;
using OrderTask.Common.Help;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.Service.ExportImport.Help;
using OrderTask.Service.ServiceInterface;

namespace OrderTask.Web.Controllers
{
    public class CommonController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IExportImportService _exportImportService; 
        public CommonController(IHostingEnvironment hostingEnvironment 
            ,IExportImportService exportImportService)
        {
            _hostingEnvironment = hostingEnvironment;
            _exportImportService = exportImportService;
        }

        
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("Export")]
        public IActionResult Export()
        {
            var properties = new PropertyByName<DataManage>[]
            {
                new PropertyByName<DataManage>("Id",d=>d.Id),
                new PropertyByName<DataManage>("Name",d=>d.ProductNum),
                new PropertyByName<DataManage>("Age",d=>d.Count)
            };

            var list = new List<DataManage>()
            {
                new DataManage() {Id=1,ProductNum="wenqing1",Count=18 },
                new DataManage() {Id=2,ProductNum="wenqing2",Count=19 },
                new DataManage() {Id=3,ProductNum="wenqing3",Count=20 },
                new DataManage() {Id=4,ProductNum="wenqing4",Count=21 },
                new DataManage() {Id=5,ProductNum="wenqing5",Count=22 }
            };
            var bytes = _exportImportService.ExportToXlsx(properties, list);
            return File(bytes, MimeTypes.TextXlsx, "DataManage.xlsx");

        }

        public string Import()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"Jeffcky.xlsx";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            try
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    StringBuilder sb = new StringBuilder();
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    bool bHeaderRow = true;
                    for (int row = 1; row <= rowCount; row++)
                    {
                        for (int col = 1; col <= ColCount; col++)
                        {
                            if (bHeaderRow)
                            {
                                sb.Append(worksheet.Cells[row, col].Value.ToString() + "\t");
                            }
                            else
                            {
                                sb.Append(worksheet.Cells[row, col].Value.ToString() + "\t");
                            }
                        }
                        sb.Append(Environment.NewLine);
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                return "Some error occured while importing." + ex.Message;
            }
        }
    }
}