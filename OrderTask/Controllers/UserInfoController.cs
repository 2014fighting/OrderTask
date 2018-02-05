using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using OrderTask.Model;
using OrderTask.Model.DbModel;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using OrderTask.Web.Models;
using OrderTask.Web.Models.Common;

namespace OrderTask.Web.Controllers
{
    [Authorize]
    public class UserInfoController : BaseController
    {
        #region Constructor
        private readonly ILogService<UserInfoController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public readonly OrderTaskContext _context;
        private readonly IUserInfoService _userInfoService;

        // Create a field to store the mapper object
        private readonly IMapper _mapper;

        public UserInfoController(IUserInfoService userInfoService,
            IUnitOfWork unitOfWork, ILogService<UserInfoController> logger, IMapper mapper,OrderTaskContext context)
        {
            _unitOfWork = unitOfWork;
            _userInfoService = userInfoService;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        #endregion


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddUser(UserInfoModel model)
        {
            var res = new MgResult();
            //权限先写死
            if (!CurUserInfo.RoleList.Any(i => i == "部门经理" || i.Contains("管理员")))
            {
                res.Code = 999;
                res.Msg = "您没有权限操作！";
                return Json(res);
            }
            if (!ModelState.IsValid)
            {
                res.Code = 110;
                res.Msg = "后端模型验证失败！";
                return Json(res);
            }
            try
            {
                var user = _mapper.Map<UserInfo>(model);

                var repoUser = _unitOfWork.GetRepository<UserInfo>();
                user.UserRoles = new List<UserRole>();
                model.UserRoles.ForEach(i =>
                {
                    user.UserRoles.Add(new UserRole { RoleId = i });
                });
                user.CreateUser = CurUserInfo.TrueName;
                //user.CreateUserId = CurUserInfo.UserId;
                repoUser.Insert(user);
                var r = _unitOfWork.SaveChanges(); res.Code = r > 0 ? 0 : 1;
                res.Msg = r > 0 ? "ok" : "SaveChanges失败！";
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
          
            return Json(res);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">1编辑 2查看</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditUser(int id, int type = 1)
        {
            ViewBag.pageType = type;

            var user = _unitOfWork.GetRepository<UserInfo>().GetFirstOrDefault(i => i.Id == id, include:
                i => i.Include(ur => ur.UserRoles));
            var temps = string.Empty;
            user.UserRoles.ForEach(i =>
            {
                temps += i.RoleId + ",";
            });
            ViewBag.Roles = temps.TrimEnd(',');
            return View(_mapper.Map<UserInfoModel>(user));
        }

        [HttpPost]
        public IActionResult EditUser(UserInfoModel model)
        {
            var res = new MgResult();
            //权限先写死
            if (!CurUserInfo.RoleList.Any(i => i == "部门经理" || i.Contains("管理员"))&&model.Id!=CurUserInfo.UserId)
            {
                res.Code = 999;
                res.Msg = "您没有权限操作！";
                return Json(res);
            }
            if (!ModelState.IsValid)
            {
                res.Code = 110;
                res.Msg = "后端验证不通过！";
                return Json(res);
            }
      
            var user = _unitOfWork.GetRepository<UserInfo>().GetEntities().Include(i => i.UserRoles)
                .FirstOrDefault(i => i.Id == model.Id);
            if (user == null)
            {
                res.Code = 120;
                res.Msg = "用户不存在！";
                return Json(res);
            }
            if (user.UserRoles.Any())
            {
                user.UserRoles.Clear();
                //为啥这个clear 会报错 ,多对多不允许这样直接clear，要先savechage()?
                if (_unitOfWork.SaveChanges() == 0)
                {
                    res.Code = 130;
                    res.Msg = "修改用户信息失败！";
                    return Json(res);
                }
            }
             
            user.UpdateTime = DateTime.Now;
            user.UpdateUser = CurUserInfo.TrueName;
            user.UpdateUserId = CurUserInfo.UserId;
            _mapper.Map(model, user);
             
            var lisUr = new List<UserRole>();
            model.UserRoles.ForEach(i =>
            {
                lisUr.Add(new UserRole { RoleId = i, UserId = user.Id });
            });
            user.UserRoles = lisUr;

            var r = _unitOfWork.SaveChanges();

            res.Code = r > 0 ? 0 : 1;
            res.Msg = r > 0 ? "ok" : "SaveChanges失败！";
            return Json(res);
        }
        [HttpGet]
        public ActionResult GetUserInfo(UserInfoModel user, int page = 1, int limit = 10)
        {
            //var w = _unitOfWork.GetRepository<UserInfo>().GetPagedList(pageIndex:page-1,pageSize:limit);

            var result = _unitOfWork.GetRepository<UserInfo>().GetEntities();
            if (!string.IsNullOrEmpty(user.UserName))
                result = result.Where(i => i.UserName.Contains(user.UserName));
            if (!string.IsNullOrEmpty(user.PhoneNo))
                result = result.Where(i => i.PhoneNo.Contains(user.PhoneNo));
            if (!string.IsNullOrEmpty(user.TrueName))
                result = result.Where(i => i.TrueName.Contains(user.TrueName));
            var w1 = result.OrderByDescending(x => x.Id).Skip((page - 1) * limit).Take(limit);
            return Json(new
            {
                code = 0,
                msg = "ok",
                count = result.Count(),
                data = w1.ToList()
            });

        }

        [HttpPost]
        public ActionResult UserDelete(List<int> ids)
        {
            var res = new MgResult();
             //权限先写死
            if (!CurUserInfo.RoleList.Any(i => i == "部门经理" || i.Contains("管理员")))
            {
                res.Code = 999;
                res.Msg = "您没有权限操作！";
                return Json(res);
            }
            var result = _unitOfWork.GetRepository<UserInfo>();
            ids.ForEach(i =>
            {
                result.Delete(result.Find(i));
            });
            var r = _unitOfWork.SaveChanges() > 0;
            res.Code = r ? 0 : 1;
            res.Msg = r ? "ok" : "SaveChanges失败！";
            return Json(res);
        }

        [HttpGet]
        public ActionResult GetUserList()
        {
            var lisSelectGroup=new List<SelectGroup>();
            var represult = _unitOfWork.GetRepository<UserInfo>();
           var result1= represult.GetEntities(i => i.Group != null).Select(i=>i.Group).Distinct().ToList();
            result1.ForEach(i =>
            {
                var tempuser = represult.GetEntities(x => x.Group == i).ProjectTo<SelectsModel>().ToList();
                lisSelectGroup.Add(new SelectGroup(){Group =i, SelectsModel=tempuser});
            });
               //var result = _unitOfWork.GetRepository<UserInfo>()
               // .GetEntities().ProjectTo<SelectsModel>();
            return Json(lisSelectGroup.ToList());
        }


        [HttpGet]
        public ActionResult GetReceivePesionList(int orderId)
        {
            var list=new List<ReceivePersonModel>();
            var represult = _unitOfWork.GetRepository<ReceivePerson>().GetEntities(i => i.OrderId == orderId).Include(i=>i.User).ToList();
            represult.ForEach(i =>
            {
                list.Add(new ReceivePersonModel()
                {
                    Id =i.Id,Group =i.User.Group
                    ,JobScheduling =i.User.JobScheduling,
                    RefuseResion =i.RefuseResion,ReceiveState =i.ReceiveState,
                    Remark = i.Remark,TrueName = i.User.TrueName,
                    UserId=i.User.Id
                });
            });
            return Json(new
            {
                code = 0,
                msg = "ok",
                count = list.Count(),
                data = list.ToList()
            });
        }
    }
}