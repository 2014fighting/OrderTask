using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OrderTask.Model.DbModel;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using OrderTask.Web.Models;


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

    }
}
