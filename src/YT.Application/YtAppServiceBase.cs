using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity;
using YT.Authorization.Users;
using YT.Managers.MultiTenancy;
using YT.Managers.Roles;
using YT.Managers.Users;
using YT.MultiTenancy;

namespace YT
{
    /// <summary>
    /// All application services in this application is derived from this class.
    /// We can add common application service methods here.
    /// </summary>
    public abstract class YtAppServiceBase : ApplicationService
    { /// <summary>
      /// 
      /// </summary>
        public TenantManager TenantManager { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserManager UserManager { get; set; }
        /// <summary>
        /// 角色管理
        /// </summary>
        public RoleManager RoleManager { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected YtAppServiceBase()
        {
            LocalizationSourceName = YtConsts.LocalizationSourceName;
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual User GetCurrentUser()
        {
            var user = UserManager.FindById(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual Tenant GetCurrentTenant()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetById(AbpSession.GetTenantId());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        /// <summary>
        /// 生成levelcode
        /// </summary>
        protected virtual string GenderLevelCode()
        {
          return Guid.NewGuid().ToString("D").Split('-')[0];
        }
    }
}