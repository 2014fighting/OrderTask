using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderTask.Model.DbModel;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using OrderTask.Web.Models;
using OrderTask.Web.Models.Common;

namespace OrderTask.Web.Controllers
{
    [Authorize]
    public class RoleInfoController : BaseController
    {
        #region Constructor
        private readonly ILogService<RoleInfoController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IUserInfoService _userInfoService;

        // Create a field to store the mapper object
        private readonly IMapper _mapper;

        public RoleInfoController(IUserInfoService userInfoService,
            IUnitOfWork unitOfWork, ILogService<RoleInfoController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userInfoService = userInfoService;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion
        [HttpGet]
        public ActionResult GetRoleInfo(RoleInfoModel role, int page = 1, int limit = 10)
        {

            var result = _unitOfWork.GetRepository<RoleInfo>().GetEntities();
            if (!string.IsNullOrEmpty(role.RoleName))
                result = result.Where(i => i.RoleName.Contains(role.RoleName));
            if (!string.IsNullOrEmpty(role.Describe))
                result = result.Where(i => i.Describe.Contains(role.Describe));

            var w1 = result.OrderByDescending(x => x.Id).Skip((page - 1) * limit).Take(limit);
            return Json(new
            {
                code = 0,
                msg = "ok",
                count = result.Count(),
                data = w1.ToList()
            });

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddRole()
        {
            return View();
        }
        public IActionResult EditRole()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAllRoleInfo()
        {
            var result = _unitOfWork.GetRepository<RoleInfo>()
                .GetEntities().ProjectTo<SelectsModel>();
            return Json(result.ToList());
        }
    }
}