

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Dto;
using YT.SpecialCards.Dtos;

namespace YT.SpecialCards
{
    /// <summary>
    /// 唯鲜卡服务接口
    /// </summary>
    public interface ISpecialCardAppService : IApplicationService
    {
        #region 唯鲜卡管理

        /// <summary>
        /// 根据查询条件获取唯鲜卡分页列表
        /// </summary>
        Task<PagedResultDto<SpecialCardListDto>> GetPagedSpecialCardsAsync(GetSpecialCardInput input);

        /// <summary>
        /// 通过Id获取唯鲜卡信息进行编辑或修改 
        /// </summary>
        Task<GetSpecialCardForEditOutput> GetSpecialCardForEditAsync(NullableIdDto<int> input);

        /// <summary>
        /// 通过指定id获取唯鲜卡ListDto信息
        /// </summary>
        Task<SpecialCardListDto> GetSpecialCardByIdAsync(EntityDto<int> input);

        /// <summary>
        /// 新增或更改唯鲜卡
        /// </summary>
        Task CreateOrUpdateSpecialCardAsync(CreateOrUpdateSpecialCardInput input);
        /// <summary>
        /// 删除唯鲜卡
        /// </summary>
        Task DeleteSpecialCardAsync(EntityDto<int> input);

        /// <summary>
        /// 批量删除唯鲜卡
        /// </summary>
        Task BatchDeleteSpecialCardAsync(List<int> input);

        #endregion

        #region Excel导出功能



        /// <summary>
        /// 获取唯鲜卡信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetSpecialCardToExcel();

        #endregion





    }
}
