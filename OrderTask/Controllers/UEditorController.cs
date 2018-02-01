using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UEditorNetCore;

namespace OrderTask.Web.Controllers
{
    [Route("api/[controller]")] //配置路由
    public class UEditorController : Controller
    {
        private readonly UEditorService _ue;
        public UEditorController(UEditorService ue)
        {
            this._ue = ue;
        }

        public void Do()
        {
            _ue.DoAction(HttpContext);
        }
    }
}