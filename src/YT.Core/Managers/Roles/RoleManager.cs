using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using YT.Authorizations;
using YT.Managers.Roles;
using YT.Managers.Users;

namespace YT.Managers.Roles
{
    /// <summary>
    /// Role manager.
    /// Used to implement domain logic for roles.
    /// </summary>
    public class RoleManager : AbpRoleManager<Role, User>
    {
        private readonly IRepository<RolePermissionSetting, long> _rolePermissionRepository;
        private readonly IRepository<YtPermission> _permissionRepository;
        private readonly ICacheManager _cacheManager;
     /// <summary>
     /// ctor
     /// </summary>
     /// <param name="store"></param>
     /// <param name="permissionManager"></param>
     /// <param name="roleManagementConfig"></param>
     /// <param name="cacheManager"></param>
     /// <param name="unitOfWorkManager"></param>
     /// <param name="rolePermissionRepository"></param>
     /// <param name="permissionRepository"></param>
     /// <param name="cacheManager1"></param>
        public RoleManager(
            RoleStore store,
            IPermissionManager permissionManager,
            IRoleManagementConfig roleManagementConfig,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<RolePermissionSetting, long> rolePermissionRepository,
            IRepository<YtPermission> permissionRepository,
            ICacheManager cacheManager1)
            : base(
                store,
                permissionManager,
                roleManagementConfig,
                cacheManager,
                unitOfWorkManager)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
            _cacheManager = cacheManager1;
        }
        /// <summary>
        /// Gets granted permissions for a role.
        /// </summary>
        /// <param name="role">Role</param>
        /// <returns>List of granted permissions</returns>
        public virtual async Task<IReadOnlyList<YtPermission>> GetHadGrantPermissionsAsync(Role role)
        {
            var permissionList = new List<YtPermission>();
            var permissions = await _permissionRepository.GetAllListAsync();
            foreach (var permission in permissions)
            {
                if (await IsGrantedAsync(role.Id, permission.Name))
                {
                    permissionList.Add(permission);
                }
            }

            return permissionList;
        }
        public async Task SetRoleGrantedPermissionsAsync(Role role, List<YtPermission> grantedPermissionNames)
        {
            var oldPermissions = await GetHadGrantPermissionsAsync(role);
            var newPermissions = grantedPermissionNames.ToArray();

            foreach (var permission in oldPermissions.Where(p => !newPermissions.Contains(p, PermissionEqualityComparer.Instance)))
            {
                await ProhibitPermissionAsync(role, permission);
            }

            foreach (var permission in newPermissions.Where(p => !oldPermissions.Contains(p, PermissionEqualityComparer.Instance)))
            {
                await GrantPermissionAsync(role, permission);
            }
        }
        /// <summary>
        /// Prohibits a permission for a role.
        /// </summary>
        /// <param name="role">Role</param>
        /// <param name="permission">Permission</param>
        public async Task ProhibitPermissionAsync(Role role, YtPermission permission)
        {
            if (!await IsGrantedAsync(role.Id, permission.Name))
            {
                return;
            }
             await RemovePermissionAsync(role, new PermissionGrantInfo(permission.Name, true));

        }
        /// <inheritdoc/>
        public virtual async Task RemovePermissionAsync(Role role, PermissionGrantInfo permissionGrant)
        {
            await _rolePermissionRepository.DeleteAsync(
                permissionSetting => permissionSetting.RoleId == role.Id &&
                                     permissionSetting.Name == permissionGrant.Name &&
                                     permissionSetting.IsGranted == permissionGrant.IsGranted
                );
        }
        /// <summary>
        /// Grants a permission for a role.
        /// </summary>
        /// <param name="role">Role</param>
        /// <param name="permission">Permission</param>
        public async Task GrantPermissionAsync(Role role, YtPermission permission)
        {
            if (await IsGrantedAsync(role.Id, permission.Name))
            {
                return;
            }
            //await _rolePermissionRepository.InsertAsync(new RolePermissionSetting()
            //{
            //    IsGranted = true,
            //    Name = permission.Name,
            //    RoleId = role.Id,
            //    TenantId = AbpSession.TenantId
            //});
            await AddPermissionAsync(role, new PermissionGrantInfo(permission.Name, true));
        }
        /// <inheritdoc/>
        public virtual async Task AddPermissionAsync(Role role, PermissionGrantInfo permissionGrant)
        {
            if (await IsGrantedAsync(role.Id, permissionGrant.Name))
            {
                return;
            }

            await _rolePermissionRepository.InsertAsync(
                new RolePermissionSetting
                {
                    TenantId = role.TenantId,
                    RoleId = role.Id,
                    Name = permissionGrant.Name,
                    IsGranted = permissionGrant.IsGranted
                });
        }
        /// <summary>
        /// Checks if a role is granted for a permission.
        /// </summary>
        /// <param name="roleId">role id</param>
        /// <param name="name">The permission</param>
        /// <returns>True, if the role has the permission</returns>
        public override async Task<bool> IsGrantedAsync(int roleId, string name)
        {
            //Get cached role permissions
            var cacheItem = await GetRolePermissionCacheItemAsync(roleId);

            //Check the permission
            return cacheItem.GrantedPermissions.Contains(name);
        }
        private async Task<RolePermissionCacheItem> GetRolePermissionCacheItemAsync(int roleId)
        {
            var cacheKey = roleId + "@" + (AbpSession.TenantId ?? 0);
            return await _cacheManager.GetRolePermissionCache().GetAsync(cacheKey, async () =>
            {
                var newCacheItem = new RolePermissionCacheItem(roleId);

                foreach (var permissionInfo in await _rolePermissionRepository.GetAllListAsync(c=>c.RoleId==roleId))
                {
                    if (permissionInfo.IsGranted)
                    {
                        newCacheItem.GrantedPermissions.Add(permissionInfo.Name);
                    }
                }

                return newCacheItem;
            });
        }
     
    }
    /// <summary>
    /// Equality comparer for <see cref="Permission"/> objects.
    /// </summary>
    internal class PermissionEqualityComparer : IEqualityComparer<YtPermission>
    {
        public static PermissionEqualityComparer Instance { get; } = new PermissionEqualityComparer();

        public bool Equals(YtPermission x, YtPermission y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }

            return Equals(x.Name, y.Name);
        }

        public int GetHashCode(YtPermission permission)
        {
            return permission.Name.GetHashCode();
        }
    }
}