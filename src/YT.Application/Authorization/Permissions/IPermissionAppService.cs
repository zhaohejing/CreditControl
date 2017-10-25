using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Authorization.Permissions.Dto;

namespace YT.Authorization.Permissions
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public interface IPermissionAppService : IApplicationService
    {
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<FlatPermissionWithLevelDto>> GetAllPermissions();
    }
}
