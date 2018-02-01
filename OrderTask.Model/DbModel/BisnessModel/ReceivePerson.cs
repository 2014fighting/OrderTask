using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrderTask.Model.DbModel.BisnessModel
{
    [Table("t_ReceivePerson")]
    public class ReceivePerson:BaseEntity<int>
    {

        /// <summary>
        /// 接单人
        /// </summary>
        public int? UserInfoId { get; set; }

        [ForeignKey("UserInfoId")]
        public UserInfo User { get; set; }

        /// <summary>
        /// 未操作 1 同意接单2 拒绝接单3 确认完成4
        /// </summary>
        public int? ReceiveState { get; set; } = 1;

        [MaxLength(500)]
        public string RefuseResion { get; set; }

        /// <summary>
        /// 接单时间
        /// </summary>
        public DateTime? ReceiveTime { get; set; }

        /// <summary>
        /// 取人完成时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }
        /// <summary>
        /// 同意或拒绝备注
        /// </summary>
        public string Remark { get; set; }

        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

    }
}
