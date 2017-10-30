using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using YT.Dto;
using YT.Models;
using YT.Products.Dtos;
using YT.Products.Exporting;

namespace YT.Products
{
    /// <summary>
    /// 产品服务实现
    /// </summary>
    [AbpAuthorize]


    public class ProductAppService : YtAppServiceBase, IProductAppService
    {
        private readonly IRepository<Product, int> _productRepository;
        private readonly IProductListExcelExporter _productListExcelExporter;


        /// <summary>
        /// 构造方法
        /// </summary>
        public ProductAppService(IRepository<Product, int> productRepository,
            IProductListExcelExporter productListExcelExporter
  )
        {
            _productRepository = productRepository;
            _productListExcelExporter = productListExcelExporter;
        }


        #region 实体的自定义扩展方法
        private IQueryable<Product> ProductRepositoryAsNoTrack => _productRepository.GetAll().AsNoTracking();


        #endregion


        #region 产品管理

        /// <summary>
        /// 根据查询条件获取产品分页列表
        /// </summary>
        public async Task<PagedResultDto<ProductListDto>> GetPagedProductsAsync(GetProductInput input)
        {

            var query = ProductRepositoryAsNoTrack;
            query = query.WhereIf(!input.Name.IsNullOrWhiteSpace(), c => c.ProductName.Contains(input.Name))
                .WhereIf(input.State.HasValue, c => c.IsActive == input.State.Value)
                .WhereIf(input.LevelOne.HasValue, c => c.LevelOneId == input.LevelOne.Value)
                .WhereIf(input.LevelTwo.HasValue, c => c.LevelTwoId == input.LevelTwo.Value);

            var productCount = await query.CountAsync();

            var products = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var productListDtos = products.MapTo<List<ProductListDto>>();
            return new PagedResultDto<ProductListDto>(
            productCount,
            productListDtos
            );
        }

        /// <summary>
        /// 通过Id获取产品信息进行编辑或修改 
        /// </summary>
        public async Task<GetProductForEditOutput> GetProductForEditAsync(NullableIdDto<int> input)
        {
            var output = new GetProductForEditOutput();
            ProductEditDto productEditDto;
            if (input.Id.HasValue)
            {
                var entity = await _productRepository.GetAsync(input.Id.Value);
                productEditDto = entity.MapTo<ProductEditDto>();
            }
            else
            {
                productEditDto = new ProductEditDto();
            }
            output.Product = productEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取产品ListDto信息
        /// </summary>
        public async Task<ProductListDto> GetProductByIdAsync(EntityDto<int> input)
        {
            var entity = await _productRepository.GetAsync(input.Id);
            return entity.MapTo<ProductListDto>();
        }
        /// <summary>
        /// 新增或更改产品
        /// </summary>
        public async Task CreateOrUpdateProductAsync(CreateOrUpdateProductInput input)
        {
            if (input.ProductEditDto.Id.HasValue)
            {
                await UpdateProductAsync(input.ProductEditDto);
            }
            else
            {
                await CreateProductAsync(input.ProductEditDto);
            }
        }

        /// <summary>
        /// 新增产品
        /// </summary>
        public virtual async Task<ProductEditDto> CreateProductAsync(ProductEditDto input)
        {
            var entity = input.MapTo<Product>();
            entity = await _productRepository.InsertAsync(entity);
            return entity.MapTo<ProductEditDto>();
        }
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateState(EntityDto<int> input)
        {
            var model = await _productRepository.FirstOrDefaultAsync(input.Id);
            model.IsActive = !model.IsActive;
        }

        /// <summary>
        /// 编辑产品
        /// </summary>
        public virtual async Task UpdateProductAsync(ProductEditDto input)
        {
            var entity = await _productRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _productRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        public async Task DeleteProductAsync(EntityDto<int> input)
        {
            await _productRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除产品
        /// </summary>
        public async Task BatchDeleteProductAsync(List<int> input)
        {
            await _productRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion
        #region 产品的Excel导出功能
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public async Task<FileDto> GetProductToExcel()
        {
            var entities = await _productRepository.GetAll().ToListAsync();

            var dtos = entities.MapTo<List<ProductListDto>>();

            var fileDto = _productListExcelExporter.ExportProductToFile(dtos);
            return fileDto;
        }


        #endregion










    }
}
