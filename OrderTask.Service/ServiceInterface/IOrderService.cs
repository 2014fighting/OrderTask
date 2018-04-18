using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTask.Service.ServiceInterface
{
    public interface IOrderService
    {
        bool IsComplete(int orderId);

        bool IsExistRefuse(int orderId,int curReceiveId);

        DateTime CaculateAutoTime(DateTime dateTime,int t=2);
    }
}
