using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using OrderTask.Web.Models;
using OrderTask.Web.Models.Common;

namespace OrderTask.Web.Controllers
{
    public class EvaluateController : BaseController
    {
        #region Constructor
        private readonly ILogService<EvaluateController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public EvaluateController(IUnitOfWork unitOfWork, ILogService<EvaluateController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion
        [HttpGet]
        public IActionResult AddEvaluate(int id, int orderId)
        {

            //todo  接单人id +订单id 联合主键判断
            var temp=new EvaluateModel();
            var evaluate = _unitOfWork.GetRepository<Evaluate>()
                .GetFirstOrDefault(i=>i.OrderId==orderId&&i.ReceivePersonId==id);
            if (evaluate != null)
            {
                temp.Communication = evaluate.Communication;
                temp.EvaluateInfo = evaluate.EvaluateInfo;
                temp.Id = evaluate.Id;
                temp.OrderId = evaluate.OrderId;
                temp.ReceivePersonId = evaluate.ReceivePersonId;
                temp.Satisfaction = evaluate.Satisfaction;
                temp.WorkProgress = evaluate.WorkProgress;
                return View(temp);
            }
                
            return View(temp);
        }
        [HttpPost]
        public IActionResult AddEvaluate(EvaluateModel evaluate)
        {
            var res = new MgResult();
            if (!ModelState.IsValid)
            {
                res.Code = 110;
                res.Msg = "后端模型验证失败！";
                return Json(res);
            }
            evaluate.OrderId = _unitOfWork.GetRepository<ReceivePerson>().Find(evaluate.ReceivePersonId).OrderId;
            _unitOfWork.GetRepository<Evaluate>().Insert(new Evaluate()
            {
                OrderId = evaluate.OrderId,
                ReceivePersonId =evaluate.ReceivePersonId,
                Communication = evaluate.Communication,
                EvaluateInfo =evaluate.EvaluateInfo,
                WorkProgress =evaluate.WorkProgress,
                Satisfaction =evaluate.Satisfaction,
                CreateTime = DateTime.Now,
                CreateUser = CurUserInfo.TrueName,
                CreateUserId = CurUserInfo.UserId

            });
            var r = _unitOfWork.SaveChanges();

            res.Code = r > 0 ? 0 : 1;
            res.Msg = r > 0 ? "ok" : "SaveChanges失败！";

            return Json(res);
        }
    }
}