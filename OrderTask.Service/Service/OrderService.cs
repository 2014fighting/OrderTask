using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;

namespace OrderTask.Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool IsExistRefuse(int orderId, int curReceiveId)
        {
            return _unitOfWork.GetRepository<ReceivePerson>().GetEntities(i => i.OrderId == orderId && i.Id != curReceiveId)
                .Any(i => i.ReceiveState ==2);
        }

        public bool IsComplete(int orderId)
        {
            return !_unitOfWork.GetRepository<ReceivePerson>().GetEntities(i => i.OrderId == orderId)
                .Any(i => i.ReceiveState !=4);
        }
    }
}
