using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrderTask.Model.DbModel.BisnessModel
{
    [Table("t_DataManage")]
    public class DataManage : BaseEntity<int>
    {
        /// <summary>
        /// 款号
        /// </summary>
        [MaxLength(200)]
        public string ProductNum { get; set; }

        /// <summary>
        /// 所属订单
        /// </summary>
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        /// <summary>
        /// 完成数量
        /// </summary>
        [MaxLength(300)]
        public int Count { get; set; }

        /// <summary>
        /// 资料类型
        /// </summary>
        [MaxLength(300)]
        public int DataType { get; set; }

        /// <summary>
        /// 资料路径
        /// </summary>
        [MaxLength(320)]
        public string DataAddress { get; set; }

        [MaxLength(300)]
        public string Remark { get; set; }
    }
}
