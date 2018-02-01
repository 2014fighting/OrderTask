using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace OrderTask.Model.DbModel
{
    [Table("t_sys_RoleInfo")]
    public  class RoleInfo:BaseEntity<int>
    {
        [MaxLength(20)]
        [Required]
        public string RoleName { get; set; }

        [MaxLength(20)]
        [Required]
        public string Describe { get; set; }

        public bool IsActive { get; set; } = true;

        public  List<UserRole> UserRoles { get; set; }
    }
}
