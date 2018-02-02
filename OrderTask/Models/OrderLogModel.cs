using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTask.Web.Models
{
    public class OrderLogModel
    {
        public int? Id { get; set; }

        public DateTime CreteTime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
 

        public int? OrderId { get; set; }
     

        /// <summary>
        /// 操作类型
        /// </summary>
        public int? OperationType { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string Describe { get; set; }
    }
}
