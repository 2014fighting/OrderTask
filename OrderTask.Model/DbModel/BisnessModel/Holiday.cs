using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrderTask.Model.DbModel.BisnessModel
{
    [Table("t_Holiday")]
    public class Holiday : BaseEntity<int>
    {
        /// <summary>
        /// 节假日
        /// </summary>
        public DateTime? HolidayTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(300)]
        public string Desc { get; set; }
    }
}
