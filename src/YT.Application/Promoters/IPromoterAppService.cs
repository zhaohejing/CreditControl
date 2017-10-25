                           
 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Dto;
using YT.Promoters.Dtos;

namespace YT.Promoters
{
	/// <summary>
    /// 推广员管理服务接口
    /// </summary>
    public interface IPromoterAppService : IApplicationService
    {
        #region 推广员管理管理

        /// <summary>
        /// 根据查询条件获取推广员管理分页列表
        /// </summary>
        Task<PagedResultDto<PromoterListDto>> GetPagedPromotersAsync(GetPromoterInput input);

        /// <summary>
        /// 通过Id获取推广员管理信息进行编辑或修改 
        /// </summary>
        Task<GetPromoterForEditOutput> GetPromoterForEditAsync(NullableIdDto<int> input);

		  /// <summary>
        /// 通过指定id获取推广员管理ListDto信息
        /// </summary>
		Task<PromoterListDto> GetPromoterByIdAsync(EntityDto<int> input);



        /// <summary>
        /// 新增或更改推广员管理
        /// </summary>
        Task CreateOrUpdatePromoterAsync(CreateOrUpdatePromoterInput input);

        /// <summary>
        /// 删除推广员管理
        /// </summary>
        Task DeletePromoterAsync(EntityDto<int> input);

        /// <summary>
        /// 批量删除推广员管理
        /// </summary>
        Task BatchDeletePromoterAsync(List<int> input);

        #endregion

#region Excel导出功能



         /// <summary>
        /// 获取推广员管理信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetPromoterToExcel();

#endregion





    }
}
