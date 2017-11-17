using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using YT.Authorizations.PermissionDefault;
using YT.CustomerPreferencePrices.Dtos;
using YT.Models;

namespace YT.CustomerPreferencePrices
{
    /// <summary>
    /// 用户折扣价格设置服务实现
    /// </summary>
    [AbpAuthorize]


    public class CustomerPreferencePriceAppService : YtAppServiceBase, ICustomerPreferencePriceAppService
    {
        private readonly IRepository<CustomerPreferencePrice, int> _customerPreferencePriceRepository;
        private readonly IRepository<Customer, int> _customerRepository;
        private readonly IRepository<Product, int> _productRepository;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="customerPreferencePriceRepository"></param>
        /// <param name="customerRepository"></param>
        /// <param name="productRepository"></param>
        public CustomerPreferencePriceAppService(IRepository<CustomerPreferencePrice, int> customerPreferencePriceRepository,
            IRepository<Customer, int> customerRepository, IRepository<Product, int> productRepository)
        {
            _customerPreferencePriceRepository = customerPreferencePriceRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }


        #region 实体的自定义扩展方法
        private IQueryable<CustomerPreferencePrice> CustomerPreferencePriceRepositoryAsNoTrack => _customerPreferencePriceRepository.GetAll().AsNoTracking();


        #endregion


        #region 用户折扣价格设置管理

        /// <summary>
        /// 根据查询条件获取用户折扣价格设置分页列表
        /// </summary>
        public async Task<List<CustomerPreferencePriceListDto>> GetProductPricesAsync(EntityDto<int> input )
        {
            var product =await _productRepository.FirstOrDefaultAsync(input.Id);
            if (product == null) throw new UserFriendlyException("该商品不存在");
            var list = from c in await _customerRepository.GetAllListAsync()
                       join d
                       in await _customerPreferencePriceRepository.GetAllListAsync(c => c.ProductId == input.Id)
                       on c.Id equals d.CustomerId into temp
                       from tt in temp.DefaultIfEmpty()
                       select new { customer = c, tt };
            if (!list.Any()) return null;
            {
                var result = list.Select(c => new CustomerPreferencePriceListDto()
                {
                    Id = c.tt?.Id,
                    Contact = c.customer.Contact,
                    CustomerId = c.customer.Id,
                    CustomerName = c.customer.CompanyName,
                    Price = c.tt==null?0:c.tt.Price,
                    ProductId = c.tt == null ? product.Id:c.tt.ProductId,
                    ProductName = product.ProductName
                }).ToList();
                return result;
            }
        }

        /// <summary>
        /// 获取客户所有商品定价
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<CustomerPreferencePriceListDto>> GetCustomerPricesAsync(EntityDto<int> input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(input.Id);
            if (customer == null) throw new UserFriendlyException("该客户不存在");
            var list = from c in await _productRepository.GetAllListAsync()
                       join d
                       in await _customerPreferencePriceRepository.GetAllListAsync(c => c.CustomerId == input.Id)
                       on c.Id equals d.ProductId into temp
                       from tt in temp.DefaultIfEmpty()
                       select new { product = c, tt };
            if (!list.Any()) return null;
            {
                var result = list.Select(c => new CustomerPreferencePriceListDto()
                {
                    Id = c.tt?.Id,
                    Contact = customer.Contact,
                    CustomerId = customer.Id,
                    CustomerName = customer.CompanyName,
                    Price = c.tt == null ? 0 : c.tt.Price,
                    ProductId =c.product.Id,
                    ProductName = c.product.ProductName,
                    LevelName =c.product.LevelOne!=null&&c.product.LevelTwo!=null?
                    c.product.LevelOne.Name+$"({c.product.LevelTwo.Name})":string.Empty
                }).ToList();
                return result;
            }
        }

        /// <summary>
        /// 新增用户折扣价格设置
        /// </summary>
        public virtual async Task ModifyPriceAsync(ModifyCustomerPreferencePriceInput input)
        {
            if (input.CustomerPrices.Any())
            {
                foreach (var dto in input.CustomerPrices)
                {
                    var model = dto.MapTo<CustomerPreferencePrice>();
                    await _customerPreferencePriceRepository.InsertOrUpdateAsync(model);
                }
            }
        }

        #endregion










    }
}
