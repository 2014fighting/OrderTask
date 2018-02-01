using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrderTask.Model.DbModel.BisnessModel;

namespace OrderTask.Web.Models.AutoMapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Order, OrderModel>().ForMember(d => d.OrderTypeIds, otp => otp.Ignore())
                .ForMember(d => d.StrOrderTypeIds, otp => otp.MapFrom(i=>i.OrderTypeIds))
                .ForMember(d => d.ReceivePersons, otp => otp.Ignore()) ;
            CreateMap<OrderModel, Order>().ForMember(d => d.OrderTypeIds, otp => otp.Ignore())
                .ForMember(d => d.ReceivePerson, otp => otp.Ignore());
        }
    }
}
