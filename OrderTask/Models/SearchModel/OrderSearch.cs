using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTask.Web.Models.SearchModel
{
    public class OrderSearch
    {
        public int? OrderId { get; set; }
        public string OrderName { get; set; }
        public int? UserInfoId { get; set; }
    }
}
