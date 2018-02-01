using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 

namespace OrderTask.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
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

    }
}
