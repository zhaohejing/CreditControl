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
    /// ��ҳ��ȡ��ɫ��Ϣ
    /// </summary>
    /// <returns></returns>
        Task<ListResultDto<RoleListDto>> GetRoles();
        /// <summary>
        /// ��ȡ��ɫ+Ȩ����Ϣ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdDto input);
        /// <summary>
        /// ������༭��ɫ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateRole(CreateOrUpdateRoleInput input);
        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteRole(EntityDto input);

        /// <summary>
        /// ��ҳ��ȡ��ɫ�б�
        /// </summary>
        Task<PagedResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input);
    }
}