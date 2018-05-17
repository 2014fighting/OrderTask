using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTask.Web.Models.ViewModel
{
    public class OrderCountModel
    {
        public int TotalCount { get; set; } = 0;

        public int CompleteCount { get; set; } = 0;

        public int NoConfirmCount { get; set; } = 0;

        public int ReceiveCount { get; set; } = 0;

    }
}
