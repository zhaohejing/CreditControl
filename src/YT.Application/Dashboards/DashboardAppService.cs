using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Net.Mail.Smtp;
using Abp.UI;
using YT.Customers.Dtos;
using YT.Dashboards.Dtos;
using YT.Models;

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
       /// <summary>
       /// ctor
       /// </summary>
       /// <param name="customerRepository"></param>
       /// <param name="smtpEmailSenderConfiguration"></param>
        public DashboardAppService(IRepository<Customer> customerRepository,
            ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration)
        {
            _customerRepository = customerRepository;
            _smtpEmailSenderConfiguration = smtpEmailSenderConfiguration;
        }
        /// <summary>
        /// 客户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CustomerListDto> Authenticate(CustomerLoginDto input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.Account.Equals(input.Account));
            if (customer==null) throw new UserFriendlyException("该账户不存在");
            if (!customer.Password.Equals(input.Password)) throw new UserFriendlyException("密码错误");
            return customer.MapTo<CustomerListDto>();
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
            var body = $@"<div>您的验证码:{code},请在30分钟内点击下边的链接以重置密码</div>";
           SendEmail(customer.Email,body);
        }
        /// <summary>
        /// 客户注册
        /// </summary>
        /// <returns></returns>
        public async Task Register(CustomerEditDto input)
        {
            if (!input.Password.Equals(input.RepeatPassword)) throw new UserFriendlyException("两次密码不一致");
            var count = await _customerRepository.CountAsync(c => c.Account.Equals(input.Account)||c.Email.Equals(input.Email));
            if (count>0) throw new UserFriendlyException("该账户或邮箱已被注册,请重试");
            var model = input.MapTo<Customer>();
            await _customerRepository.InsertAsync(model);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        private void SendEmail(string to, string body,bool isHtml=true)
        {
            SmtpEmailSender emailSender = new SmtpEmailSender(_smtpEmailSenderConfiguration);
            emailSender.Send(to, "不知道干嘛用的", "密码重置链接", body, isHtml);
        }
    }
}
