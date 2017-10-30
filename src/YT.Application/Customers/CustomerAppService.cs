using System;
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
        private readonly IRepository<ChargeRecord> _chargeRecordRepository;
        private readonly IRepository<ApplyCharge> _applyChargeRepository;
        /// <summary>
        /// 构造方法
        /// </summary>
        public CustomerAppService(IRepository<Customer, int> customerRepository,
      ICustomerListExcelExporter customerListExcelExporter, IRepository<ChargeRecord> chargeRecordRepository, IRepository<ApplyCharge> applyChargeRepository)
        {
            _customerRepository = customerRepository;
            _customerListExcelExporter = customerListExcelExporter;
            _chargeRecordRepository = chargeRecordRepository;
            _applyChargeRepository = applyChargeRepository;
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
            query = query.WhereIf(!input.CompanyName.IsNullOrWhiteSpace(), c => c.CompanyName.Contains(input.CompanyName))
                .WhereIf(!input.Contact.IsNullOrWhiteSpace(), c => c.Contact.Contains(input.Contact))
                .WhereIf(!input.City.IsNullOrWhiteSpace(), c => c.City.Contains(input.City))
                .WhereIf(!input.Mobile.IsNullOrWhiteSpace(), c => c.Mobile.Contains(input.Mobile));
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
        /// 用户审核
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AuditCustomer(AuditInput input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(input.Id);
            if (customer == null) throw new UserFriendlyException("当前客户信息不存在");
            customer.State = input.State;
            customer.AuditOpinion = input.Opinion;
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ResetCustomer(ResetInput input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(input.Id);
            if (customer == null) throw new UserFriendlyException("当前客户信息不存在");
            customer.Password = input.Password;
        }

        /// <summary>
        /// 用户充值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ChargeCustomer(ChargeInput input)
        {
            var current =await AbpSession.Current();
            var customer = await _customerRepository.FirstOrDefaultAsync(input.Id);
            if (customer == null) throw new UserFriendlyException("当前客户信息不存在");
            customer.Balance = input.Money;
            await _chargeRecordRepository.InsertAsync(new ChargeRecord()
            {
                ActionName = current.Name,
                ActionTime = DateTime.Now,
                ChargeMoney = input.Money,
                CustomerId = customer.Id,
                CustomerName = customer.CompanyName
            });
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
        protected virtual async Task<CustomerEditDto> CreateCustomerAsync(CustomerEditDto input)
        {

            var entity = input.MapTo<Customer>();

            entity = await _customerRepository.InsertAsync(entity);
            return entity.MapTo<CustomerEditDto>();
        }

        /// <summary>
        /// 编辑客户信息
        /// </summary>
        protected virtual async Task UpdateCustomerAsync(CustomerEditDto input)
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
