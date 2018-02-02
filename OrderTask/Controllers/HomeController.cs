using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
 
        public HomeController(  ILogService<HomeController> logger)
        { 
            _logger = logger;
        }
        #endregion
        public IActionResult Index()
        {
            ViewBag.userid = CurUserInfo.UserId;
            ViewBag.truename = CurUserInfo.TrueName;
            ViewBag.userid = CurUserInfo.RoleList;
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
