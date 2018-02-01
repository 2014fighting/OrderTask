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
        /// 拍摄图地址
        /// </summary>
        [MaxLength(300)]
        public string ShotMapAddress { get; set; }
        /// <summary>
        /// 上脚图地址
        /// </summary>
        [MaxLength(300)]
        public string FootMapAddress { get; set; }

        /// <summary>
        /// 精修图地址
        /// </summary>
        [MaxLength(300)]
        public string RuingMapAddress { get; set; }

        /// <summary>
        /// 设计图地址
        /// </summary>
        [MaxLength(300)]
        public string DesignMapAddress { get; set; }

        [MaxLength(300)]
        public string PictureAddress1 { get; set; }

        [MaxLength(300)]
        public string PictureAddress2 { get; set; }

        [MaxLength(300)]
        public string PictureAddress3 { get; set; }

        [MaxLength(300)]
        public string PictureAddress4 { get; set; }

        [MaxLength(300)]
        public string PictureAddress5 { get; set; }


        [MaxLength(300)]
        public string Remark { get; set; }
    }
}
