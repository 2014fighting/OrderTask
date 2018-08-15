using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderTask.Model.DbModel;
using OrderTask.UnitOfWork;
using OrderTask.Web.Models.Common;

namespace OrderTask.Web.SysFilter
{
    public class ManagerAuthorFilter: ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public ManagerAuthorFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int Message { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = _unitOfWork.GetRepository<UserInfo>().Find(Message);
            if (user.DepartMentId != 3 || user.UserRoles.Any(i => i.RoleId != 1))
            {
                return;
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

    }
}
