using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Adsences.Dtos;

namespace YT.Adsences
{
	/// <summary>
    /// 公告管理服务接口
    /// </summary>
    public interface IAdsenceAppService : IApplicationService
    {
        #region 公告管理管理

        /// <summary>
        /// 根据查询条件获取公告管理分页列表
        /// </summary>
        Task<PagedResultDto<AdsenceListDto>> GetPagedAdsencesAsync(GetAdsenceInput input);

        /// <summary>
        /// 通过Id获取公告管理信息进行编辑或修改 
        /// </summary>
        Task<GetAdsenceForEditOutput> GetAdsenceForEditAsync(NullableIdDto<int> input);

		  /// <summary>
        /// 通过指定id获取公告管理ListDto信息
        /// </summary>
		Task<AdsenceListDto> GetAdsenceByIdAsync(EntityDto<int> input);

        /// <summary>
        /// 发布公告
        /// </summary>
        Task PublicAdsenceAsync(EntityDto<int> input);


        /// <summary>
        /// 新增或更改公告管理
        /// </summary>
        Task CreateOrUpdateAdsenceAsync(CreateOrUpdateAdsenceInput input);

        /// <summary>
        /// 删除公告管理
        /// </summary>
        Task DeleteAdsenceAsync(EntityDto<int> input);

        /// <summary>
        /// 批量删除公告管理
        /// </summary>
        Task BatchDeleteAdsenceAsync(List<int> input);

        #endregion




    }
}
