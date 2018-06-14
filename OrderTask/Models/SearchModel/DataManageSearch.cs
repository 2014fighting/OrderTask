using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTask.Web.Models.SearchModel
{
    public class DataManageSearch: BaseSearch
    {
        public int? Id { get; set; }
    
        public string ProductNum { get; set; }

        public int? OrderId { get; set; }
    }
}
