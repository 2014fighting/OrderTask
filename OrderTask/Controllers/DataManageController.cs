using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderTask.Model;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using OrderTask.Web.Models;
using OrderTask.Web.Models.Common;

namespace OrderTask.Web.Controllers
{
    public class DataManageController : BaseController
    {

        #region Constructor
        private readonly ILogService<UserInfoController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DataManageController(IUserInfoService userInfoService,
            IUnitOfWork unitOfWork, ILogService<UserInfoController> logger
            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
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
            if (!ModelState.IsValid)
            {
                res.Code = 110;
                res.Msg = "后端模型验证失败！";
                return Json(res);
            }
            var dataManage = _mapper.Map<DataManage>(model);

            var repoDataManage = _unitOfWork.GetRepository<DataManage>();
            dataManage.CreteUser = CurUserInfo.TrueName;
            dataManage.CreteTime=DateTime.Now;
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
            dataManage.UpdateTime=DateTime.Now;
            dataManage.UpdateUser = CurUserInfo.TrueName;
            var r = _unitOfWork.SaveChanges();

            res.Code = r > 0 ? 0 : 1;
            res.Msg = r > 0 ? "ok" : "SaveChanges失败！";
            return Json(res);
        }
        [HttpGet]
        public ActionResult GetDataManage(DataManageModel dataManage, int page = 1, int limit = 10)
        {
            var result = _unitOfWork.GetRepository<DataManage>().GetEntities();
            if (!string.IsNullOrEmpty(dataManage.ProductNum))
                result = result.Where(i => i.ProductNum.Contains(dataManage.ProductNum));
            var w1 = result.OrderByDescending(x => x.Id).Skip((page - 1) * limit).Take(limit);
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

    }
}