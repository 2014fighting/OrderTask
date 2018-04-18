using System;
using System.IO;
using System.Net;
using System.Text;
using OrderTask.Common.Config;

namespace OrderTask.Common
{
    public static class NetHelper
    {

        public static string GetPage(this string posturl)
        {
            Encoding encoding = Encoding.UTF8;
            try
            {
                // 设置参数
                if (!(WebRequest.Create(posturl) is HttpWebRequest request)) return string.Empty;
                var cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //发送请求并获取相应回应数据

                if (!(request.GetResponse() is HttpWebResponse response)) return string.Empty;

                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream instream = response.GetResponseStream();
                if (instream == null) return string.Empty;
                var sr = new StreamReader(instream, encoding);
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }

        public static string GetPostPage(this string posturl, string postData=null)
        {
            Encoding encoding = Encoding.UTF8;
            byte[] data = null;
            if (!string.IsNullOrEmpty(postData)) data = encoding.GetBytes(postData);
            try
            {
                // 设置参数
                if (!(WebRequest.Create(posturl) is HttpWebRequest request)) return string.Empty;
                var cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                if (data != null)
                {
                    request.ContentLength = data.Length;
                    Stream outstream = request.GetRequestStream();
                    outstream.Write(data, 0, data.Length);
                    outstream.Close();
                }
                //发送请求并获取相应回应数据
                if (!(request.GetResponse() is HttpWebResponse response)) return string.Empty;

                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream instream = response.GetResponseStream();
                if (instream == null) return string.Empty;
                var sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                //Response.Write(content);
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }

        public static T JsonConvert<T>(this string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// 获取所有的快递公司
        /// </summary>
        /// <returns></returns>
        public static string GetKuaidi()
        {
            var getUrl =
                string.Format("https://route.showapi.com/64-20?showapi_appid=10432&showapi_timestamp={0}&showapi_sign=0c05274d3ca7468e8e019466a8491cf9", DateTime.Now.ToString("yyyyMMddHHmmss"));
            return GetPage(getUrl);
        }

        public static string GetKuaidiNo(this string comp, string expCode)
        {
            var url = string.Format(
                "https://route.showapi.com/64-19?com={1}&nu={0}&showapi_appid=10432&showapi_timestamp={2}&showapi_sign=0c05274d3ca7468e8e019466a8491cf9",
                expCode, comp, DateTime.Now.ToString("yyyyMMddHHmmss"));
            return url.GetPage();
        }

        public static bool IsImages(this string ext)
        {
            if (ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase)) return true;
            if (ext.Equals(".png", StringComparison.OrdinalIgnoreCase)) return true;
            if (ext.Equals(".bmp", StringComparison.OrdinalIgnoreCase)) return true;
            if (ext.Equals(".jpeg", StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }

        public static string GetKuaidiInfo100(this string comp, string expCode)
        {
            string str = "http://wap.kuaidi100.com/wap_result.jsp?rand=20120517&id={0}&fromWeb=null&&postid={1}";

            return string.Format(str, comp, expCode);
        }

    }
}
