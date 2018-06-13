using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OrderTask.Common.Definitions;
using OrderTask.Common.Help;
using OrderTask.Model.DbModel;

namespace OrderTask.Service.ServiceInterface
{
    public interface IImportManagerService
    {
        ResultModel ImportDataManageFromXlsx(Stream stream, UserSession curUserInfo);
    }
}
