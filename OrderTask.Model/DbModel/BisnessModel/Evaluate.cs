﻿using System;
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

        /// <summary>
        /// 接单人
        /// </summary>
        public int? ReceivePersonId { get; set; }
        [ForeignKey("ReceivePersonId")]
        public ReceivePerson ReceivePerson { get; set; }

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
