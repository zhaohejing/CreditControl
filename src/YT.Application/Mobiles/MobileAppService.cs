

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using YT.Customers.Dtos;
using YT.Mobiles.Dtos;
using YT.Models;
using System.Linq.Dynamic;
using Abp.UI;
using Microsoft.AspNet.Identity;

namespace YT.Mobiles
{
    /// <summary>
    /// 手机端服务
    /// </summary>
    public class MobileAppService : YtAppServiceBase, IMobileAppService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<SpecialCard> _specialRepository;
        private readonly IRepository<VilidateCode> _codeRepository;
        private readonly IRepository<Straw> _strawRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<ChargeRecord> _chargeRecordRepository;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="specialRepository"></param>
        /// <param name="codeRepository"></param>
        /// <param name="strawRepository"></param>
        /// <param name="orderRepository"></param>
        /// <param name="orderItemRepository"></param>
        /// <param name="chargeRecordRepository"></param>
        public MobileAppService(IRepository<Customer> customerRepository,
            IRepository<SpecialCard> specialRepository,
            IRepository<VilidateCode> codeRepository,
            IRepository<Straw> strawRepository,
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<ChargeRecord> chargeRecordRepository)
        {
            _customerRepository = customerRepository;
            _specialRepository = specialRepository;
            _codeRepository = codeRepository;
            _strawRepository = strawRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _chargeRecordRepository = chargeRecordRepository;
        }

