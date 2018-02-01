using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderTask.Common.Definitions;

namespace OrderTask.Web.Controllers
{
    public class BaseController : Controller
    {
        public UserSession CurUserInfo
        {
            get
            {
                if (HttpContext != null)
                    return new UserSession(HttpContext.User);
                else
                    return new UserSession();
            }
        }
    }
}