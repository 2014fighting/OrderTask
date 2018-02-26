using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using OrderTask.Web.Models;
using OrderTask.Web.Models.Common;

namespace OrderTask.Web.Controllers
{
    public class EvaluateController : Controller
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
        public IActionResult AddEvaluate()
        {
            return View();
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

            var r = _unitOfWork.SaveChanges();

            res.Code = r > 0 ? 0 : 1;
            res.Msg = r > 0 ? "ok" : "SaveChanges失败！";

            return Json(res);
        }
    }
}