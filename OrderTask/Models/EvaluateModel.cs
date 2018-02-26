using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTask.Web.Models
{
    public class EvaluateModel
    {

        public int? Id { get; set; }
        public int? OrderId { get; set; }
 
        /// <summary>
        /// 接单人
        /// </summary>
        public int? ReceivePersonId { get; set; }
 

        /// <summary>
        /// 评价
        /// </summary>
        public string EvaluateInfo { get; set; }


        /// <summary>
        /// 工作进度
        /// </summary>
        public float? WorkProgress { get; set; }

        /// <summary>
        /// 沟通效率
        /// </summary>
        public float? Communication { get; set; }

        /// <summary>
        /// 作品满意度
        /// </summary>
        public float? Satisfaction { get; set; }
    }
}
