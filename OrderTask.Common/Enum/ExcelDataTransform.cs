
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTask.Common.Enum
{
    /// <summary>
    /// 用于excel导入转换下拉类型数据
    /// </summary>
    public static class ExcelDataTransform
    {
        public static int GetDataType(string typeName)
        {
            switch (typeName)
            {
                case "白底图": return 1;
                case "场景图": return 2;
                case "套脚图": return 3;
                case "模特图": return 4;
                case "详情页": return 5;
                case "专辑页": return 6;
                case "标准图": return 7;
                case "广告图": return 8;
                case "入口图": return 9;
                default: return 0;
            }
        }
    }
}
