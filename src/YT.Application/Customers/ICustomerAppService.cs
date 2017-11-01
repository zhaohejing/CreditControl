using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Customers.Dtos;
using YT.Dto;

namespace YT.Customers
{
    /// <summary>
    /// 客户信息服务接口
    /// </summary>
    public interface ICustomerAppService : IApplicationService
    {
        #region 客户信息管理

        /// <summary>
        /// 根据查询条件获取客户信息分页列表
        /// </summary>
        Task<PagedResultDto<CustomerListDto>> GetPagedCustomersAsync(GetCustomerInput input);

        /// <summary>
        /// 通过Id获取客户信息信息进行编辑或修改 
        /// </summary>
        Task<GetCustomerForEditOutput> GetCustomerForEditAsync(NullableIdDto<int> input);

        /// <summary>
        /// 通过指定id获取客户信息ListDto信息
        /// </summary>
        Task<CustomerListDto> GetCustomerByIdAsync(EntityDto<int> input);



        /// <summary>
        /// 新增或更改客户信息
        /// </summary>
        Task CreateOrUpdateCustomerAsync(CreateOrUpdateCustomerInput input);

        /// <summary>
        /// 用户审核
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
          Task AuditCustomer(AuditInput input);

      



        /// <summary>
        /// 删除客户信息
        /// </summary>
        Task DeleteCustomerAsync(EntityDto<int> input);

        /// <summary>
        /// 批量删除客户信息
        /// </summary>
        Task BatchDeleteCustomerAsync(List<int> input);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ResetCustomer(ResetInput input);
        #endregion

        #region Excel导出功能



        /// <summary>
        /// 获取客户信息信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetCustomerToExcel();

        #endregion





    }
}
