using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTask.Common.Help
{
   public  class ResultModel
    {
        public ResultModel()
        {
        }

        public ResultModel(string msg, int code)
        {
            this.Code = code;
            this.Msg = msg;
        }

        public string Msg { get; set; } = "ok";
        //默认0为成功，其它均为失败
        public int Code { get; set; } = 0;
    }

}
