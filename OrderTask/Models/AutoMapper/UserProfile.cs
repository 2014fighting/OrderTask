using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrderTask.Model.DbModel;

namespace OrderTask.Web.Models.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<UserInfo, UserInfoModel>().ForMember(d => d.UserRoles, otp => otp.Ignore());
            CreateMap<UserInfoModel, UserInfo>().ForMember(d => d.UserRoles, otp => otp.Ignore());
        }
    }
}
