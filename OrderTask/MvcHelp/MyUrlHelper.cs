using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using OrderTask.Common.Config;

namespace OrderTask.Web.MvcHelp
{
    public static class MyUrlHelper
    {
        public static string AddVersion(this IUrlHelper helper, string value)
        {
            string jsCssVersion = ConfigurationManager.GetSection("StaticFiles");
            if (string.IsNullOrEmpty(jsCssVersion))
            {
                return helper.Content(value);
            }
            else
            {
                return helper.Content(string.Format(value + "?_v={0}", jsCssVersion));
            }

        }
    }
}
