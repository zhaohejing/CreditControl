using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Extensions;
using Abp.Runtime.Caching;
using YT.Authorization.Permissions;
using YT.Authorization.Permissions.Dto;
using YT.Authorization.Roles.Dto;
using YT.Authorizations;
using YT.Authorizations.PermissionDefault;
using YT.Caching;
using YT.Managers.Roles;

namespace YT.Authorization.Roles
{
    /// <summary>
    /// 角色管理 application
    /// </summary>
    // [AbpAuthorize(StaticPermissionsName.Page_Role)]
    [AbpAuthorize]
    public class RoleAppService : YtAppServiceBase, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IRepository<YtPermission> _permissionRepository;
        private readonly ICachingAppService _cacheManager;
        /// <summary>
        /// 
        /// </summary>
        public RoleAppService(RoleManager roleManager, IRepository<YtPermission> permissionRepository, ICachingAppService cacheManager)
        {
            _roleManager = roleManager;
            _permissionRepository = permissionRepository;
            _cacheManager = cacheManager;
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task<ListResultDto<RoleListDto>> GetRoles()
        {
            var roles = await _roleManager
                .Roles
                .ToListAsync();
            return new ListResultDto<RoleListDto>(roles.MapTo<List<RoleListDto>>());
        }


        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        public async Task<PagedResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input)
        {
            var query = RoleManager.Roles
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), w => w.DisplayName.Contains(input.Filter))
                .WhereIf(
                    !input.Permission.IsNullOrWhiteSpace(),
                    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
                );
            var roleCount = await query.CountAsync();
            var roles = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<RoleListDto>(roleCount, roles.MapTo<List<RoleListDto>>());
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdDto input)
        {
            // var permissions =await _permissionRepository.GetAllListAsync();
            var grantedPermissions = new List<string>();
            RoleEditDto roleEditDto;
            if (input.Id.HasValue) //Editing existing role?
            {
                var role = await _roleManager.GetRoleByIdAsync(input.Id.Value);
                grantedPermissions = role.Permissions.Where(c => c.IsGranted).Select(c => c.Name).ToList();
                roleEditDto = role.MapTo<RoleEditDto>();
            }
            else
            {
                roleEditDto = new RoleEditDto();
            }
            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                //  Permissions = permissions.MapTo<List<FlatPermissionWithLevelDto>>().OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions
            };
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task CreateOrUpdateRole(CreateOrUpdateRoleInput input)
        {
            if (input.Role.Id.HasValue)
            {
                await UpdateRoleAsync(input);
            }
            else
            {
                await CreateRoleAsync(input);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task DeleteRole(EntityDto input)
        {
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            CheckErrors(await _roleManager.DeleteAsync(role));
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual async Task UpdateRoleAsync(CreateOrUpdateRoleInput input)
        {
            Debug.Assert(input.Role.Id != null, "角色id不可为null");

            var role = await _roleManager.GetRoleByIdAsync(input.Role.Id.Value);
            role.DisplayName = input.Role.DisplayName;
            role.IsDefault = input.Role.IsDefault;
            role.IsActive = input.Role.IsActive;
            role.Description = input.Role.Description;
            await UpdateGrantedPermissionsAsync(role, input.GrantedPermissionNames);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        protected virtual async Task CreateRoleAsync(CreateOrUpdateRoleInput input)
        {
            var role = new Role(AbpSession.TenantId, input.Role.DisplayName)
            {
                IsDefault = input.Role.IsDefault,
                IsActive = input.Role.IsActive,
                Description = input.Role.Description
            };
            CheckErrors(await _roleManager.CreateAsync(role));
            await CurrentUnitOfWork.SaveChangesAsync(); //It's done to get Id of the role.
            await UpdateGrantedPermissionsAsync(role, input.GrantedPermissionNames);
        }

        private async Task UpdateGrantedPermissionsAsync(Role role, List<string> grantedPermissionNames)
        {
            var permissions = await
                _permissionRepository.GetAllListAsync(c => grantedPermissionNames.Any(e => e.Equals(c.Name)));
            await _roleManager.SetRoleGrantedPermissionsAsync(role, permissions);
            await _cacheManager.ClearCache(new EntityDto<string>(CacheName.PermissionCache));
        }

    }
}
