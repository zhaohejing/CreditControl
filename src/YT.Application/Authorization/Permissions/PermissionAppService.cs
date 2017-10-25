using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using YT.Authorization.Permissions.Dto;
using YT.Caching;

namespace YT.Authorization.Permissions
{
    /// <summary>
    /// Ȩ��service
    /// </summary>
    [AbpAuthorize]
    public class PermissionAppService : YtAppServiceBase, IPermissionAppService
    {
        private readonly ICachingAppService _cachingAppService;
   /// <summary>
   /// ctor
   /// </summary>
   /// <param name="cachingAppService"></param>
        public PermissionAppService(
            ICachingAppService cachingAppService)
        {
            _cachingAppService = cachingAppService;
         
        }

        /// <summary>
        /// ��ȡ���е�Ȩ��
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<FlatPermissionWithLevelDto>> GetAllPermissions()
        {
            return await _cachingAppService.GetPermissionCache();
        }

    }
}