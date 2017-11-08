using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Net.Mail.Smtp;
using Abp.UI;
using YT.ChargeRecords.Dtos;
using YT.Customers.Dtos;
using YT.Dashboards.Dtos;
using YT.Models;
using YT.Products.Dtos;
using YT.Storage;


namespace YT.Dashboards
{
    /// <summary>
    /// 前段接口
    /// </summary>
    public class DashboardAppService : YtAppServiceBase, IDashboardAppService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly ISmtpEmailSenderConfiguration _smtpEmailSenderConfiguration;
        private readonly IRepository<Category> _cateRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<Order> _ordeRepository;
        private readonly IRepository<CustomerCost> _costRepository;
        private readonly IRepository<ApplyCharge> _applyRepository;
        private readonly IRepository<CustomerForm> _formRepository;
        private readonly IBinaryObjectManager _objectManager;


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="smtpEmailSenderConfiguration"></param>
        /// <param name="cateRepository"></param>
        /// <param name="productRepository"></param>
        /// <param name="binaryObjectManager"></param>
        /// <param name="ordeRepository"></param>
        /// <param name="costRepository"></param>
        /// <param name="applyRepository"></param>
        /// <param name="objectManager"></param>
        /// <param name="formRepository"></param>
        public DashboardAppService(
            IRepository<Customer> customerRepository,
            ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration,
            IRepository<Category> cateRepository,
            IRepository<Product> productRepository,
            IBinaryObjectManager binaryObjectManager,
            IRepository<Order> ordeRepository, IRepository<CustomerCost> costRepository, IRepository<ApplyCharge> applyRepository, IBinaryObjectManager objectManager, IRepository<CustomerForm> formRepository)
        {
            _customerRepository = customerRepository;
            _smtpEmailSenderConfiguration = smtpEmailSenderConfiguration;
            _cateRepository = cateRepository;
            _productRepository = productRepository;
            _binaryObjectManager = binaryObjectManager;
            _ordeRepository = ordeRepository;
            _costRepository = costRepository;
            _applyRepository = applyRepository;
            _objectManager = objectManager;
            _formRepository = formRepository;
        }

