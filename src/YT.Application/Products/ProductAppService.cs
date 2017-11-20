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
using Abp.UI;
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
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrderExcelExporter _orderExcelExporter;
        private readonly IRepository<CustomerCost> _costRepository;
      /// <summary>
      /// ctor
      /// </summary>
      /// <param name="productRepository"></param>
      /// <param name="productListExcelExporter"></param>
      /// <param name="orderRepository"></param>
      /// <param name="costRepository"></param>
      /// <param name="orderExcelExporter"></param>
        public ProductAppService(IRepository<Product, int> productRepository,
            IProductListExcelExporter productListExcelExporter, IRepository<Order> orderRepository, IRepository<CustomerCost> costRepository, IOrderExcelExporter orderExcelExporter)
        {
            _productRepository = productRepository;
            _productListExcelExporter = productListExcelExporter;
            _orderRepository = orderRepository;
            _costRepository = costRepository;
            _orderExcelExporter = orderExcelExporter;
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
        /// 根据查询条件获取产品分页列表
        /// </summary>
        public async Task<PagedResultDto<OrderListDto>> GetPagedOrdersAsync(GetOrderInput input)
        {

            var query = _orderRepository.GetAll()
               .WhereIf(!input.Contact.IsNullOrWhiteSpace(), c => c.Customer.Contact.Contains(input.Contact))
                 .WhereIf(!input.Account.IsNullOrWhiteSpace(), c => c.Customer.CompanyName.Contains(input.Account))
                 .WhereIf(!input.Product.IsNullOrWhiteSpace(), c => c.ProductName.Contains(input.Product))

                 .WhereIf(input.State==1, c => c.State.HasValue&&c.State.Value)
                 .WhereIf(input.State==2, c => c.State.HasValue&&!c.State.Value)
                 .WhereIf(input.State==3, c => !c.State.HasValue)
                 .WhereIf(input.RequireForm.HasValue && input.RequireForm.Value, c => c.FormId.HasValue && c.Form != null)
                 .WhereIf(input.RequireForm.HasValue && !input.RequireForm.Value, c => !c.FormId.HasValue)
                 .WhereIf(input.Start.HasValue, c => c.CreationTime >= input.Start.Value)
                 .WhereIf(input.End.HasValue, c => c.CreationTime < input.End.Value);

            var productCount = await query.CountAsync();

            var products = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var productListDtos = products.MapTo<List<OrderListDto>>();
            return new PagedResultDto<OrderListDto>(
            productCount,
            productListDtos
            );
        }

        /// <summary>
        /// 导出订单信息
        /// </summary>
        public async Task<FileDto> ExportOrdersAsync(GetOrderInput input)
        {

            var query = _orderRepository.GetAll()
                .WhereIf(!input.Contact.IsNullOrWhiteSpace(), c => c.Customer.Contact.Contains(input.Contact))
                  .WhereIf(!input.Account.IsNullOrWhiteSpace(), c => c.Customer.CompanyName.Contains(input.Account))
                  .WhereIf(!input.Product.IsNullOrWhiteSpace(), c => c.ProductName.Contains(input.Product))

                  .WhereIf(input.State == 1, c => c.State.HasValue && c.State.Value)
                 .WhereIf(input.State == 2, c => c.State.HasValue && !c.State.Value)
                 .WhereIf(input.State == 3, c => !c.State.HasValue)
                  .WhereIf(input.RequireForm.HasValue&&input.RequireForm.Value, c => c.FormId.HasValue&& c.Form != null)
                  .WhereIf(input.RequireForm.HasValue&&!input.RequireForm.Value, c => !c.FormId.HasValue)
                  .WhereIf(input.Start.HasValue, c => c.CreationTime >= input.Start.Value)
                  .WhereIf(input.End.HasValue, c => c.CreationTime < input.End.Value);
            var orders =await query.ToListAsync();

            var orderexport = orders.MapTo<List<OrderExportDto>>();
          return  _orderExcelExporter.ExportToFile(orderexport);
        }


        /// <summary>
        /// 获取订单详情
        /// </summary>
        public async Task<OrderListDto> GetOrderByIdAsync(EntityDto<int> input)
        {
            var order = await _orderRepository.FirstOrDefaultAsync(input.Id);
            if (order == null) throw new UserFriendlyException("该订单不存在");
            return order.MapTo<OrderListDto>();
        }
        /// <summary>
        /// 完成订单
        /// </summary>
        public async Task CompleteOrder(EntityDto<int> input)
        {
            var order = await _orderRepository.FirstOrDefaultAsync(input.Id);
            if (order == null) throw new UserFriendlyException("该订单不存在");

            if (order.State.HasValue && !order.State.Value) throw new UserFriendlyException("该订单已取消");

            var customer = order.Customer;
            if (customer.Balance < order.TotalPrice) throw new UserFriendlyException("该用户余额不足,请提醒充值");
            var cost = new CustomerCost()
            {
                Balance = customer.Balance,
                CustomerId = customer.Id
            };
            customer.Balance -= order.TotalPrice;
            cost.Cost = order.TotalPrice;
            cost.CurrentBalance = customer.Balance;
            order.State = true;
            await _costRepository.InsertAsync(cost);
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
