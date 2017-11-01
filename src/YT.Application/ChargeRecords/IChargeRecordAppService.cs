using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.ChargeRecords.Dtos;

namespace YT.ChargeRecords
{
	/// <summary>
    /// 充值记录服务接口
    /// </summary>
    public interface IChargeRecordAppService : IApplicationService
    {
        #region 充值记录管理

        /// <summary>
        /// 根据查询条件获取充值记录分页列表
        /// </summary>
        Task<PagedResultDto<ChargeRecordListDto>> GetPagedChargeRecordsAsync(GetChargeRecordInput input);

        /// <summary>
        /// 根据查询条件获取充值记录分页列表
        /// </summary>
        Task<PagedResultDto<ApplyChargeListDto>> GetPagedApplyChargesAsync(GetChargeRecordInput input);

        #endregion




    }
}