        /// <summary>
        /// 客户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CustomerListDto> Authenticate(CustomerLoginDto input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.Account.Equals(input.Account));
            if (customer == null) throw new UserFriendlyException("该账户不存在");
            if (!customer.Password.Equals(input.Password)) throw new UserFriendlyException("密码错误");
            var dto = customer.MapTo<CustomerListDto>();
            if (customer.License.HasValue)
            {
                var pro = await _objectManager.GetOrNullAsync(customer.License.Value);
                if (pro != null)
                {
                    dto.LicenseUrl = Host + pro.Url;

                }
            }
            if (customer.IdentityCard.HasValue)
            {
                var pro = await _objectManager.GetOrNullAsync(customer.IdentityCard.Value);
                if (pro != null)
                {
                    dto.IdentityCardUrl = Host + pro.Url;
                }
            }
            return dto;
        }
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CustomerListDto> GetInfo(EntityDto<int> input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(input.Id);
            if (customer == null) throw new UserFriendlyException("该账户不存在");
            var dto = customer.MapTo<CustomerListDto>();
            if (customer.License.HasValue)
            {
                var pro = await _objectManager.GetOrNullAsync(customer.License.Value);
                if (pro != null)
                {
                    dto.LicenseUrl = Host + pro.Url;

                }
            }
            if (customer.IdentityCard.HasValue)
            {
                var pro = await _objectManager.GetOrNullAsync(customer.IdentityCard.Value);
                if (pro != null)
                {
                    dto.IdentityCardUrl = Host + pro.Url;
                }
            }
            return dto;
        }
        /// <summary>
        /// 完成订单
        /// </summary>
        /// <returns></returns>
        public async Task CompleteOrder(CompleteOrderInput input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.Id == input.CustomerId);
            if (customer == null) throw new UserFriendlyException("该账户不存在");
            var order = await _ordeRepository.FirstOrDefaultAsync(c => c.OrderNum.Equals(input.OrderNum));
            if (order == null) throw new UserFriendlyException("该订单不存在");
            order.State = input.State;
            if (input.State)
            {
                if (customer.Balance < order.TotalPrice)
                {
                    throw new UserFriendlyException("账户下余额不足,请充值后再试");
                }
                var cost = new CustomerCost()
                {
                    Balance = customer.Balance,
                    CustomerId = customer.Id
                };
                customer.Balance -= order.TotalPrice;
                cost.Cost = order.TotalPrice;
                cost.CurrentBalance = customer.Balance;

                await _costRepository.InsertAsync(cost);
            }
            else
            {
                order.CancelReason = input.CancelReason;
            }
        }
        /// <summary>
        /// 创建订单扩展信息
        /// </summary>
        /// <returns></returns>
        public async Task ModifyForm(CustomerFormEditDto input)
        {
            var order = await _ordeRepository.FirstOrDefaultAsync(input.OrderId);
            if(order==null)throw new UserFriendlyException("订单信息不存在");
            var form = input.MapTo<CustomerForm>();
            await _formRepository.InsertOrUpdateAsync(form);
            await CurrentUnitOfWork.SaveChangesAsync();
            order.FormId = form.Id;
        }
        /// <summary>
        /// 获取用户消费记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<CustomerCostListDto>> GetCustomerCosts(GetCustomerCostsInput input)
        {
            var now = DateTime.Now.Date;
            DateTime left = now.AddMonths(-input.Num);
            var lists = await _costRepository.GetAllListAsync(c => c.CustomerId == input.CustomerId
                                                                   && c.CreationTime >= left
            );
            if (!lists.Any()) return null;
            return lists.MapTo<List<CustomerCostListDto>>();
        }

        /// <summary>
        /// 获取订单扩展信息
        /// </summary>
        /// <returns></returns>
        public async Task<CustomerFormEditDto> GetFormByOrder(EntityDto input)
        {
            var order = await _ordeRepository.FirstOrDefaultAsync(input.Id);
            if (order.Form != null)
            {
                var model = order.Form.MapTo<CustomerFormEditDto>();
                if (model.License.HasValue)
                {
                    model.LicenseUrl = Host + (await _binaryObjectManager.GetOrNullAsync(model.License.Value))?.Url;
                }
                if (model.IdentityCard.HasValue)
                {
                    model.IdentityCardUrl = Host + (await _binaryObjectManager.GetOrNullAsync(model.IdentityCard.Value))?.Url;
                }
                if (model.CompanyLogo.HasValue)
                {
                    model.CompanyLogoUrl = Host + (await _binaryObjectManager.GetOrNullAsync(model.CompanyLogo.Value))?.Url;
                }
                if (model.PermitCard.HasValue)
                {
                    model.PermitCardUrl = Host + (await _binaryObjectManager.GetOrNullAsync(model.PermitCard.Value))?.Url;
                }
                return model;
            }
            return new CustomerFormEditDto();
        }
        /// <summary>
        /// 提交充值申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task ApplyCharge(ApplyChargeInput input)
        {
            var apply = input.MapTo<ApplyCharge>();
            await _applyRepository.InsertAsync(apply);
        }
        /// <summary>
        /// 获取用户菜单集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<TreeModel>> GetCustomerMenus(EntityDto<int> input)
        {

            var customer = await _customerRepository.FirstOrDefaultAsync(input.Id);
            if (customer == null) throw new UserFriendlyException("该账户不存在");

            var cates = await _cateRepository.GetAllListAsync(c => !c.ParentId.HasValue);
            if (!cates.Any()) return null;
            var temp = cates.Select(
                c =>
                    new TreeModel(c.Id, c.Name, "/dashboard", "",
                        c.Childern.Select(d => new TreeModel(d.Id, d.Name, "/dashboard")).ToList())).ToList();
            return temp;
        }

        /// <summary>
        /// 获取所有产品列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ProductDetail>> GetProducts(NullableIdDto<int> input)
        {
            var products = await _productRepository.GetAll().Where(c => c.IsActive)
                .WhereIf(input.Id.HasValue, c => c.LevelTwoId == input.Id.Value).ToListAsync();
            var output = new List<ProductDetail>();
            if (!products.Any()) return output;
            foreach (var product in products)
            {
                var dto = product.MapTo<ProductDetail>();
                if (product.Profile.HasValue)
                {
                    var file = await _binaryObjectManager.GetOrNullAsync(product.Profile.Value);
                    if (file != null)
                    {
                        dto.ProfileUrl = Host + file.Url;
                    }
                }

                output.Add(dto);
            }
            return output;
        }

        /// <summary>
        /// 获取所有产品列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<OrderProductDetail>> GetHaveProducts(GetHaveProductInput input)
        {
          
            var query = _ordeRepository.GetAll().Where(c=>c.CustomerId==input.CustomerId);
            var chargeRecordCount = await query.CountAsync();
            var chargeRecords = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();
            var output = new List<OrderProductDetail>();
            if (chargeRecordCount<=0) return new PagedResultDto<OrderProductDetail>(0,output);
            foreach (var o in chargeRecords)
            {
                var dto = new OrderProductDetail()
                {
                    Id = o.Id,
                    Cate = o.LevelTwo,
                    Count = o.Count,
                    FormId = o.FormId,
                    Price = o.Price,
                    ProductName = o.ProductName,
                    TotalPrice = o.TotalPrice,
                    OrderNum = o.OrderNum,
                    CreationTime = o.CreationTime,
                    State = o.State,
                    ProductId = o.Id
                };
                if (o.Profile.HasValue)
                {
                    dto.Profile = Host + (await _objectManager.GetOrNullAsync(o.Profile.Value))?.Url;
                }
                output.Add(dto);
            }
            return new PagedResultDto<OrderProductDetail>(
            chargeRecordCount,
            output
            );
        }
        /// <summary>
        /// 获取所有产品列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OrderProductDetail> GetHaveProduct(EntityDto<int> input)
        {
            var order = await _ordeRepository.FirstOrDefaultAsync(input.Id);
            if (order == null) throw new UserFriendlyException("该订单不存在");
            var dto = new OrderProductDetail()
            {
                Id = order.Id,
                Cate = order.LevelTwo,
                Count = order.Count,
                FormId = order.FormId,
                Price = order.Price,
                ProductName = order.ProductName,
                TotalPrice = order.TotalPrice,
                OrderNum = order.OrderNum,
                CreationTime = order.CreationTime,
                State = order.State,
                ProductId = order.ProductId
            };
            if (order.Profile.HasValue)
            {
                dto.Profile = Host + (await _objectManager.GetOrNullAsync(order.Profile.Value))?.Url;
            }
            return dto;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ChangePassword(ChangeCustomerPasswordInput input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.Email.Equals(input.Email));
            if (customer == null) throw new UserFriendlyException("该账户不存在");
            if (!customer.EmailCode.Equals(input.Code)) throw new UserFriendlyException("重置码错误");
            if (!customer.CodeTime.HasValue) throw new UserFriendlyException("重置码已过期,请重新获取");
            if (customer.CodeTime.Value <= DateTime.Now.AddMinutes(-30))
                throw new UserFriendlyException("重置码已过期,请重新获取");
            customer.Password = input.Password;
            customer.EmailCode = string.Empty;
        }
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <returns></returns>
        public async Task<OrderListDto> CreateOrder(CreateOrderInput input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.Id == input.CustomerId);
            if (customer == null) throw new UserFriendlyException("该账户不存在");
            var product = await _productRepository.FirstOrDefaultAsync(c => c.Id == input.ProductId);
            if (product == null) throw new UserFriendlyException("该产品不存在");
            var totalPrice = product.Price * input.Count;
            var dto = new Order()
            {
                CustomerId = input.CustomerId,
                OrderNum = Guid.NewGuid().ToString("N"),
                State = null,
                Count = input.Count,
                TotalPrice = totalPrice,
                ProductId = product.Id,
                Price = product.Price,
                ProductName = product.ProductName,
                LevelOne = product.LevelOne.Name,
                LevelTwo = product.LevelTwo.Name,
                Profile = product.Profile
            };
            dto = await _ordeRepository.InsertAsync(dto);
            await CurrentUnitOfWork.SaveChangesAsync();
            return dto.MapTo<OrderListDto>();
        }



        /// <summary>
        /// 获取所有产品列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ProductDetail> GetProductDetail(EntityDto<int> input)
        {
            var product = await _productRepository.FirstOrDefaultAsync(c => c.Id == input.Id && c.IsActive);

            var dto = product.MapTo<ProductDetail>();
            if (!product.Profile.HasValue) return dto;
            var file = await _binaryObjectManager.GetOrNullAsync(product.Profile.Value);
            if (file != null)
            {
                dto.ProfileUrl = Host + file.Url;
            }
            return dto;

        }

        /// <summary>
        /// 重置密码链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ResetPassword(ResetPasswordInput input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.Email.Equals(input.Email));
            if (customer == null) throw new UserFriendlyException("该邮箱绑定的账户不存在");
            var code = Guid.NewGuid().ToString().Split('-').Last();
            customer.EmailCode = code;
            customer.CodeTime = DateTime.Now;
            var body = $@"<div>您的验证码:{code},请及时使用以防过期</div>";
            await SendEmail(customer.Email, body);
        }

        /// <summary>
        /// 客户注册
        /// </summary>
        /// <returns></returns>
        public async Task Register(CustomerEditDto input)
        {
            if (!input.Password.Equals(input.RepeatPassword)) throw new UserFriendlyException("两次密码不一致");
            var count =
                await
                    _customerRepository.CountAsync(c => c.Account.Equals(input.Account) || c.Email.Equals(input.Email));
            if (count > 0) throw new UserFriendlyException("该账户或邮箱已被注册,请重试");
            var model = input.MapTo<Customer>();
            await _customerRepository.InsertAsync(model);
        }
        /// <summary>
        /// 用户modify
        /// </summary>
        /// <returns></returns>
        public async Task Modify(CustomerEditDto input)
        {
            if (!input.Password.Equals(input.RepeatPassword)) throw new UserFriendlyException("两次密码不一致");
            var count =
                await
                    _customerRepository.CountAsync(c => (c.Account.Equals(input.Account) || c.Email.Equals(input.Email)) && c.Id != input.Id.Value);
            if (count > 0) throw new UserFriendlyException("该账户或邮箱已被注册,请重试");
            var model = input.MapTo<Customer>();
            await _customerRepository.UpdateAsync(model);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        private async Task SendEmail(string to, string body, bool isHtml = true)
        {
            try
            {
                SmtpEmailSender emailSender = new SmtpEmailSender(_smtpEmailSenderConfiguration);
                await emailSender.SendAsync(to, "密码重置链接", body, isHtml);
            }
            catch (Exception e)
            {

                throw new UserFriendlyException(e.Message);
            }

        }
    }
}
