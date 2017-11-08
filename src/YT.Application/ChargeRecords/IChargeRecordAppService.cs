using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.ChargeRecords.Dtos;
using YT.Customers.Dtos;
using YT.Dashboards.Dtos;

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

        /// <summary>
        /// 删除充值申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
          Task DeleteApplyChargeAsync(EntityDto<int> input);

        /// <summary>
        /// 用户充值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
          Task ChargeCustomer(ChargeInput input);
        /// <summary>
        /// 用户完成充值申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
          Task ChargeApplyCustomer(ChargeInput input);

        /// <summary>
        /// 获取用户消费记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CustomerCostListDto>> GetCustomerCosts(GetCustomerCostsPagesInput input);

    }
}
