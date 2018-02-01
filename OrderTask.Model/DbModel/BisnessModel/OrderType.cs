using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrderTask.Model.DbModel.BisnessModel
{
    [Table("t_OrderType")]
    public class OrderType : BaseEntity<int>
    {
        [MaxLength(20)]
        public string TypeName { get; set; }
    }
}
