using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderTask.Common.Help;
using OrderTask.Model;
using OrderTask.Model.DbModel;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.Service.ExportImport.Help;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using OrderTask.Web.Models;
using OrderTask.Web.Models.Common;
using OrderTask.Web.Models.SearchModel;
using OrderTask.Web.SysFilter;

namespace OrderTask.Web.Controllers
{
    [Authorize]
    public class DataManageController : BaseController
    {

        #region Constructor
        private readonly ILogService<UserInfoController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IExportImportService _exportImportService; 
        private readonly IImportManagerService _importManager;
        public DataManageController(
            IUserInfoService userInfoService,
            IUnitOfWork unitOfWork,
            ILogService<UserInfoController> logger,
            IExportImportService exportImportService,
            IMapper mapper,
            IImportManagerService importManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _exportImportService = exportImportService;
            _importManager = importManager;
        }

        #endregion


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDataManage(DataManageModel model)
        {
            var res = new MgResult();

            var order = _unitOfWork.GetRepository<Order>().Find(model.OrderId);
            if (order == null)
            {
                res.Code = 120;
                res.Msg = $"{model.OrderId}该订单不存在,请重新输入！";
                return Json(res);
            }

            var receivePerson = _unitOfWork.GetRepository<ReceivePerson>()
                .GetEntities(i => i.OrderId == model.OrderId && i.UserInfoId == CurUserInfo.UserId).FirstOrDefault();
            if (receivePerson == null)
            {
                res.Code = 1;
                res.Msg = "您不是该订单的接单人,不允许保存订单资料库！";
                return Json(res);
            }
            var orderCount = receivePerson.TotalCount;
            var dataMange = _unitOfWork.GetRepository<DataManage>().GetEntities();
            var curCount = dataMange.Where(i => i.OrderId == model.OrderId
                                                && i.CreateUserId == CurUserInfo.UserId).Sum(i => i.Count);

            if (curCount + model.Count > orderCount)
            {
                res.Code = 2;
                res.Msg = $"{ model.OrderId}该订单您应完成总数{orderCount},已经完成{curCount},现有数量{ model.Count}已超标！";
                return Json(res);
            }

            if (!ModelState.IsValid)
            {
                res.Code = 110;
                res.Msg = "后端模型验证失败！";
                return Json(res);
            }
            var dataManage = _mapper.Map<DataManage>(model);

            var repoDataManage = _unitOfWork.GetRepository<DataManage>();
            dataManage.CreateUser = CurUserInfo.TrueName;
            dataManage.CreateUserId = CurUserInfo.UserId;
            dataManage.CreateTime = DateTime.Now;
            repoDataManage.Insert(dataManage);
            var r = _unitOfWork.SaveChanges();

            res.Code = r > 0 ? 0 : 1;
            res.Msg = r > 0 ? "ok" : "SaveChanges失败！";
            return Json(res);
        }

        [HttpGet]
        public IActionResult AddDataManage()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">1编辑 2查看</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditDataManage(int id, int type = 1)
        {
            ViewBag.pageType = type;

            var dataManage = _unitOfWork.GetRepository<DataManage>().Find(id);

            return View(_mapper.Map<DataManageModel>(dataManage));
        }

