using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderTask.Model.DbModel;

namespace OrderTask.Web.Models.SearchModel
{
    public class OrderLogSearch
    {
        /// <summary>
        /// 操作人
        /// </summary>
        public int? Operator { get; set; }


        public int? OrderId { get; set; }
     
        /// <summary>
        /// 操作类型
        /// </summary>
        public int? OperationType { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string Describe { get; set; }

        public DateTime CreteTime { get; set; }
    }
}
