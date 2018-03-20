using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using OrderTask.Service.ServiceInterface;

namespace OrderTask.Service.Service
{
    public class ShortMessage : IShortMessage
    {
        private readonly ILogService<ShortMessage> _logService;
        public ShortMessage(ILogService<ShortMessage> logService)
        {
            _logService = logService;
        }
        public string SendSmsInfo()
        {

            string targeturl = ""  , strRet;
            try
            {
                HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
                hr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                hr.Method = "GET";
                hr.Timeout = 30 * 60 * 1000;
                WebResponse hs = hr.GetResponse();
                Stream sr = hs.GetResponseStream();
                StreamReader ser = new StreamReader(sr, Encoding.Default);
                strRet = ser.ReadToEnd();
            }
            catch (Exception ex)
            {
                _logService.Error("发送短信失败:"+ex.Message);
                strRet = null;
            }
            return strRet;
        }
    }
}
