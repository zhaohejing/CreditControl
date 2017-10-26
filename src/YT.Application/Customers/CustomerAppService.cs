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
using YT.Customers.Dtos;
using YT.Customers.Exporting;
using YT.Dto;
using YT.Models;

namespace YT.Customers
{
    /// <summary>
    /// 客户信息服务实现
    /// </summary>
    [AbpAuthorize]


    public class CustomerAppService : YtAppServiceBase, ICustomerAppService
    {
        private readonly IRepository<Customer, int> _customerRepository;
        private readonly ICustomerListExcelExporter _customerListExcelExporter;
        /// <summary>
        /// 构造方法
        /// </summary>
        public CustomerAppService(IRepository<Customer, int> customerRepository,
      ICustomerListExcelExporter customerListExcelExporter
  )
        {
            _customerRepository = customerRepository;
            _customerListExcelExporter = customerListExcelExporter;
        }


        #region 实体的自定义扩展方法
        private IQueryable<Customer> CustomerRepositoryAsNoTrack => _customerRepository.GetAll().AsNoTracking();


        #endregion


        #region 客户信息管理

        /// <summary>
        /// 根据查询条件获取客户信息分页列表
        /// </summary>
        public async Task<PagedResultDto<CustomerListDto>> GetPagedCustomersAsync(GetCustomerInput input)
        {

            var query = CustomerRepositoryAsNoTrack;
            query = query.WhereIf(input.CompanyName.IsNullOrWhiteSpace(), c => c.CompanyName.Contains(input.CompanyName))
                .WhereIf(input.Contact.IsNullOrWhiteSpace(), c => c.Contact.Contains(input.Contact))
                .WhereIf(input.Mobile.IsNullOrWhiteSpace(), c => c.Mobile.Contains(input.Mobile));
            var customerCount = await query.CountAsync();

            var customers = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var customerListDtos = customers.MapTo<List<CustomerListDto>>();
            return new PagedResultDto<CustomerListDto>(
            customerCount,
            customerListDtos
            );
        }

        /// <summary>
        /// 通过Id获取客户信息信息进行编辑或修改 
        /// </summary>
        public async Task<GetCustomerForEditOutput> GetCustomerForEditAsync(NullableIdDto<int> input)
        {
            var output = new GetCustomerForEditOutput();

            CustomerEditDto customerEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _customerRepository.GetAsync(input.Id.Value);
                customerEditDto = entity.MapTo<CustomerEditDto>();
            }
            else
            {
                customerEditDto = new CustomerEditDto();
            }

            output.Customer = customerEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取客户信息ListDto信息
        /// </summary>
        public async Task<CustomerListDto> GetCustomerByIdAsync(EntityDto<int> input)
        {
            var entity = await _customerRepository.GetAsync(input.Id);

            return entity.MapTo<CustomerListDto>();
        }







        /// <summary>
        /// 新增或更改客户信息
        /// </summary>
        public async Task CreateOrUpdateCustomerAsync(CreateOrUpdateCustomerInput input)
        {
            if (input.CustomerEditDto.Id.HasValue)
            {
                await UpdateCustomerAsync(input.CustomerEditDto);
            }
            else
            {
                await CreateCustomerAsync(input.CustomerEditDto);
            }
        }

        /// <summary>
        /// 新增客户信息
        /// </summary>
        public virtual async Task<CustomerEditDto> CreateCustomerAsync(CustomerEditDto input)
        {

            var entity = input.MapTo<Customer>();

            entity = await _customerRepository.InsertAsync(entity);
            return entity.MapTo<CustomerEditDto>();
        }

        /// <summary>
        /// 编辑客户信息
        /// </summary>
        public virtual async Task UpdateCustomerAsync(CustomerEditDto input)
        {
            var entity = await _customerRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _customerRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除客户信息
        /// </summary>
        public async Task DeleteCustomerAsync(EntityDto<int> input)
        {
            await _customerRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除客户信息
        /// </summary>
        public async Task BatchDeleteCustomerAsync(List<int> input)
        {
            await _customerRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion
        #region 客户信息的Excel导出功能

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public async Task<FileDto> GetCustomerToExcel()
        {
            var entities = await _customerRepository.GetAll().ToListAsync();

            var dtos = entities.MapTo<List<CustomerListDto>>();

            var fileDto = _customerListExcelExporter.ExportCustomerToFile(dtos);



            return fileDto;
        }


        #endregion










    }
}
