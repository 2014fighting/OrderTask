using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTask.Web.Models
{
    public class ReceivePersonModel
    {
        public int? ReceiveState { get; set; } = 1;

        public int? Id { get; set; }
        public string RefuseResion { get; set; }
        public string TrueName { get; set; }
        public DateTime? JobScheduling { get; set; } = DateTime.Now;
        public string Group { get; set; }
        public int? UserId { get; set; }
        public int TotalCount { get; set; }

        /// <summary>
        /// 同意或拒绝备注
        /// </summary>
        public string Remark { get; set; }
    }
}
