using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using OrderTask.Common.Definitions;
using OrderTask.Common.Enum;
using OrderTask.Common.Help;
using OrderTask.Model.DbModel;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.Service.ExportImport.Help;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;

namespace OrderTask.Service.Service.ExportImport
{
   public class ImportManagerService : IImportManagerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ImportManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ResultModel ImportDataManageFromXlsx(Stream stream, UserSession curUserInfo)
        {
            try
            {
                var res = new ResultModel("导入成功!",0);
                using (ExcelPackage package = new ExcelPackage(stream))
                { 
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        res.Code = 1;
                        res.Msg = "worksheet不存在!";
                        return res;
                    }
                       

                    //the columns
                    //var properties = GetPropertiesByExcelCells<DataManage>(worksheet);

                    //var manager = new PropertyManager<DataManage>(properties);
                    var repOrder = _unitOfWork.GetRepository<Order>().GetEntities();
                    var repDataManage = _unitOfWork.GetRepository<DataManage>().GetEntities();
                    var repReceivePersion=_unitOfWork.GetRepository<ReceivePerson>();

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;
                    var listdm = new List<DataManage>();

                    for (int row = 2; row <= rowCount; row++)
                    { 
                        var dm=new DataManage();
                        dm.OrderId = worksheet.Cells[row, 1].Value.ToInt();
                        dm.ProductNum = worksheet.Cells[row,2].Value.ToString();
                        dm.Count = worksheet.Cells[row, 3].Value.ToInt();
                        dm.DataType = ExcelDataTransform.GetDataType(worksheet.Cells[row, 4].Value.ToString()) ;
                        dm.DataAddress = worksheet.Cells[row,5].Value.ToString();
                        dm.Remark = worksheet.Cells[row,6].Value.ToString();
                        dm.CreateUserId = curUserInfo.UserId;
                        dm.CreateTime=DateTime.Now;
                        dm.CreateUser = curUserInfo.TrueName;
                        listdm.Add(dm);
                    }
                  
                    //根据订单分组统计导入的订单的完成数量是否超标
                    foreach (var item in listdm.GroupBy(i => i.OrderId))
                    {
                        var orderid = item.First().OrderId;

                        var receivePerson = repReceivePersion
                            .GetEntities(i => i.OrderId == orderid && i.UserInfoId == curUserInfo.UserId).FirstOrDefault();
                        if (receivePerson == null)
                        {
                            res.Code = 2;
                            res.Msg = $"您不是{orderid}该订单的接单人,不允许导入订单资料库！！";
                            return res;
                        }
                        if (!repOrder.Any(i =>
                            i.Id == orderid))
                        {
                            res.Code = 3;
                            res.Msg = $"订单{orderid}不存在!";
                            return res;
                        }
                        var curCount = repDataManage.Where(i => i.OrderId
                                                           == orderid
                            && i.CreateUserId == curUserInfo.UserId).Sum(i => i.Count);
                        var c = item.Sum(i => i.Count);

                        if (curCount + c > receivePerson.TotalCount)
                        {
                            res.Code = 2;
                            res.Msg = $"{ orderid}该订单您应完成总数{receivePerson.TotalCount},已经完成{curCount},导入数量{c},已超标！";
                            return res;
                        }
                    }
                    _unitOfWork.GetRepository<DataManage>().Insert(listdm);
                    _unitOfWork.SaveChanges();
                   return res;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        protected virtual IList<PropertyByName<T>> GetPropertiesByExcelCells<T>(ExcelWorksheet worksheet)
        {
            var properties = new List<PropertyByName<T>>();
            var poz = 1;
            while (true)
            {
                try
                {
                    var cell = worksheet.Cells[1, poz];

                    if (cell == null || cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
                        break;

                    poz += 1;
                    properties.Add(new PropertyByName<T>(cell.Value.ToString()));
                }
                catch
                {
                    break;
                }
            }

            return properties;
        }
    }
}
