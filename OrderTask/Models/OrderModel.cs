using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTask.Web.Models
{
    public class OrderModel
    {

        public int? Id { get; set; }
        public string OrderName { get; set; }
        public bool ExistRefuse { get; set; }
        /// <summary>
        /// 订单描述
        /// </summary>
        public string OrderDescribe { get; set; }

        /// <summary>
        /// 完成描述
        /// </summary>
        public string FinishDescribe { get; set; }

        /// <summary>
        /// 未确认，部分接单，全部接单，部分完成，全部完成
        /// </summary>
        public int? OrderState { get; set; }

        /// <summary>
        /// 紧急程度
        /// </summary>
        public int? Degree { get; set; }

        /// <summary>
        /// 下单人
        /// </summary>
        public int? UserInfoId { get; set; }


        public List<int> OrderTypeIds { get; set; }

        public  string  StrOrderTypeIds { get; set; }
        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime? ComplateTime { get; set; }

        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime? ExpectTime { get; set; }

        /// <summary>
        /// 接单人
        /// </summary>
        public List<int> ReceivePersons { get; set; }
        public string  StrReceivePersons { get; set; }

        public string CreteUser { get; set; }

        public string StrNumber { get; set; }
    }
}
