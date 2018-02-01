using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace OrderTask.Model.DbModel
{ 
    [Table("t_sys_UserRole")]
    public class UserRole
    {
        public int UserId { get; set; }
        public UserInfo UserInfo { get; set; }
        public int RoleId { get; set; }
        public RoleInfo RoleInfo { get; set; }
    }
}
