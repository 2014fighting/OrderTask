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
            try
            {
                var result = new MgResult();
                //https://blog.csdn.net/xinit1/article/details/72833988
                var holidayRep = _unitOfWork.GetRepository<Holiday>();
                var listH = new List<Holiday>();
                var curDate = DateTime.Now;
                var year = curDate.Year;
                for (DateTime dt = new DateTime(year, 1, 1); dt < new DateTime(year + 1, 1, 1); dt = dt.AddDays(1))
                {
                    var t = dt.ToString("yyyyMMdd");
                    if (holidayRep.GetEntities().Any(i => i.HolidayTime.Value.Year
                     == dt.Year && i.HolidayTime.Value.Day == dt.Day
                     && i.HolidayTime.Value.Month == dt.Month))
                        continue;

                    var url = $"http://api.goseek.cn/Tools/holiday?date={t}";
                    var getRs = url.GetPostPage().JsonConvert<ResultHolidy>();
                    if (dt.DayOfWeek.ToString() != "Saturday" && getRs.data != 0)
                    {
                        if (getRs.data == 2)
                            listH.Add(new Holiday()
                            {
                                Desc = "节假日",
                                HolidayTime = DateTime.Parse(dt.ToString("yyyy-MM-dd"))
                                ,
                                CreateTime = DateTime.Now,
                                CreateUser = CurUserInfo.UserName,
                                CreateUserId = CurUserInfo.UserId
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
                if (listH.Count == 0)
                {
                    result.Code = 2;
                    result.Msg = "没有符合的日期";
                    return Json(result);
                }
                holidayRep.Insert(listH);
                var r = _unitOfWork.SaveChanges();
                result.SetResult(r);
                return Json(result);
            }
            catch (Exception e)
            {
                _logger.Error("生成日期报错~",e);
                throw;
            }
           
        }

        [HttpGet]
        public ActionResult HolidayAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HolidayAdd(DateTime holidayTime ,string desc)
        {
            var result = new MgResult();
            var holidayRep = _unitOfWork.GetRepository<Holiday>();
            if (holidayRep.GetEntities().Any(i => i.HolidayTime.Value.Year
                                                  == holidayTime.Year && i.HolidayTime.Value.Day == holidayTime.Day
                                                  && i.HolidayTime.Value.Month == holidayTime.Month))
            {
                result.Msg = "该日期已存在！";
                result.Code = 2;
                return Json(result);
            }

            holidayRep.Insert(new Holiday()
            {
                Desc =desc,HolidayTime =holidayTime,CreateTime = DateTime.Now
                ,CreateUser =CurUserInfo.UserName,CreateUserId =CurUserInfo.UserId
            });
             
                var r = _unitOfWork.SaveChanges();
            result.SetResult(r);
            return Json(result);
        }


        [HttpPost]
        public ActionResult HolidayDelete(List<int> ids)
        {
            var result = _unitOfWork.GetRepository<Holiday>();
            ids.ForEach(i =>
            {
                result.Delete(result.Find(i));
            });
            var r = _unitOfWork.SaveChanges() > 0;

            return Json(new MgResult
            {
                Code = r ? 0 : 1,
                Msg = r ? "ok" : "SaveChanges失败！"
            });
        }
    }
}