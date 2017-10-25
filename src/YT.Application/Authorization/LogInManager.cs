using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using YT.Authorization.Roles;
using YT.Authorization.Users;
using YT.Managers.MultiTenancy;
using YT.Managers.Roles;
using YT.Managers.Users;
using YT.MultiTenancy;

namespace YT.Authorization
{
    /// <summary>
    /// µÇÂ½¹ÜÀí
    /// </summary>
    public class LogInManager : AbpLogInManager<Tenant, Role, User>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="multiTenancyConfig"></param>
        /// <param name="tenantRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="settingManager"></param>
        /// <param name="userLoginAttemptRepository"></param>
        /// <param name="userManagementConfig"></param>
        /// <param name="iocResolver"></param>
        /// <param name="roleManager"></param>
        public LogInManager(
            UserManager userManager, 
            IMultiTenancyConfig multiTenancyConfig, 
            IRepository<Tenant> tenantRepository, 
            IUnitOfWorkManager unitOfWorkManager, 
            ISettingManager settingManager, 
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository, 
            IUserManagementConfig userManagementConfig, 
            IIocResolver iocResolver, 
            RoleManager roleManager)
            : base(
                  userManager, 
                  multiTenancyConfig, 
                  tenantRepository, 
                  unitOfWorkManager, 
                  settingManager, 
                  userLoginAttemptRepository, 
                  userManagementConfig, 
                  iocResolver, 
                  roleManager)
        {

        }
    }
}