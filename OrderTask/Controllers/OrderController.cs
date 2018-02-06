using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using OrderTask.Common.Enum;
using OrderTask.Model.DbModel;
using OrderTask.Model.DbModel.BisnessModel;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using OrderTask.Web.Models;
using OrderTask.Web.Models.Common;
using OrderTask.Web.Models.SearchModel;

namespace OrderTask.Web.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        #region Constructor
        private readonly ILogService<OrderController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IOrderLogService _orderLogService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IOrderLogService orderLogService,
            IUnitOfWork unitOfWork, ILogService<OrderController> logger, IMapper mapper)
        {
            _orderLogService = orderLogService;
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddOrder(OrderModel model)
        {
            var res = new MgResult();
            if (!ModelState.IsValid)
            {
                res.Code = 110;
                res.Msg = "后端模型验证失败！";
                return Json(res);
            }
            var order = _mapper.Map<Order>(model);

            order.UserInfoId = CurUserInfo.UserId;
            order.CreateTime = DateTime.Now;
            order.OrderState = 1;
            order.CreateUser = CurUserInfo.TrueName;
            order.CreateUserId = CurUserInfo.UserId;

            var receivePerson = new List<ReceivePerson>();
            model.ReceivePersons.ForEach(i =>
            {
                receivePerson.Add(new ReceivePerson()
                {
                    CreateTime = DateTime.Now,
                    CreateUser = CurUserInfo.TrueName
                    ,
                    ReceiveState = 1,
                    UserInfoId = i
                });
            });
            order.ReceivePerson = receivePerson;
            order.OrderTypeIds = string.Join(",", model.OrderTypeIds);
            var repoOrder = _unitOfWork.GetRepository<Order>();

            repoOrder.Insert(order);
            var r = _unitOfWork.SaveChanges();

            res.Code = r > 0 ? 0 : 1;
            res.Msg = r > 0 ? "ok" : "SaveChanges失败！";
            return Json(res);
        }

        [HttpGet]
        public IActionResult AddOrder()
        {
            return View();
        }


        [HttpGet]
        public IActionResult EditOrder(int id)
        {
            var order = _unitOfWork.GetRepository<Order>().GetEntities().Include(i => i.ReceivePerson).FirstOrDefault(i => i.Id == id);
            var res = new OrderModel();
            if (order == null)
                return View();

            res.StrOrderTypeIds = order.OrderTypeIds;
            res.OrderName = order.OrderName;
            res.ExpectTime = order.ExpectTime;
            res.Degree = order.Degree;
            res.Id = order.Id;
            res.OrderDescribe = order.OrderDescribe;
            ViewBag.ismanager = CurUserInfo.RoleList.Any(i => i.Contains("经理"));
            ViewBag.ReceivePesions = string.Join(",", order.ReceivePerson.Select(i => i.UserInfoId));
 

            return View(res);
        }

        [HttpGet]
        public IActionResult ShowOrder(int id)
        {
            var order = _unitOfWork.GetRepository<Order>().GetEntities().Include(i => i.ReceivePerson).FirstOrDefault(i => i.Id == id);
            var res = new OrderModel();
            res.StrOrderTypeIds = order.OrderTypeIds;
            res.OrderName = order.OrderName;
            res.ExpectTime = order.ExpectTime;
            res.Degree = order.Degree;
            res.Id = order.Id;
            res.OrderDescribe = order.OrderDescribe;
            ViewBag.ismanager = CurUserInfo.RoleList.Any(i => i.Contains("经理"));
            ViewBag.isReceivePersion = order.ReceivePerson.Any(i=>i.UserInfoId==CurUserInfo.UserId);
            ViewBag.ReceivePesions = string.Join(",", order.ReceivePerson.Select(i => i.UserInfoId));
            ViewBag.curuserid = CurUserInfo.UserId;
            return View(res);
        }

        [HttpPost]
        public IActionResult EditOrder(OrderModel model)
        {
            var res = new MgResult();
            if (!ModelState.IsValid)
            {
                res.Code = 110;
                res.Msg = "后端验证不通过！";
                return Json(res);
            }
            var order = _unitOfWork.GetRepository<Order>().GetEntities().Include(i => i.ReceivePerson)
                .FirstOrDefault(i => i.Id == model.Id&&i.OrderState==1);
            if (order == null) {
                res.Code = 120;
                res.Msg = "只有未确认的订单才允许修改！";
                return Json(res);
            }
            if (order.CreateUser != CurUserInfo.TrueName)
            {
                res.Code = 130;
                res.Msg = "只有该订单的派单人才允许修改！";
                return Json(res);
            }
           
            order.Degree = model.Degree;order.ExpectTime = model.ExpectTime;
            order.UpdateTime=DateTime.Now;
            order.OrderName = model.OrderName;
            order.UpdateUser = CurUserInfo.TrueName;
            order.UpdateUserId = CurUserInfo.UserId;
            order.OrderDescribe = model.OrderDescribe;
         
            order.ReceivePerson.Clear();
            var lisRp = new List<ReceivePerson>();
            model.ReceivePersons.ForEach(i =>
            {
                lisRp.Add(new ReceivePerson(){UserInfoId =i,ReceiveState =1,CreateTime = DateTime.Now,OrderId =model.Id});
            });
            order.ReceivePerson = lisRp;
            order.OrderTypeIds = string.Join(",", model.OrderTypeIds.ToArray());

            var r = _unitOfWork.SaveChanges();

            res.Code = r > 0 ? 0 : 1;
            res.Msg = r > 0 ? "ok" : "SaveChanges失败！";
            return Json(res);
        }
        [HttpGet]
        public ActionResult GetOrderInfo(OrderSearch order, int page = 1, int limit = 10)
        {
            var user = _unitOfWork.GetRepository<UserInfo>().Find(CurUserInfo.UserId);
            var result = _unitOfWork.GetRepository<Order>()
                .GetEntities().Include(i => i.ReceivePerson)
                .ThenInclude(w => w.User).AsQueryable();
            if (CurUserInfo.RoleList.Any(i => i == "组长"))
            {
                result= result.Where(i => i.ReceivePerson.Any(x => x.User.Group == user.Group));
            }
            else if(CurUserInfo.RoleList.Any(i => i == "组员"))
            {
                result = result.Where(i => i.ReceivePerson.Any(x => x.UserInfoId==CurUserInfo.UserId));
            }
            else if (CurUserInfo.RoleList.Any(i => i != "经理" && !i.Contains("管理员") && i != "业务员"))
            {
                result = result.Where(i => i.ReceivePerson.Any(x => x.UserInfoId == CurUserInfo.UserId));
            }

            if (!string.IsNullOrEmpty(order.OrderName))
                result = result.Where(i => i.OrderName.Contains(order.OrderName));
            if (order.UserInfoId.HasValue)
                result = result.Where(i => i.UserInfoId == order.UserInfoId);

            var w1 = result.OrderByDescending(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var listtermp = new List<OrderModel>();
            w1.ForEach(i =>
            {
                listtermp.Add(new OrderModel()
                {
                    ExistRefuse=i.ExistRefuse,
                    StrOrderTypeIds = i.OrderTypeIds,
                    ComplateTime = i.ComplateTime,
                    Degree = i.Degree,
                    ExpectTime = i.ExpectTime,
                    FinishDescribe = i.FinishDescribe,
                    Id = i.Id,
                    OrderDescribe = i.OrderDescribe,
                    OrderName = i.OrderName,
                    OrderState = i.OrderState,
                    CreteUser = i.CreateUser,
                    StrReceivePersons = string.Join(",", i.ReceivePerson.Select(x => x.User.TrueName).ToArray())
                });
            });
            return Json(new
            {
                code = 0,
                msg = "ok",
                count = result.Count(),
                data = listtermp
            });

        }
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OrderClose(List<int> ids)
        {
            var res = new MgResult();
            var result = _unitOfWork.GetRepository<Order>();
            var temp = false;
            ids.ForEach(i =>
            {
                var x = result.Find(i);
                if (x.CreateUser != CurUserInfo.TrueName) //只允许取消自建的派单
                {
                    temp = true;
                }
               x.OrderState = 5;
                    _orderLogService.AddOrderLog(i, EnumOrderLogType.Close
                        , CurUserInfo, "取消订单操作.");
                
            });
            if (temp)
            {
                res.Code = 998;
                res.Msg = "只允许取消自己的派单！";
                return Json(res);
            }
            var r = _unitOfWork.SaveChanges() > 0;
            res.Code = r ? 0 : 1;
            res.Msg = r ? "ok" : "SaveChanges失败！";
            return Json(res);
        }

        [HttpGet]
        public ActionResult GetOrderTypeList()
        {
            var result = _unitOfWork.GetRepository<OrderType>()
                .GetEntities().ProjectTo<SelectsModel>();
            return Json(result.ToList());
        }

        /// <summary>
        /// 拒绝接单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="resion"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RefuseOrder(int orderId, string resion)
        {
            var result = new MgResult();
            var order = _unitOfWork.GetRepository<Order>().Find(orderId);

            var receivep = _unitOfWork.GetRepository<ReceivePerson>().GetEntities(i => i.OrderId == orderId && i.UserInfoId == CurUserInfo.UserId).FirstOrDefault();
            if (receivep == null)
            {
                result.Code = 1;
                result.Msg = "订单对应的接单人不存在！";
                return Json(result);
            }
            if (receivep.ReceiveState == 3)
            {
                result.Code = 2;
                result.Msg = "该订单您已经拒绝了！请等待经理重新指派.";
                return Json(result);
            }
            if (receivep.ReceiveState!=1)
            {
                result.Code = 3;
                result.Msg = "只有未操作的状态才允许拒绝接单！";
                return Json(result);
            }
            receivep.ReceiveState = 3;//拒绝
            receivep.RefuseResion = resion;

            order.OrderState = 2;//接单中
            order.ExistRefuse = true;//一旦有拒绝的接单人就标记拒绝状态
            _orderLogService.AddOrderLog(orderId, EnumOrderLogType.Refuse
                , CurUserInfo, "拒绝接单动作.");
            var r = _unitOfWork.SaveChanges();

            result.Code = r > 0 ? 0 : 1;
            result.Msg = r > 0 ? "ok" : "SaveChanges失败！";
            return Json(result);
        }

        /// <summary>
        /// 确认接单
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReceiveOrder(int orderId)
        {
            var result = new MgResult();
            var user = _unitOfWork.GetRepository<UserInfo>().Find(CurUserInfo.UserId);
            var order = _unitOfWork.GetRepository<Order>().Find(orderId);
            var receivep = _unitOfWork.GetRepository<ReceivePerson>().GetEntities(i => i.OrderId == orderId && i.UserInfoId == CurUserInfo.UserId).FirstOrDefault();
            if (receivep == null)
            {
                result.Code = 1;
                result.Msg = "订单对应的接单人不存在！";
                return Json(result);
            }
            if (receivep.ReceiveState !=1)
            {
                result.Code = 2;
                result.Msg = "只有未操作的状态才允许确认接单！";
                return Json(result);
            }
            var needData = order.ExpectTime - DateTime.Now;//任务需要天数
            user.JobScheduling = user.JobScheduling< DateTime.Now ? DateTime.Now : user.JobScheduling;//排期小于当前时间则等于当前时间
            if (needData != null&& user.JobScheduling != null)
                user.JobScheduling = user.JobScheduling.Value.Add(needData.Value);
            receivep.ReceiveState = 2;//同意接单
            receivep.ReceiveTime = DateTime.Now;
            receivep.RefuseResion = "";
            order.OrderState = 2;//接单中
            _orderLogService.AddOrderLog(orderId, EnumOrderLogType.Receive
                , CurUserInfo, "确认接单.");
            var r = _unitOfWork.SaveChanges();

            result.Code = r > 0 ? 0 : 1;
            result.Msg = r > 0 ? "ok" : "SaveChanges失败！";
            return Json(result);
        }

        /// <summary>
        /// 确认完成
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="remark">订单完成备注</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConfrimeOrder(int orderId,string remark)
        {
            var result = new MgResult();
            var receivep = _unitOfWork.GetRepository<ReceivePerson>().GetEntities(i => i.OrderId == orderId && i.UserInfoId == CurUserInfo.UserId).FirstOrDefault();
            if (receivep == null)
            {
                result.Code = 1;
                result.Msg = "订单对应的接单人不存在！";
                return Json(result);
            }
            if (receivep.ReceiveState != 2)
            {
                result.Code = 2;
                result.Msg = "只有同意接单的状态才允许确认完成！";
                return Json(result);
            }
            receivep.ReceiveState = 4;//确认完成
            receivep.CompleteTime = DateTime.Now;
            receivep.Remark = remark;
            //添加订单操作日志
            _orderLogService.AddOrderLog(orderId, EnumOrderLogType.Confirm
                , CurUserInfo, "确认完成订单.");
            var r= _unitOfWork.SaveChanges();
            result.Code = r > 0 ? 0 : 1;
            result.Msg = r > 0 ? "ok" : "SaveChanges失败！";
            if (r > 0)
            {
                var order = _unitOfWork.GetRepository<Order>().Find(orderId);

                //判断所有接单人是否都完成 若都完成才能吧订单状态改成已完成
                if (_orderService.IsComplete(orderId))
                {
                     order.OrderState = 3;//已完成
                    _orderLogService.AddOrderLog(orderId, EnumOrderLogType.ChangeOrderstate
                        , CurUserInfo, "确认完成订单.同时更新订单状态为已完成！！");
                }
                    
                var w= _unitOfWork.SaveChanges();
                if(w==0)
                    _logger.Error("确认完成订单.同时更新订单状态失败。");
            }
        
            return Json(result);
        }

        /// <summary>
        /// 重新指派
        /// </summary>
        /// <param name="userId">被指派的人</param>
        /// <param name="receiveId">指派行编号</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReAppoint(int userId, int receiveId)
        {
            var result = new MgResult();
            var receive = _unitOfWork.GetRepository<ReceivePerson>().GetEntities().Include(i => i.User)
                .FirstOrDefault(i => i.Id == receiveId && i.ReceiveState == 3);
            if (receive == null)
            {
                result.Code = 1;
                result.Msg = "该接单人未拒绝不需要重新分配！";
                return Json(result);
            }
            receive.UserInfoId = userId;
            receive.ReceiveState =1;//重新指派后默认人为接收

            _orderLogService.AddOrderLog(Convert.ToInt32(receive.OrderId), EnumOrderLogType.ReAppont
                , CurUserInfo, "重新指派,原接单人:" + receive.User.TrueName + ",拒绝原因:" + receive.RefuseResion);
            receive.RefuseResion = "";
            
            //变更订单是否存在拒绝的人字段
            var curorder = _unitOfWork.GetRepository<Order>().Find(receive.OrderId);
            if (receive.OrderId != null)
                curorder.ExistRefuse = _orderService.IsExistRefuse(receive.OrderId.Value,receiveId);

            var r = _unitOfWork.SaveChanges();

            result.Code = r > 0 ? 0 : 1;
            result.Msg = r > 0 ? "ok" : "SaveChanges失败！";
            return Json(result);
        }
    }
}