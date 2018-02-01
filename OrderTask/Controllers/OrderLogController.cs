using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using OrderTask.Web.Models;
using OrderTask.Web.Models.SearchModel;

namespace OrderTask.Web.Controllers
{
    public class OrderLogController : Controller
    {
        #region Constructor
        private readonly ILogService<UserInfoController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderLogController(IUserInfoService userInfoService,
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

 
        [HttpGet]
        public ActionResult GetOrderLog(OrderLogSearch model, int page = 1, int limit = 10)
        {
            var result = _unitOfWork.GetRepository<OrderLog>().GetEntities();
            if (model.OrderId.HasValue)
                result = result.Where(i => i.OrderId== model.OrderId);

            var w1 = result.OrderByDescending(x => x.Id).Skip((page - 1) * limit).Take(limit);
            return Json(new
            {
                code = 0,
                msg = "ok",
                count = result.Count(),
                data = w1.ToList()
            });

        }
 
    }
}