        [HttpPost]
        public IActionResult EditDataManage(DataManageModel model)
        {
            var res = new MgResult();


            var user = _unitOfWork.GetRepository<UserInfo>().
                GetEntities(x => x.Id == CurUserInfo.UserId).FirstOrDefault();

            if (user?.DepartMentId != 3 || CurUserInfo.RoleList.Any(i => i != "经理"))
            {
                res.Code = 999;
                res.Msg = "只有部门经理可以操作！";
                return Json(res);
            }
            var receivePerson = _unitOfWork.GetRepository<ReceivePerson>()
                .GetEntities(i => i.OrderId == model.OrderId && i.UserInfoId == CurUserInfo.UserId).FirstOrDefault();
            if (receivePerson == null)
            {
                res.Code = 1;
                res.Msg = "您不是该订单的接单人,不允许保存订单资料库！";
                return Json(res);
            }
            var orderCount = receivePerson.TotalCount;
            var dataMange = _unitOfWork.GetRepository<DataManage>().GetEntities();
            var curCount = dataMange.Where(i => i.OrderId == model.OrderId
                                                && i.CreateUserId == CurUserInfo.UserId
                                                && i.Id != model.Id)
                .Sum(i => i.Count);

            if (curCount + model.Count > orderCount)
            {
                res.Code = 2;
                res.Msg = $"{ model.OrderId}该订单您应完成总数{orderCount},已经完成{curCount},现有数量{ model.Count}已超标！";
                return Json(res);
            }
            if (!ModelState.IsValid)
            {
                res.Code = 110;
                res.Msg = "后端验证不通过！";
                return Json(res);
            }

            var dataManage = _unitOfWork.GetRepository<DataManage>().Find(model.Id);
            if (dataManage == null)
            {
                res.Code = 120;
                res.Msg = "资料不存在！";
                return Json(res);
            }

            _mapper.Map(model, dataManage);
            dataManage.UpdateTime = DateTime.Now;
            dataManage.UpdateUser = CurUserInfo.TrueName;
            dataManage.UpdateUserId = CurUserInfo.UserId;
            var r = _unitOfWork.SaveChanges();

            res.Code = r > 0 ? 0 : 1;
            res.Msg = r > 0 ? "ok" : "SaveChanges失败！";
            return Json(res);
        }
        [HttpGet]
        public ActionResult GetDataManage(DataManageSearch dataManage)
        {
            var result = _unitOfWork.GetRepository<DataManage>().GetEntities();
            if (!string.IsNullOrEmpty(dataManage.ProductNum))
                result = result.Where(i => i.ProductNum.Contains(dataManage.ProductNum));

            if (dataManage.OrderId.HasValue)
                result = result.Where(i => i.OrderId==dataManage.OrderId);


            var w1 = result.OrderByDescending(x => x.Id).Skip((dataManage.page - 1) * dataManage.limit).Take(dataManage.limit);
            return Json(new
            {
                code = 0,
                msg = "ok",
                count = result.Count(),
                data = w1.ToList()
            });

        }

        [HttpPost]
        public ActionResult DataManageDelete(List<int> ids)
        {
            var res = new MgResult();

            var user = _unitOfWork.GetRepository<UserInfo>().
                GetEntities(x=>x.Id==CurUserInfo.UserId).FirstOrDefault();
            
            if (user?.DepartMentId != 3 || CurUserInfo.RoleList.Any(i => i!="经理"))
            {
                res.Code = 999;
                res.Msg = "只有部门经理可以操作！";
                return Json(res);
            }
            var result = _unitOfWork.GetRepository<DataManage>();
            ids.ForEach(i =>
            {
                result.Delete(result.Find(i));
            });
            var r = _unitOfWork.SaveChanges() > 0;

            return Json(new MgResult
            {
                Code = r ? 0 : 1,
                Msg = r ? "ok" : "SaveChanges失败！"
            });
        }

        [HttpGet]
        public IActionResult DataManageExport(DataManageSearch dataManage)
        {
            var properties = new PropertyByName<DataManage>[]
            {
                new PropertyByName<DataManage>("Id",d=>d.Id),
                new PropertyByName<DataManage>("ProductNum",d=>d.ProductNum),
                new PropertyByName<DataManage>("DataType",d=>d.DataType),
                new PropertyByName<DataManage>("DataAddress",d=>d.DataAddress),
                new PropertyByName<DataManage>("Count",d=>d.Count),
                new PropertyByName<DataManage>("Remark",d=>d.Remark),
                new PropertyByName<DataManage>("OrderId",d=>d.OrderId)
            };
            var result = _unitOfWork.GetRepository<DataManage>().GetEntities();
            if (!string.IsNullOrEmpty(dataManage.ProductNum))
                result = result.Where(i => i.ProductNum.Contains(dataManage.ProductNum));
            var bytes = _exportImportService.ExportToXlsx(properties,result);
            return File(bytes, MimeTypes.TextXlsx, "DataManage.xlsx");
        }
 

        public ActionResult DataManageImport()
        {
            try
            {
                var importexcelfile = Request.Form.Files[0];
                if (importexcelfile != null && importexcelfile.Length > 0)
                {
                   var res= _importManager.ImportDataManageFromXlsx(importexcelfile.OpenReadStream(),CurUserInfo);
                    return Json(new { code = res.Code, msg =res.Msg });
                }
                    return Json(new { code = 1, msg = "导入失败:没有读取到要导入的数据内容！" });
            }
            catch (Exception e)
            {
                return Json(new { code = 1, msg = $"导入失败:{e.InnerException.Message}" });
            }
            
         
        }
    }
}