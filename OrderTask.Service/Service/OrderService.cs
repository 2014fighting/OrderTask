using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;

namespace OrderTask.Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool IsExistRefuse(int orderId, int curReceiveId)
        {
            return _unitOfWork.GetRepository<ReceivePerson>().GetEntities(i => i.OrderId == orderId && i.Id != curReceiveId)
                .Any(i => i.ReceiveState ==2);
        }

        public bool IsComplete(int orderId)
        {
            return !_unitOfWork.GetRepository<ReceivePerson>().GetEntities(i => i.OrderId == orderId)
                .Any(i => i.ReceiveState !=4);
        }
        /// <summary>
        /// 计算最晚自动接单时间，规则 写死
        /// </summary>
        /// <param name="curDateTime">当前下单时间</param>
        /// <param name="t">接单延迟时间单位小时</param>
        /// <returns></returns>
        public DateTime CaculateAutoTime(DateTime curDateTime, int t = 2)
        {
            var resultTime=DateTime.MaxValue;
            var year = curDateTime.Day;var month = curDateTime.Month;
            var day = curDateTime.Day;
            var hour = curDateTime.Hour;
            
            var holidays = _unitOfWork.GetRepository<Holiday>().GetEntities();
            //是否为工作日
            var  boolAny = holidays.Any(i => i.HolidayTime.Value.Day == 
           day&&i.HolidayTime.Value.Year==year&&
            i.HolidayTime.Value.Month== month);
            

            string _strWorkingDayAM = "08:30";//工作时间
            string _strWorkingDayPM = "17:30";//下班时间
            TimeSpan dspWorkingDayAm = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
            TimeSpan dspWorkingDayPm = DateTime.Parse(_strWorkingDayPM).TimeOfDay;
            if (!boolAny)
            {
                TimeSpan tsNow = curDateTime.TimeOfDay;
                TimeSpan dspNow = curDateTime.AddHours(t).TimeOfDay;
              //加上接单延迟时间是否为下班时间  如果为上班时间则直接计算 否则 算入下一个工作日
                if (tsNow >= dspWorkingDayAm&& dspNow >= dspWorkingDayAm && dspNow <= dspWorkingDayPm)
                {
                    return curDateTime.AddHours(t);
                }
                else
                {
                    if (tsNow < dspWorkingDayAm)
                    {
                        return DateTime.Parse(curDateTime.ToString("yyyy-MM-dd")
                                              + " " + _strWorkingDayAM).AddHours(t);
                    }
                    //计算下一个工作日 
                    var tempd=curDateTime.AddDays(1);
                     
                    while (holidays.Any(i => i.HolidayTime.Value.Day ==
                                             tempd.Day && i.HolidayTime.Value.Year == tempd.Year &&
                                             i.HolidayTime.Value.Month == tempd.Month))
                    {
                        tempd = curDateTime.AddDays(1);
                    }
                    //下一个工作日的上班时间+延迟小时数-（上一个工作日的下班时间-下单时间）=最晚接单时间
                    var lastReceiveTime = DateTime.Parse(tempd.ToString("yyyy-MM-dd") + " " + _strWorkingDayAM).AddHours(t)
                            - (DateTime.Parse(curDateTime.ToString("yyyy-MM-dd") + " " + _strWorkingDayPM)
                               - curDateTime);
                    return lastReceiveTime;
                }
            }
            else
            {
                //下一个工作日
                var tempd = hour > 12 ? curDateTime.AddDays(1) : curDateTime;

                while (holidays.Any(i => i.HolidayTime.Value.Day ==
                                         tempd.Day && i.HolidayTime.Value.Year == tempd.Year &&
                                         i.HolidayTime.Value.Month == tempd.Month))
                {
                    tempd = curDateTime.AddDays(1);
                }
                //直接返回下一个工作日的 上班时间+延迟小时
                return DateTime.Parse(tempd.ToString("yyyy-MM-dd") + " " + _strWorkingDayAM).AddHours(t);
            }
          
        }
    }
}
