using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.CustomerPreferencePrices.Dtos;

namespace YT.CustomerPreferencePrices
{
	/// <summary>
    /// 用户折扣价格设置服务接口
    /// </summary>
    public interface ICustomerPreferencePriceAppService : IApplicationService
    {
        #region 用户折扣价格设置管理

        /// <summary>
        /// 根据查询条件获取用户折扣价格设置分页列表
        /// </summary>
          Task<List<CustomerPreferencePriceListDto>> GetProductPricesAsync(EntityDto<int> input);

        /// <summary>
        /// 获取客户所有商品定价
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CustomerPreferencePriceListDto>> GetCustomerPricesAsync(EntityDto<int> input);




        /// <summary>
        /// 修改价格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ModifyPriceAsync(ModifyCustomerPreferencePriceInput input);

        #endregion




    }
}
