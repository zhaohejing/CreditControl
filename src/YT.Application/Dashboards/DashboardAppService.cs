using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Net.Mail.Smtp;
using Abp.UI;
using Nito.AsyncEx.Synchronous;
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
    [DisableAuditing]
    public class DashboardAppService : YtAppServiceBase, IDashboardAppService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly ISmtpEmailSenderConfiguration _smtpEmailSenderConfiguration;
        private readonly IRepository<Category> _cateRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<Order> _ordeRepository;

        /// <summary>
        /// host
        /// </summary>
        private static readonly string Host = ConfigurationManager.AppSettings.Get("WebSiteRootAddress");

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="smtpEmailSenderConfiguration"></param>
        /// <param name="cateRepository"></param>
        /// <param name="productRepository"></param>
        /// <param name="binaryObjectManager"></param>
        /// <param name="ordeRepository"></param>
        public DashboardAppService(
            IRepository<Customer> customerRepository,
            ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration,
            IRepository<Category> cateRepository,
            IRepository<Product> productRepository,
            IBinaryObjectManager binaryObjectManager,
            IRepository<Order> ordeRepository)
        {
            _customerRepository = customerRepository;
            _smtpEmailSenderConfiguration = smtpEmailSenderConfiguration;
            _cateRepository = cateRepository;
            _productRepository = productRepository;
            _binaryObjectManager = binaryObjectManager;
            _ordeRepository = ordeRepository;
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
            return customer.MapTo<CustomerListDto>();
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
                customer.Balance -= order.TotalPrice;
            }
            else
            {
                order.CancelReason = input.CancelReason;
            }
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
            var products = await _productRepository.GetAll()
                .WhereIf(input.Id.HasValue, c => c.LevelTwoId == input.Id.Value && c.IsActive).ToListAsync();
            var output = new List<ProductDetail>();
            if (!products.Any()) return output;
            foreach (var product in products)
            {
                var dto = product.MapTo<ProductDetail>();
                if (!product.Profile.HasValue) continue;
                var file = await _binaryObjectManager.GetOrNullAsync(product.Profile.Value);
                if (file != null)
                {
                    dto.ProfileUrl = Host + file.Url;
                }
                output.Add(dto);
            }
            return output;
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
        public async Task CreateOrder(CreateOrderInput input)
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
            };
            await _ordeRepository.InsertAsync(dto);
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
