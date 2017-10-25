using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Authorization.Permissions.Dto;
using YT.Authorizations;
using YT.Caching.Dto;
using YT.Navigations;
using YT.Navigations.Dtos;

namespace YT.Caching
{/// <summary>
/// �������
/// </summary>
    public interface ICachingAppService : IApplicationService
    {/// <summary>
    /// ��ȡ���л�����
    /// </summary>
    /// <returns></returns>
        ListResultDto<CacheDto> GetAllCaches();
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ClearCache(EntityDto<string> input);
        /// <summary>
        /// ȫ�����
        /// </summary>
        /// <returns></returns>
        Task ClearAllCaches();

        /// <summary>
        /// ��ȡ�˵�����
        /// </summary>
        /// <returns></returns>
        Task<List<MenuEditDto>> GetMenuCache();

        /// <summary>
        /// ��ȡȨ�޻���
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<FlatPermissionWithLevelDto>> GetPermissionCache();
    }
}
