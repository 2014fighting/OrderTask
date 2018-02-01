using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using OrderTask.Model.DbModel.BisnessModel;

namespace OrderTask.Web.Models.AutoMapper
{
    public class DataManageProfile : Profile
    {
        public DataManageProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<DataManage, DataManageModel>();
            CreateMap<DataManageModel, DataManage>();
        }
    }
}
