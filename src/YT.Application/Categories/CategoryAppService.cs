using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using YT.Categories.Dtos;
using YT.Models;

namespace YT.Categories
{
    /// <summary>
    /// 分类服务实现
    /// </summary>

    public class CategoryAppService : YtAppServiceBase, ICategoryAppService
    {
        private readonly IRepository<Category, int> _categoryRepository;


        /// <summary>
        /// 构造方法
        /// </summary>
        public CategoryAppService(IRepository<Category, int> categoryRepository
  )
        {
            _categoryRepository = categoryRepository;

        }


        #region 实体的自定义扩展方法
        private IQueryable<Category> CategoryRepositoryAsNoTrack => _categoryRepository.GetAll().AsNoTracking();


        #endregion


        #region 分类管理

        /// <summary>
        /// 根据查询条件获取分类分页列表
        /// </summary>
        public async Task<PagedResultDto<CategoryListDto>> GetPagedCategorysAsync(GetCategoryInput input)
        {

            var query = CategoryRepositoryAsNoTrack;
            query = query.WhereIf(!input.Name.IsNullOrWhiteSpace(), c => c.Name.Contains(input.Name));

            var categoryCount = await query.CountAsync();

            var categorys = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var categoryListDtos = categorys.MapTo<List<CategoryListDto>>();
            return new PagedResultDto<CategoryListDto>(
            categoryCount,
            categoryListDtos
            );
        }
        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<CategoryListDto>> GetCategories(NullableIdDto<int> input )
        {
            var query = CategoryRepositoryAsNoTrack;
            query = query.WhereIf(input.Id.HasValue, c => c.ParentId == input.Id.Value)
                .WhereIf(!input.Id.HasValue, c => !c.ParentId.HasValue);
            var list =await query.ToListAsync();
           return list.MapTo<List<CategoryListDto>>();
        }
        /// <summary>
        /// 通过Id获取分类信息进行编辑或修改 
        /// </summary>
        public async Task<GetCategoryForEditOutput> GetCategoryForEditAsync(NullableIdDto<int> input)
        {
            var output = new GetCategoryForEditOutput();

            CategoryEditDto categoryEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _categoryRepository.GetAsync(input.Id.Value);
                categoryEditDto = entity.MapTo<CategoryEditDto>();
            }
            else
            {
                categoryEditDto = new CategoryEditDto();
            }

            output.Category = categoryEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取分类ListDto信息
        /// </summary>
        public async Task<CategoryListDto> GetCategoryByIdAsync(EntityDto<int> input)
        {
            var entity = await _categoryRepository.GetAsync(input.Id);

            return entity.MapTo<CategoryListDto>();
        }







        /// <summary>
        /// 新增或更改分类
        /// </summary>
        public async Task CreateOrUpdateCategoryAsync(CreateOrUpdateCategoryInput input)
        {
            if (input.CategoryEditDto.Id.HasValue)
            {
                await UpdateCategoryAsync(input.CategoryEditDto);
            }
            else
            {
                await CreateCategoryAsync(input.CategoryEditDto);
            }
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        protected virtual async Task<CategoryEditDto> CreateCategoryAsync(CategoryEditDto input)
        {

            var entity = input.MapTo<Category>();

            entity = await _categoryRepository.InsertAsync(entity);
            return entity.MapTo<CategoryEditDto>();
        }

        /// <summary>
        /// 编辑分类
        /// </summary>
        protected virtual async Task UpdateCategoryAsync(CategoryEditDto input)
        {

            var entity = await _categoryRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _categoryRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        public async Task DeleteCategoryAsync(EntityDto<int> input)
        {
            await _categoryRepository.DeleteAsync(c => c.Id == input.Id);
        }

        /// <summary>
        /// 批量删除分类
        /// </summary>
        public async Task BatchDeleteCategoryAsync(List<int> input)
        {
            await _categoryRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion










    }
}
