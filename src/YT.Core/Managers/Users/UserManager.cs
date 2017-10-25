using System;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Threading;
using Microsoft.AspNet.Identity;
using YT.Managers.Roles;

namespace YT.Managers.Users
{
    /// <summary>
    /// User manager.
    /// Used to implement domain logic for users.
    /// Extends <see cref="AbpUserManager{TRole,TUser}"/>.
    /// </summary>
    public class UserManager : AbpUserManager<Role, User>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UserManager(
            UserStore userStore, 
            RoleManager roleManager, 
            IPermissionManager permissionManager, 
            IUnitOfWorkManager unitOfWorkManager, 
            ICacheManager cacheManager, 
            IRepository<OrganizationUnit, long> organizationUnitRepository, 
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository, 
            IOrganizationUnitSettings organizationUnitSettings,
            IdentityEmailMessageService emailService,
            ILocalizationManager localizationManager,
            ISettingManager settingManager,
            IUserTokenProviderAccessor userTokenProviderAccessor) 
            : base(
                  userStore, 
                  roleManager, 
                  permissionManager, 
                  unitOfWorkManager, 
                  cacheManager, 
                  organizationUnitRepository, 
                  userOrganizationUnitRepository, 
                  organizationUnitSettings,
                  localizationManager,
                  emailService,
                  settingManager,
                  userTokenProviderAccessor)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<User> GetUserOrNullAsync(UserIdentifier userIdentifier)
        {
            using (_unitOfWorkManager.Begin())
            {
                using (_unitOfWorkManager.Current.SetTenantId(userIdentifier.TenantId))
                {
                    return await FindByIdAsync(userIdentifier.UserId);
                }
            }
        }
        /// <summary>
        /// ÷ÿ–¥∑Ω∑®
        /// </summary>
        /// <param name="expectedUserId"></param>
        /// <param name="userName"></param>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public override async Task<IdentityResult> CheckDuplicateUsernameOrEmailAddressAsync(long? expectedUserId, string userName, string emailAddress)
        {
            var user = (await FindByNameAsync(userName));
            if (user != null && user.Id != expectedUserId)
            {
                return AbpIdentityResult.Failed(string.Format(L("Identity.DuplicateName"), userName));
            }
            return IdentityResult.Success;
        }
    
        public User GetUserOrNull(UserIdentifier userIdentifier)
        {
            return AsyncHelper.RunSync(() => GetUserOrNullAsync(userIdentifier));
        }
        private string L(string name)
        {
            return LocalizationManager.GetString(YtConsts.LocalizationSourceName, name);
        }
        public override async Task<IdentityResult> CreateAsync(User user)
        {
            var result = await CheckDuplicateUsernameOrEmailAddressAsync(user.Id, user.UserName, user.EmailAddress);
            if (!result.Succeeded)
            {
                return result;
            }

            user.EmailAddress = string.Empty;

            var tenantId = GetCurrentTenantId();
            if (tenantId.HasValue && !user.TenantId.HasValue)
            {
                user.TenantId = tenantId.Value;
            }

            try
            {
                return await base.CreateAsync(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private int? GetCurrentTenantId()
        {
            return _unitOfWorkManager.Current != null ? _unitOfWorkManager.Current.GetTenantId() : AbpSession.TenantId;
        }
        public async Task<User> GetUserAsync(UserIdentifier userIdentifier)
        {
            var user = await GetUserOrNullAsync(userIdentifier);
            if (user == null)
            {
                throw new ApplicationException("There is no user: " + userIdentifier);
            }

            return user;
        }

        public User GetUser(UserIdentifier userIdentifier)
        {
            return AsyncHelper.RunSync(() => GetUserAsync(userIdentifier));
        }
    }
}