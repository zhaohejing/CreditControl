using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Categories.Dtos;

namespace YT.Categories
{
	/// <summary>
    /// 分类服务接口
    /// </summary>
    public interface ICategoryAppService : IApplicationService
    {
        #region 分类管理

        /// <summary>
        /// 根据查询条件获取分类分页列表
        /// </summary>
        Task<PagedResultDto<CategoryListDto>> GetPagedCategorysAsync(GetCategoryInput input);

        /// <summary>
        /// 通过Id获取分类信息进行编辑或修改 
        /// </summary>
        Task<GetCategoryForEditOutput> GetCategoryForEditAsync(NullableIdDto<int> input);

		  /// <summary>
        /// 通过指定id获取分类ListDto信息
        /// </summary>
		Task<CategoryListDto> GetCategoryByIdAsync(EntityDto<int> input);

        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CategoryListDto>> GetCategories(NullableIdDto<int> input);

        /// <summary>
        /// 新增或更改分类
        /// </summary>
        Task CreateOrUpdateCategoryAsync(CreateOrUpdateCategoryInput input);






        /// <summary>
        /// 删除分类
        /// </summary>
        Task DeleteCategoryAsync(EntityDto<int> input);

        /// <summary>
        /// 批量删除分类
        /// </summary>
        Task BatchDeleteCategoryAsync(List<int> input);

        #endregion




    }
}
