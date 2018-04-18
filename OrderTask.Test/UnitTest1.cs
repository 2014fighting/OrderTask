using System;
using Microsoft.EntityFrameworkCore;
using OrderTask.Model;
using OrderTask.Service.Service;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using Xunit;

namespace OrderTask.Test
{
    public class UnitTest1
    {
        private readonly IOrderService _orderService;
        public UnitTest1()
        {
            //集成测试
            var optionsBuilder = new DbContextOptionsBuilder<OrderTaskContext>();
            var optionsBuilder1 = optionsBuilder.UseMySql("Server=localhost;database=OrderTaskDb;uid=root;pwd=abc123;");
            _orderService = new OrderService(
                new UnitOfWork<OrderTaskContext>(new OrderTaskContext(optionsBuilder1.Options)));
        }

        [Fact]
        public void Test2()
        {
            var result = _orderService.CaculateAutoTime(DateTime.Parse("2018-04-18  02:20"), 2);
        }
        [Fact]
        public void Test1()
        {
            var curTime = DateTime.Parse("2018-04-21 06:00");
           
            var week = curTime.DayOfWeek;
            var x = curTime.ToShortDateString();
            var w = curTime.TimeOfDay;
            var tempTime = DateTime.MaxValue;

            string _strWorkingDayAM = "08:30";//工作时间
            string _strWorkingDayPM = "17:30";//下班时间
            TimeSpan dspWorkingDayAm = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
            TimeSpan dspWorkingDayPm = DateTime.Parse(_strWorkingDayPM).TimeOfDay;
            var curDateTime = DateTime.Parse("2018-04-17 16:00");
            var s = DateTime.Parse(curDateTime.AddDays(1).ToString("yyyy-MM-dd") + " "+_strWorkingDayAM).AddHours(2)
                    - (DateTime.Parse(curDateTime.ToString("yyyy-MM-dd") + " " + _strWorkingDayPM)
                       - curDateTime);
        }

        [Fact]
        public void Test3()
        {
            var curDate = DateTime.Now;
            var year = curDate.Year;
            for (DateTime dt = new DateTime(year, 1, 1); dt < new DateTime(year + 1, 1, 1); dt = dt.AddDays(1))
            {
                Console.WriteLine(dt.ToString("yy-MM-dd"));
            }
        }


    }
}
