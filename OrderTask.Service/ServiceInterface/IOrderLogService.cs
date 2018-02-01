using System;
using System.Collections.Generic;
using System.Text;
using OrderTask.Common.Definitions;
using OrderTask.Common.Enum;

namespace OrderTask.Service.ServiceInterface
{
    public interface IOrderLogService
    {
        bool AddOrderLog(int orderId, EnumOrderLogType logType,UserSession curUserSession,string content="");
    }
}
