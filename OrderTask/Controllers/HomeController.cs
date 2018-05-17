using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OrderTask.Model.DbModel;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using OrderTask.Web.Models;
using OrderTask.Web.Models.ViewModel;


namespace OrderTask.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        #region Constructor
        private readonly ILogService<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork, ILogService<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion
        public IActionResult Index()
        {
            var curUser = _unitOfWork.GetRepository<UserInfo>().Find(CurUserInfo.UserId);
            ViewBag.userid = curUser.Id;
            ViewBag.truename =curUser.TrueName;
            ViewBag.handimg = curUser.Picture;
            return View();
        }
        public ActionResult MainPage()
        {
            return View();
        }

        public IActionResult Error()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;
            _logger.Error(error);

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult GetOrderCount()
        {
            var orderRsp = _unitOfWork.GetRepository<Order>();
            var result = new OrderCountModel()
            {
                TotalCount = orderRsp.Count(),
                CompleteCount = orderRsp.Count(i => i.OrderState == 3),
                NoConfirmCount = orderRsp.Count(i => i.OrderState == 1),
                ReceiveCount = orderRsp.Count(i => i.OrderState == 2)
            };
            return Json(result);
        }
        [HttpGet]
        public IActionResult GetCurUserEvaluate()
        {
            var orderRsp = _unitOfWork.GetRepository<Evaluate>();
             
            var a =
                orderRsp.GetEntities(i => i.ReceivePerson.UserInfoId == CurUserInfo.UserId)
               .Sum(i=> Convert.ToSingle(i.WorkProgress));

            var b =
                orderRsp.GetEntities(i => i.ReceivePerson.UserInfoId == CurUserInfo.UserId)
                .Sum(i => Convert.ToSingle(i.Communication)); 

            var c =
                orderRsp.GetEntities(i => i.ReceivePerson.UserInfoId == CurUserInfo.UserId)
                .Sum(i => Convert.ToSingle(i.Satisfaction));
            var res = new EvaluateModel()
            {
                WorkProgress =a
                , Communication = b
                , Satisfaction = c
            };

            return Json(res);
        }

        [HttpGet]
        public IActionResult GetOrderTypeCount()
        {
            var orderRsp = _unitOfWork.GetRepository<Order>();
            var result =new
            {
                xiutu = orderRsp.Count(i => i.OrderTypeIds.Contains("1")),
                sheji = orderRsp.Count(i => i.OrderTypeIds.Contains("2")),
                paishe = orderRsp.Count(i => i.OrderTypeIds.Contains("3")),
                cehua = orderRsp.Count(i => i.OrderTypeIds.Contains("4"))
            };
            return Json(result);
        }

    }
}
