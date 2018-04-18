using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderTask.Common;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using OrderTask.Web.Models.Common;

namespace OrderTask.Web.Controllers
{
    public class HolidayController : BaseController
    {
        #region Constructor
        private readonly ILogService<HolidayController> _logger;
        private readonly IUnitOfWork _unitOfWork;
 
        public HolidayController( IUnitOfWork unitOfWork
            , ILogService<HolidayController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetHoliday(int page = 1, int limit = 10)
        {
            var result = _unitOfWork.GetRepository<Holiday>().GetEntities();
            var w1 = result.OrderByDescending(x => x.Id).Skip((page - 1) * limit).Take(limit);
            return Json(new
            {
                code = 0,
                msg = "ok",
                count = result.Count(),
                data = w1.ToList()
            });

        }
        [HttpGet]
        public ActionResult HolidayBulid()
        {
            var result = new MgResult();
            //https://blog.csdn.net/xinit1/article/details/72833988
            var holidayRep = _unitOfWork.GetRepository<Holiday>();
            var listH = new List<Holiday>();
            var curDate = DateTime.Now;
            var year =curDate.Year;
            for (DateTime dt = new DateTime(year, 1, 1); dt < new DateTime(year+1, 1, 1); dt = dt.AddDays(1))
            {
                var t = dt.ToString("yyyyMMdd");
                 
                var url = $"http://api.goseek.cn/Tools/holiday?date={t}";
                var getRs = url.GetPostPage().JsonConvert<ResultHolidy>();
                if (dt.DayOfWeek.ToString() != "Saturday" && getRs.data != 0)
                {
                   if(getRs.data==2)
                       listH.Add(new Holiday() {Desc ="节假日",
                           HolidayTime =DateTime.Parse(dt.ToString("yyyy-MM-dd"))
                           ,CreateTime = DateTime.Now,CreateUser = CurUserInfo.UserName,CreateUserId = CurUserInfo.UserId
                       });
                   else
                   {
                       listH.Add(new Holiday()
                       {
                           Desc = "周日",
                           HolidayTime = DateTime.Parse(dt.ToString("yyyy-MM-dd"))
                           ,
                           CreateTime = DateTime.Now,
                           CreateUser = CurUserInfo.UserName,
                           CreateUserId = CurUserInfo.UserId
                       });
                    }
                }
            }
             holidayRep.Insert(listH);
            var r=_unitOfWork.SaveChanges();
            result.SetResult(r);
            return Json(result);
        }

    }
}