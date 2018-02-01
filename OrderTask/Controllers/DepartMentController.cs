using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using OrderTask.Model.DbModel;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using OrderTask.Web.Models.Common;

namespace OrderTask.Web.Controllers
{
    public class DepartMentController : BaseController
    {

        #region Constructor
        private readonly ILogService<DepartMentController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        public DepartMentController (IUnitOfWork unitOfWork, ILogService<DepartMentController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult GetDpList()
        {
            var result = _unitOfWork.GetRepository<DepartMent>()
                .GetEntities().ProjectTo<SelectsModel>();
            return Json(result.ToList());
        }
    }
}