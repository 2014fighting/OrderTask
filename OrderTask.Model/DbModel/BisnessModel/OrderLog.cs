using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrderTask.Model.DbModel.BisnessModel
{
    [Table("t_OrderLog")]
    public  class OrderLog:BaseEntity<int>
    {
        /// <summary>
        /// 操作人
        /// </summary>
        public int? Operator { get; set; }


        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

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
