using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrderTask.Model.DbModel.BisnessModel
{
    [Table("t_Order")]
    public class Order : BaseEntity<int>
    {
        [MaxLength(300)]
        public string OrderName { get; set; }

        
        /// <summary>
        /// 订单描述
        /// </summary>
        public string OrderDescribe { get; set; }

        /// <summary>
        /// 完成描述
        /// </summary>
        public string FinishDescribe { get; set; }

        /// <summary>
        /// 未确认1，接单中2，已完成3, 已评价4,已取消5
        /// </summary>
        public int? OrderState { get; set; }

        /// <summary>
        /// 是否存在拒绝的接单人
        /// </summary>
        public bool ExistRefuse { get; set; }

        /// <summary>
        /// 紧急程度
        /// </summary>
        public int? Degree { get; set; }

        /// <summary>
        /// 下单人
        /// </summary>
        public int? UserInfoId { get; set; }

        [ForeignKey("UserInfoId")]
        public UserInfo OrderPerson { get; set; }

        public string OrderTypeIds { get; set; }
  
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
        public List<ReceivePerson> ReceivePerson { get; set; }

    }
}
