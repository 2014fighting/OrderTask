using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrderTask.Model.DbModel.BisnessModel
{
    [Table("t_Evaluate")]
    public class Evaluate : BaseEntity<int>
    {

        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }


        public int? UserInfoId { get; set; }
        [ForeignKey("UserInfoId")]
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// 评价
        /// </summary>
        public string EvaluateInfo { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public int? Rrade { get; set; }

        /// <summary>
        /// 类别：工作进度、沟通效率、作品满意
        /// </summary>
        public int? EvaluateType { get; set; }
    }
}
