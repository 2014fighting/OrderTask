using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTask.Web.Models
{
    public class DataManageModel
    {

        public int? Id { get; set; }
        /// <summary>
        /// 款号
        /// </summary>
        [MaxLength(200)]
        public string ProductNum { get; set; }

        /// <summary>
        /// 所属订单
        /// </summary>
        public int OrderId { get; set; }

      
        /// <summary>
        /// 完成数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 资料类型
        /// </summary>
        public int DataType { get; set; }

        /// <summary>
        /// 资料路径
        /// </summary>
        [MaxLength(300)]
        public string DataAddress { get; set; }


        [MaxLength(300)]
        public string Remark { get; set; }
    }
}
