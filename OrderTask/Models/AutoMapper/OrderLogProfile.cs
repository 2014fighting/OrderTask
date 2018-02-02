using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrderTask.Model.DbModel.BisnessModel;

namespace OrderTask.Web.Models.AutoMapper
{
    public class OrderLogProfile:Profile
    {
        public OrderLogProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<OrderLog, OrderLogModel>()
                .ForMember(d => d.Operator, otp => otp.MapFrom(i => i.User.TrueName))
                ;

        }
    }
}
