                           
 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Cards.Dtos;
using YT.Dto;
namespace YT.Cards
{
	/// <summary>
    /// 充值卡服务接口
    /// </summary>
    public interface ICardAppService : IApplicationService
    {
        #region 充值卡管理

        /// <summary>
        /// 根据查询条件获取充值卡分页列表
        /// </summary>
        Task<PagedResultDto<CardListDto>> GetPagedCardsAsync(GetCardInput input);

        /// <summary>
        /// 通过Id获取充值卡信息进行编辑或修改 
        /// </summary>
        Task<GetCardForEditOutput> GetCardForEditAsync(NullableIdDto<int> input);

		  /// <summary>
        /// 通过指定id获取充值卡ListDto信息
        /// </summary>
		Task<CardListDto> GetCardByIdAsync(EntityDto<int> input);



        /// <summary>
        /// 新增或更改充值卡
        /// </summary>
        Task CreateOrUpdateCardAsync(CreateOrUpdateCardInput input);

        /// <summary>
        /// 删除充值卡
        /// </summary>
        Task DeleteCardAsync(EntityDto<int> input);

        /// <summary>
        /// 批量删除充值卡
        /// </summary>
        Task BatchDeleteCardAsync(List<int> input);

        #endregion

#region Excel导出功能



         /// <summary>
        /// 获取充值卡信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetCardToExcel();

#endregion





    }
}