        #region 登陆注册相关
        /// <summary>
        /// 登陆接口 byopenId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CustomerListDto> LoginByOpenId(LoginOpenIdModel input)
        {

            var customer =
              await _customerRepository.FirstOrDefaultAsync(
                    c => c.Account.Equals(input.Name));
            if (customer == null) throw new UserFriendlyException("当前用户不存在");
            if (new PasswordHasher().VerifyHashedPassword(customer.Password, input.Password) != PasswordVerificationResult.Success)
            {
                throw new UserFriendlyException("密码错误");
            }
            if (customer.UserKey.IsNullOrWhiteSpace()) customer.UserKey = input.OpenId;//每次更新openid-----------------by gxq
            //customer.UserKey = input.OpenId;
            return customer.MapTo<CustomerListDto>();
        }
        /// <summary>
        /// 登陆接口 byspecial
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CustomerListDto> LoginBySpecial(LoginSpecialModel input)
        {
            var card =
               await _specialRepository.FirstOrDefaultAsync(
                    c => c.CardCode.Equals(input.CardCode) && c.IsUsed);
            if (card == null)
            {
                throw new UserFriendlyException("该唯鲜卡不存在");
            }

            //var customer =
            //  await _customerRepository.FirstOrDefaultAsync(c => c.SpecialId.HasValue && c.SpecialId == card.Id && c.Position.Equals(input.DeviceCode));去掉设备编码判断（因为登录没有）-------by gxq
            var customer =
              await _customerRepository.FirstOrDefaultAsync(c => c.SpecialId.HasValue && c.SpecialId == card.Id);
            if (customer == null) throw new UserFriendlyException("当前用户不存在");
            return customer.MapTo<CustomerListDto>();
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public async Task<CustomerListDto> Register(RegisterModel input)
        {
            var code = _codeRepository.GetAllList(c => c.Mobile.Equals(input.Mobile))
                .Where(c => c.CreationTime >= DateTime.Now.AddMinutes(-30)).OrderByDescending(c => c.CreationTime).FirstOrDefault();
            if (code == null)
            {
                throw new UserFriendlyException("请先获取验证码");
            }
            if (!code.Code.Equals(input.Code))
            {
                throw new UserFriendlyException("验证码错误");
            }
            var count = await _customerRepository.CountAsync(c => c.Account.Equals(input.Mobile));
            if (count > 0)
            {
                throw new UserFriendlyException("当前手机号已被注册,请登录");
            }
            var model = new Customer()
            {
                Account = input.Mobile,
                BirthDay = input.Birthday,
                CustomerName = input.CustomerName,
                Gender = input.Gender,
                IsActive = true,
                Mobile = input.Mobile,
                Password = input.Password,
                UserKey = input.OpenId,
                Avatar = input.Avatar
            };
            await _customerRepository.InsertAsync(model);
            await CurrentUnitOfWork.SaveChangesAsync();
            return model.MapTo<CustomerListDto>();
        }
        /// <summary>
        /// 绑定唯鲜卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task BindSpecialCard(BindSpecialCardModel input)
        {
            var card =
              await _specialRepository.FirstOrDefaultAsync(
                   c => c.CardCode.Equals(input.CardCode) && c.Password.Equals(input.Password));
            if (card == null)
            {
                throw new UserFriendlyException("该唯鲜卡不存在");
            }
            if (card.IsUsed)
            {
                throw new UserFriendlyException("该唯鲜卡已被使用");
            }
            var customer = await GetCurrentCustomerByOpenId(input.OpenId);
            customer.SpecialId = card.Id;
            card.IsUsed = true;
        }
        #endregion
        #region 充值相关
        /// <summary>
        /// 获取充值记录
        /// </summary>
        /// <returns></returns>
        public List<ChargeType> GetChargeTypeList()
        {
            return MilkConsts.ChargeTypes;
        }
        #endregion
        #region 吸管相关
        /// <summary>
        /// 管理员设置用户的吸管状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateCustomerStrawState(EntityDto<int> input)
        {

            //var customer =
            //  await _customerRepository.FirstOrDefaultAsync(c => c.SpecialId.HasValue && c.SpecialId == card.Id && c.Position.Equals(input.DeviceCode));

            var customer = await GetCurrentCustomerById(input.Id);
            customer.CanPickUpStraw = true;
        }
        /// <summary>
        /// 获取用户是否可以取得吸管
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> CanPickUpStraw(LoginKeyInput input)//这里要传设备编码的
        {

            var customer =
            await GetCurrentCustomerById(input.Id,input.DeviceCode);//获取是不是可以获取吸管的时候，还要判断设备编码
            var now = DateTime.Today;
            var left = now.AddDays(-(int)now.DayOfWeek + 1);
            var right = now.AddDays(8 - (int)now.DayOfWeek);
            var count =
              await _strawRepository.CountAsync(
                    c => c.CreationTime >= left && c.CreationTime < right && c.CustomerId == customer.Id);
            customer.CanPickUpStraw = count <= 0;//这个逻辑不对吧。
            return customer.CanPickUpStraw;
        }
        /// <summary>
        /// 用户获取吸管
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> PickUpStraw(LoginKeyInput input)//这里要传设备编码的
        {

            var customer =
           await GetCurrentCustomerById(input.Id, input.DeviceCode);//获取是不是可以获取吸管的时候，还要判断设备编码
            customer.CanPickUpStraw = false;
            await _strawRepository.InsertAsync(new Straw() { CustomerId = customer.Id });//这个是没看懂
            return true;// 外部调用接口要有返回值,我这么写可能不对，

        }
        #endregion
        #region 奶瓶相关
        /// <summary>
        /// 获取用户的奶瓶数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> GetCustomerBottleCount(EntityDto input)
        {
            var customer = await GetCurrentCustomerById(input.Id);
            return customer.BottleCount;
        }
        /// <summary>
        /// 核销奶瓶
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> DealCustomerBottle(DealBottleModel input)
        {
            var customer = await GetCurrentCustomerById(input.Id);
            if (input.DealCount > customer.BottleCount)
            {
                throw new UserFriendlyException("核销奶瓶数不可大于持有数");
            }
            customer.BottleCount -= input.DealCount;//这里缺少个逻辑，就是核销多少瓶，就要为余额增加多少，一个瓶子，一元钱
            customer.Balance += input.DealCount;
            return true;//外部调用接口要有返回值,我这么写可能不对，
        }
        #endregion
        #region 订单相关
        /// <summary>
        /// 获取当天订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<OrderDto>> GetUserOrdersCurrentDay(UserKeyModel input)
        {
            var end = DateTime.Today.AddDays(1);
            var today = DateTime.Today;
            var customer = await GetCurrentCustomerByOpenId(input.OpenId);
            var orders = await _orderRepository.GetAllListAsync(c => c.CustomerId == customer.Id && c.OrderTime >= today && c.OrderTime < end);
            return orders.Any() ? orders.MapTo<List<OrderDto>>() : null;
        }
        /// <summary>
        /// 获取时间段内订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<OrderDto>> GetUserOrdersRangeDay(UserOrderRangeModel input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.UserKey.Equals(input.OpenId));
            if (customer == null)
            {
                throw new UserFriendlyException("当前用户不存在");
            }
            var orders = await _orderRepository.GetAllListAsync(c => c.CustomerId == customer.Id
            && c.OrderTime >= input.Start && c.OrderTime < input.End);
            return orders.Any() ? orders.MapTo<List<OrderDto>>() : null;
        }
        /// <summary>
        /// 验证用户是否可以取奶接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<dynamic>> CanUpdateOrder(LoginKeyInput input)
        {
           var customer = await GetCurrentCustomerById(input.Id,input.DeviceCode);
            var end = DateTime.Today.AddDays(1);
            var today = DateTime.Today;
            var orders =
                _orderRepository.GetAllIncluding(c => c.OrderItems)
                    .Where(c => c.OrderTime >= today && c.OrderTime < end);
            if (!orders.Any())
            {
                throw new UserFriendlyException("当天没有订单可以核销");
            }
            if (orders.All(c => c.OrderItems.All(e => e.OrderState == OrderState.HadTake)))
            {
                throw new UserFriendlyException("当天订单商品已经全部核销");
            }
            var result = new List<dynamic>();
            foreach (var order in orders)
            {
                result.AddRange(order.OrderItems.Where(c => c.OrderState == OrderState.Predetermined).Select(c => new
                {
                    c.CommodityId,
                    c.Commodity,
                    c.Id
                }));
            }
            return result;
        }
        /// <summary>
        /// 用户核销当天商品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateOrderItemState(UserUpdateOrderModel input)//少设备编码。这个咋加。
        {
            var customer = await GetCurrentCustomerById(input.Id, input.DeviceCode);
            var orderItem = await _orderItemRepository.FirstOrDefaultAsync(c => c.Id == input.OrderItemId);
            if (orderItem == null) throw new UserFriendlyException("该商品订单不存在");
            var end = DateTime.Today.AddDays(86399F / 86400);
            var today = DateTime.Today;
            if (orderItem.Order.CreationTime < today || orderItem.Order.CreationTime > end)
            {
                throw new UserFriendlyException("仅可核销当天的订单项");
            }
            if (orderItem.Order.CustomerId != customer.Id)
            {
                throw new UserFriendlyException("非当前用户的订单项");
            }
            if (customer.Balance <= orderItem.Cost)
            {
                throw new UserFriendlyException("您的余额不足,无法核销商品");
            }
            orderItem.OrderState = OrderState.HadTake;
            orderItem.PickTime = DateTime.Now;
            customer.Balance -= orderItem.Cost;

        }
        /// <summary>
        /// 预定商品
        /// </summary>
        /// <returns></returns>
        public async Task OrderProduct(OrderProductModel input)
        {
            var customer = await GetCurrentCustomerByOpenId(input.OpenId);
            int bottleCount = 0;
            foreach (var order in input.Orders)
            {

                if (order.Order.HasValue)
                {
                    var o = await _orderRepository.FirstOrDefaultAsync(c => c.OrderNum == order.Order.Value);
                    if (o.OrderState == OrderState.Predetermined)
                    {
                        bottleCount += await DealOrder(o, o.OrderItems, order.Products);
                    }
                }
                else
                {
                    var model = new Order()
                    {
                        CustomerId = customer.Id,
                        OrderNum = Guid.NewGuid(),
                        OrderState = OrderState.Predetermined,
                        OrderTime = order.Time,
                        OrderItems = order.Products.Select(c => new OrderItem()
                        {
                            CommodityId = c.CommodityId,
                            Commodity = c.Commodity,
                            Cost = c.Cost,
                            OrderState = OrderState.Predetermined
                        }).ToList()
                    };
                    bottleCount += model.OrderItems.Count;
                    await _orderRepository.InsertAsync(model);
                }
            }
            customer.BottleCount += bottleCount;
        }
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ClearOrderProduct(UserOrderRangeModel input)
        {
            var customer = await GetCurrentCustomerByOpenId(input.OpenId);
            if (input.Start <= DateTime.Now.AddDays(3))
            {
                throw new UserFriendlyException("只能取消3天以后的订单");
            }
            await _orderRepository.DeleteAsync(
                  c => c.CustomerId == customer.Id && c.OrderTime >= input.Start && c.OrderTime <= input.End);
        }
        /// <summary>
        /// 获取用户消费记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ChargeRecordDto>> GetChargeRecord(UserChargeModel input)
        {
            var customer = await GetCurrentCustomerByOpenId(input.OpenId);
            var query = _chargeRecordRepository.GetAll();
            query = query.WhereIf(!input.Start.HasValue, c => c.CreationTime >= input.Start.Value)
                .WhereIf(!input.End.HasValue, c => c.CreationTime <= input.End.Value)
                .WhereIf(!input.ReChargeType.HasValue,
                    c => c.ReChargeType == input.ReChargeType.Value);
            var recordsCount = await query.CountAsync();

            var records = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();
            var result = new List<ChargeRecordDto>();
            foreach (var record in records)
            {
                result.Add(new ChargeRecordDto()
                {
                    CustomerName = customer.CustomerName,
                    Id = record.Id,
                    ReCharge = record.ReCharge,
                    ReChargeType = record.ReChargeType
                });
            }
            return new PagedResultDto<ChargeRecordDto>(
            recordsCount,
            result
            );
        }
        /// <summary>
        /// 获取用户七天消费记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> GetRangeTimeOrder(EntityDto<int> input)
        {
            var now = DateTime.Today;
            var right = now.AddDays(1);
            var left = now.AddDays(-7);
            var customer = await GetCurrentCustomerById(input.Id);
            var orders =
                _orderRepository.GetAllIncluding(c => c.OrderItems)
                    .Where(c => c.OrderTime >= left && c.OrderTime < right && c.CustomerId == customer.Id);
            var total = await orders.SumAsync(c => c.OrderItems.Count);
            var hapick = await orders.SumAsync(c => c.OrderItems.Count(d => d.OrderState == OrderState.HadTake));
            var cost = await orders.SumAsync(c => c.OrderItems.Sum(d => d.Cost));
            return new { total = total, hadpick = hapick, cost = cost };
        }
        #endregion
        #region 商品相关
        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetProducts()
        {
            return new List<dynamic>()
            {
                new  {CommodityId=1,Commodity="鲜奶1号",Cost=1000  },
                new  {CommodityId=2,Commodity="鲜奶2号" ,Cost=1100 },
                new  {CommodityId=3,Commodity="鲜奶3号" ,Cost=1200 },
                new  {CommodityId=4,Commodity="鲜奶4号" ,Cost=1300 },
                new  {CommodityId=5,Commodity="鲜奶5号"  ,Cost=1400},
                new  {CommodityId=6,Commodity="鲜奶6号" ,Cost=1500 },
            };
        }
        #endregion
        #region 公用方法
        /// <summary>
        /// 处理订单逻辑
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderItems"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        private async Task<int> DealOrder(Order order, ICollection<OrderItem> orderItems, List<ProductItem> products)
        {
            int count = 0;
            foreach (var product in products)
            {
                if (orderItems.Any(e => e.CommodityId == product.CommodityId)) continue;
                count++;
                await _orderItemRepository.InsertAsync(new OrderItem()
                {
                    CommodityId = product.CommodityId,
                    Cost = product.Cost,
                    OrderId = order.Id,
                    OrderState = OrderState.Predetermined
                });
            }
            foreach (var orderItem in orderItems)
            {
                if (products.Any(e => e.CommodityId == orderItem.CommodityId)) continue;
                count--;
                await _orderItemRepository.DeleteAsync(c => c.Id == orderItem.Id);
            }
            return count;
        }
        /// <summary>
        /// 根据id和设备id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deviceCode"></param>
        /// <returns></returns>
        private async Task<Customer> GetCurrentCustomerById(int id,string deviceCode="")
        {
            Customer current = null;
            if (deviceCode.IsNullOrWhiteSpace())
            {
                current = await _customerRepository.FirstOrDefaultAsync(id);
            }
            else
            {
                current = await _customerRepository.FirstOrDefaultAsync(c => c.Id == id && c.Position.Equals(deviceCode));
            }
            if (current == null)
            {
                throw new UserFriendlyException("当前用户不存在");
            }
            return current;
        }
        /// <summary>
        /// 根据openid 获取用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        private async Task<Customer> GetCurrentCustomerByOpenId(string openId)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.UserKey.Equals(openId));
            if (customer == null)
            {
                throw new UserFriendlyException("当前用户不存在");
            }
            return customer;
        }
        #endregion
    }
    /// <summary>
    /// 手机端接口
    /// </summary>
    public interface IMobileAppService : IApplicationService
    {
        /// <summary>
        /// 登陆接口 byopenId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerListDto> LoginByOpenId(LoginOpenIdModel input);
        /// <summary>
        ///  登陆接口 byspecial
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerListDto> LoginBySpecial(LoginSpecialModel input);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        Task<CustomerListDto> Register(RegisterModel input);
        /// <summary>
        /// 绑定唯鲜卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task BindSpecialCard(BindSpecialCardModel input);
        #region 吸管相关

        /// <summary>
        /// 获取用户是否可以取得吸管
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> CanPickUpStraw(LoginKeyInput input);

        /// <summary>
        /// 用户获取吸管
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> PickUpStraw(LoginKeyInput input);
        /// <summary>
        /// 管理员设置用户的吸管状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateCustomerStrawState(EntityDto<int> input);

        #endregion
        #region 奶瓶相关

        /// <summary>
        /// 获取用户的奶瓶数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> GetCustomerBottleCount(EntityDto input);

        /// <summary>
        /// 核销奶瓶
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> DealCustomerBottle(DealBottleModel input);

        #endregion
        #region 订单相关

        /// <summary>
        /// 获取当天订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<OrderDto>> GetUserOrdersCurrentDay(UserKeyModel input);
        /// <summary>
        /// 获取时间段内订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<OrderDto>> GetUserOrdersRangeDay(UserOrderRangeModel input);

        /// <summary>
        /// 用户核销当天商品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateOrderItemState(UserUpdateOrderModel input);

        /// <summary>
        /// 预定商品
        /// </summary>
        /// <returns></returns>
        Task OrderProduct(OrderProductModel input);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ClearOrderProduct(UserOrderRangeModel input);

        /// <summary>
        /// 获取用户消费记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ChargeRecordDto>> GetChargeRecord(UserChargeModel input);

        /// <summary>
        /// 获取用户可以取奶的商品接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<dynamic>> CanUpdateOrder(LoginKeyInput input);
        /// <summary>
        /// 获取用户七天消费记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<dynamic> GetRangeTimeOrder(EntityDto<int> input);
        #endregion
        #region 商品相关

        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <returns></returns>
        Task<dynamic> GetProducts();

        #endregion
    }
}

