using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTask.Web.Models.SearchModel
{
    public class BaseSearch
    {
        public int page { get; set; } = 1;
        public int limit { get; set; } = 10;
    }
}
