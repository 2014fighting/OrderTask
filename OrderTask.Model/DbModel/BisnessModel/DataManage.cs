using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrderTask.Model.DbModel.BisnessModel
{
    [Table("t_DataManage")]
    public  class DataManage : BaseEntity<int>
    {
        /// <summary>
        /// 款号
        /// </summary>
        [MaxLength(200)]
        public string ProductNum { get; set; }

        /// <summary>
        /// 白底图
        /// </summary>
        [MaxLength(300)]
        public string BaidiAddress { get; set; }
        /// <summary>
        /// 场景图
        /// </summary>
        [MaxLength(300)]
        public string ChangjingAddress { get; set; }

        /// <summary>
        /// 套脚图
        /// </summary>
        [MaxLength(300)]
        public string TaojiaoAddress { get; set; }

        /// <summary>
        /// 模特图
        /// </summary>
        [MaxLength(300)]
        public string MoteAddress { get; set; }

        /// <summary>
        /// 详情页
        /// </summary>
        [MaxLength(300)]
        public string XiangxiAddress { get; set; }

        /// <summary>
        /// 专辑页
        /// </summary>
        [MaxLength(300)]
        public string ZhuantiAddress { get; set; }

        /// <summary>
        /// 标准图
        /// </summary>
        [MaxLength(300)]
        public string BiaozhunAddress { get; set; }

        /// <summary>
        /// 广告图
        /// </summary>
        [MaxLength(300)]
        public string GuanggaoAddress { get; set; }

        /// <summary>
        /// 入口图
        /// </summary>
        [MaxLength(300)]
        public string RukouAddress { get; set; }


        [MaxLength(300)]
        public string Remark { get; set; }
    }
}
