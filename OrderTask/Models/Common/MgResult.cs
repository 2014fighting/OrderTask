using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTask.Web.Models.Common
{
    public class MgResult
    {
        public MgResult()
        {}
        public MgResult(string msg, int code)
        {
            this.Code = code;
            this.Msg = msg;
        }
        public void SetResult(int result)
        {
            this.Code = result > 0 ? 0 : 1;
            this.Msg = result > 0 ? "ok" : "SaveChanges失败！";
        }
        public string Msg { get; set; } = "";
        //默认0为成功，其它均为失败
        public int Code { get; set; } = 0;
    }
}
