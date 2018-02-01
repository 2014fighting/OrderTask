using System;
using System.Collections.Generic;
using System.Text;
using OrderTask.Common.Definitions;
using OrderTask.Common.Enum;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;

namespace OrderTask.Service.Service
{
    public class OrderLogService : IOrderLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool AddOrderLog(int orderId, EnumOrderLogType logType, UserSession curUserSession, string content = "")
        { 
            var repOrderLog=_unitOfWork.GetRepository<OrderLog>();
            repOrderLog.Insert(new OrderLog()
            {
                CreteUser =curUserSession.TrueName,
                Operator = curUserSession.UserId
                ,CreteTime = DateTime.Now,
                Describe =content,
                OperationType =Convert.ToInt32(logType),
                OrderId =orderId
            });
            return true;
            //return _unitOfWork.SaveChanges() > 0;
        }
    }
}
