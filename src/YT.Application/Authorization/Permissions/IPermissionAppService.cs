using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Authorization.Permissions.Dto;

namespace YT.Authorization.Permissions
{
    /// <summary>
    /// Ȩ�޹���
    /// </summary>
    public interface IPermissionAppService : IApplicationService
    {
        /// <summary>
        /// ��ȡ����Ȩ��
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<FlatPermissionWithLevelDto>> GetAllPermissions();
    }
}
