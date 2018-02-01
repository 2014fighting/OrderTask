using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrderTask.Model.DbModel
{
    [Table("t_sys_RolePower")]
    public class RolePower : BaseEntity<int>
    {
        public int ButtonId { get; set; }

        public int RoleId { get; set; }

        public int MentId { get; set; }

        public int Type { get; set; }
    }
}
