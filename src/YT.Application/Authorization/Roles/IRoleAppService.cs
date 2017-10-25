using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Authorization.Roles.Dto;

namespace YT.Authorization.Roles
{
    /// <summary>
    /// Application service that is used by 'role management' page.
    /// </summary>
    public interface IRoleAppService : IApplicationService
    {/// <summary>
    /// 分页获取角色信息
    /// </summary>
    /// <returns></returns>
        Task<ListResultDto<RoleListDto>> GetRoles();
        /// <summary>
        /// 获取角色+权限信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdDto input);
        /// <summary>
        /// 创建或编辑角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateRole(CreateOrUpdateRoleInput input);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteRole(EntityDto input);

        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        Task<PagedResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input);
    }
}