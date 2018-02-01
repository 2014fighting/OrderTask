using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrderTask.Model.DbModel;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Web.Models.Common;

namespace OrderTask.Web.Models.AutoMapper
{
    public class SelectsModelProfile : Profile
    {
        public SelectsModelProfile()
        {
            CreateMap<RoleInfo, SelectsModel>().ForMember(d => d.value, otp => otp.MapFrom(i => i.Id))
                .ForMember(d => d.text, otp => otp.MapFrom(i => i.RoleName));
            CreateMap<DepartMent, SelectsModel>().ForMember(d => d.value, otp => otp.MapFrom(i => i.Id))
                .ForMember(d => d.text, otp => otp.MapFrom(i => i.DptName));
            CreateMap<OrderType ,SelectsModel>().ForMember(d => d.value, otp => otp.MapFrom(i => i.Id))
                .ForMember(d => d.text, otp => otp.MapFrom(i => i.TypeName));


            CreateMap<UserInfo, SelectsModel>().ForMember(d => d.value, otp => otp.MapFrom(i => i.Id))
                .ForMember(d => d.text, otp => otp.MapFrom
                (i => i.TrueName+"_"+(i.JobScheduling.HasValue?i.JobScheduling.Value.ToShortDateString():"")));
        }
    }
}
