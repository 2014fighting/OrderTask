using System;
using System.Collections.Generic;
using System.Text;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.Service.ExportImport.Help;

namespace OrderTask.Service.ServiceInterface
{
    public  interface IExportImportService
    {
          byte[] ExportToXlsx<T>(PropertyByName<T>[] properties, IEnumerable<T> itemsToExport);
    }
}
