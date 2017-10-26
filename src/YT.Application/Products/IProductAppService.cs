using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Dto;
using YT.Products.Dtos;

namespace YT.Products
{
    /// <summary>
    /// 产品服务接口
    /// </summary>
    public interface IProductAppService : IApplicationService
    {
        #region 产品管理

        /// <summary>
        /// 根据查询条件获取产品分页列表
        /// </summary>
        Task<PagedResultDto<ProductListDto>> GetPagedProductsAsync(GetProductInput input);

        /// <summary>
        /// 通过Id获取产品信息进行编辑或修改 
        /// </summary>
        Task<GetProductForEditOutput> GetProductForEditAsync(NullableIdDto<int> input);

        /// <summary>
        /// 通过指定id获取产品ListDto信息
        /// </summary>
        Task<ProductListDto> GetProductByIdAsync(EntityDto<int> input);



        /// <summary>
        /// 新增或更改产品
        /// </summary>
        Task CreateOrUpdateProductAsync(CreateOrUpdateProductInput input);





        /// <summary>
        /// 新增产品
        /// </summary>
        Task<ProductEditDto> CreateProductAsync(ProductEditDto input);

        /// <summary>
        /// 更新产品
        /// </summary>
        Task UpdateProductAsync(ProductEditDto input);

        /// <summary>
        /// 删除产品
        /// </summary>
        Task DeleteProductAsync(EntityDto<int> input);

        /// <summary>
        /// 批量删除产品
        /// </summary>
        Task BatchDeleteProductAsync(List<int> input);

        #endregion

        #region Excel导出功能



        /// <summary>
        /// 获取产品信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetProductToExcel();

        #endregion





    }
}
