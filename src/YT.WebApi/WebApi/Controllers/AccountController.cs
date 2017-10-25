using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Abp;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using YT.Authorization;
using YT.Configuration;
using YT.Managers.MultiTenancy;
using YT.Managers.Roles;
using YT.Managers.Users;
using YT.MultiTenancy;
using YT.WebApi.Models;

namespace YT.WebApi.Controllers
{
    /// <summary>
    /// token 服务器
    /// </summary>
    public class AccountController : YtApiControllerBase
    {
        /// <summary>
        /// 认证标准
        /// </summary>
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        private readonly LogInManager _logInManager;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TenantManager _tenantManager;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        #region ctors
        /// <summary>
        /// static ctor
        /// </summary>
        static AccountController()
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
        }
       /// <summary>
       /// ctor
       /// </summary>
       /// <param name="abpLoginResultTypeHelper"></param>
       /// <param name="logInManager"></param>
       /// <param name="tenantManager"></param>
       /// <param name="roleManager"></param>
       /// <param name="userManager"></param>
       /// <param name="unitOfWorkManager"></param>
        public AccountController(
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            LogInManager logInManager,
            TenantManager tenantManager,
            RoleManager roleManager,
            UserManager userManager,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _logInManager = logInManager;
            _tenantManager = tenantManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
        }
        #endregion
        #region 登陆/认证
        /// <summary>
        /// 获取认证
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpAllowAnonymous]
        public async Task<AjaxResponse> Authenticate(LoginModel loginModel)
        {
            var loginResult = await GetLoginResultAsync(
                loginModel.UsernameOrEmailAddress,
                loginModel.Password,
               string.Empty
                );

            var ticket = new AuthenticationTicket(loginResult.Identity, new AuthenticationProperties());
            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            //有效期1天
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromDays(1));
            return new AjaxResponse(OAuthBearerOptions.AccessTokenFormat.Protect(ticket));
        }
        /// <summary>
        /// 获取登录结果
        /// </summary>
        /// <param name="usernameOrEmailAddress"></param>
        /// <param name="password"></param>
        /// <param name="tenancyName"></param>
        /// <returns></returns>
        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        #endregion
        #region 注册
        [HttpPost]
        [UnitOfWork]
        [AbpAllowAnonymous]
        public virtual async Task<AjaxResponse> Register(RegisterViewModel model)
        {
            try
            {
              //  CheckSelfRegistrationIsEnabled();
                CurrentUnitOfWork.SetTenantId(null);
                var tenant = await GetActiveTenantAsync(Tenant.DefaultTenantName);
                CurrentUnitOfWork.SetTenantId(tenant.Id);
                //Getting tenant-specific settings
               // var isNewRegisteredUserActiveByDefault = await SettingManager.GetSettingValueForApplicationAsync<bool>(YtSettings.General.UserDefaultActive);
                var user = new User
                {
                    TenantId = tenant.Id,
                    Name = model.Name,
                    IsActive = true
                };
                if (model.UserName.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
                {
                    throw new AbpException("用户名或密码不可为空");
                }
                user.UserName = model.UserName;
                user.Password = new PasswordHasher().HashPassword(model.Password);
                user.Roles = new List<UserRole>();
                var roles = _roleManager.Roles.Where(r => r.IsDefault).ToList();
                foreach (var defaultRole in roles)
                {
                    user.Roles.Add(new UserRole(tenant.Id, user.Id, defaultRole.Id));
                }

             //  CheckErrors(await _userManager.CreateAsync(user));
               await _userManager.CreateAsync(user); 
                await _unitOfWorkManager.Current.SaveChangesAsync();
                if (!user.IsActive)
                    return new AjaxResponse("用户注册成功,处于禁用状态");
                AbpLoginResult<Tenant, User>
                    loginResult = await GetLoginResultAsync(user.UserName, model.Password, tenant.TenancyName);
                if (loginResult.Result == AbpLoginResultType.Success)
                {
                    var ticket = new AuthenticationTicket(loginResult.Identity, new AuthenticationProperties());
                    var currentUtc = new SystemClock().UtcNow;
                    ticket.Properties.IssuedUtc = currentUtc;
                    //有效期1天
                    ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromDays(1));
                    return new AjaxResponse(OAuthBearerOptions.AccessTokenFormat.Protect(ticket));
                }
                Logger.Warn("登陆失败,原因: " + loginResult.Result);
                return new AjaxResponse("用户注册成功,登陆失败,原因"+loginResult.Result);
            }
            catch (UserFriendlyException ex)
            {
                return new AjaxResponse(ex.Message);
            }
        }
        private async Task<Tenant> GetActiveTenantAsync(string tenancyName)
        {
            var tenant = await _tenantManager.FindByTenancyNameAsync(tenancyName);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIsNotActive", tenancyName));
            }

            return tenant;
        }
        #endregion

        #region 忘记密码/修改密码
        [HttpPost]
        [UnitOfWork]
        public virtual async Task<AjaxResponse> ResetPassword(ResetPasswordFormViewModel model)
        {
           // var tenantId = model.TenantId.IsNullOrEmpty() ? (int?)null : SimpleStringCipher.Instance.Decrypt(model.TenantId).To<int>();
            var tenantId = 1;
            var userId = Convert.ToInt64(model.UserId);
            _unitOfWorkManager.Current.SetTenantId(tenantId);
            var user = await _userManager.GetUserByIdAsync(userId);
            if (user == null )
            {
                throw new AbpException("当前用户信息不存在");
            }

            user.Password = new PasswordHasher().HashPassword(model.NewPassword);
          //  user.PasswordResetCode = null;
          //  user.IsEmailConfirmed = true;
            //  user.ShouldChangePasswordOnNextLogin = false;

            await _userManager.UpdateAsync(user);
            if (user.IsActive)
            {
                 var loginResult = await GetLoginResultAsync(
                     user.UserName,
                     model.NewPassword,
                     string.Empty
                 );

                var ticket = new AuthenticationTicket(loginResult.Identity, new AuthenticationProperties());
                var currentUtc = new SystemClock().UtcNow;
                ticket.Properties.IssuedUtc = currentUtc;
                //有效期1天
                ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromDays(1));
                return new AjaxResponse(OAuthBearerOptions.AccessTokenFormat.Protect(ticket));
            }

            return null;
        }
        #endregion


    }
}
