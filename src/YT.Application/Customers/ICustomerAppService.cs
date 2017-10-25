                           
 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Customers.Dtos;
using YT.Dto;
namespace YT.Customers
{
	/// <summary>
    /// 客户表服务接口
    /// </summary>
    public interface ICustomerAppService : IApplicationService
    {
        #region 客户表管理

        /// <summary>
        /// 根据查询条件获取客户表分页列表
        /// </summary>
        Task<PagedResultDto<CustomerListDto>> GetPagedCustomersAsync(GetCustomerInput input);

        /// <summary>
        /// 通过Id获取客户表信息进行编辑或修改 
        /// </summary>
        Task<GetCustomerForEditOutput> GetCustomerForEditAsync(NullableIdDto<int> input);

		  /// <summary>
        /// 通过指定id获取客户表ListDto信息
        /// </summary>
		Task<CustomerListDto> GetCustomerByIdAsync(EntityDto<int> input);



        /// <summary>
        /// 新增或更改客户表
        /// </summary>
        Task CreateOrUpdateCustomerAsync(CreateOrUpdateCustomerInput input);

        /// <summary>
        /// 用户充值
        /// </summary>
        Task CustomerCharge(CustomerChargeInput input);
        /// <summary>
        /// 删除客户表
        /// </summary>
        Task DeleteCustomerAsync(EntityDto<int> input);

        /// <summary>
        /// 批量删除客户表
        /// </summary>
        Task BatchDeleteCustomerAsync(List<int> input);

        #endregion

#region Excel导出功能



         /// <summary>
        /// 获取客户表信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetCustomerToExcel();

#endregion





    }
}
