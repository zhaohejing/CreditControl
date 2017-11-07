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
using YT.ChargeRecords.Dtos;
using YT.Customers.Dtos;
using YT.Models;

namespace YT.ChargeRecords
{
    /// <summary>
    /// 充值记录服务实现
    /// </summary>
    [AbpAuthorize]


    public class ChargeRecordAppService : YtAppServiceBase, IChargeRecordAppService
    {
        private readonly IRepository<ChargeRecord, int> _chargeRecordRepository;
        private readonly IRepository<ApplyCharge, int> _applyRepository;
        private readonly IRepository<Customer, int> _customerRepository;
      /// <summary>
      /// ctor
      /// </summary>
      /// <param name="chargeRecordRepository"></param>
      /// <param name="applyRepository"></param>
      /// <param name="customerRepository"></param>
        public ChargeRecordAppService(IRepository<ChargeRecord, int> chargeRecordRepository,
            IRepository<ApplyCharge, int> applyRepository, IRepository<Customer, int> customerRepository)
        {
            _chargeRecordRepository = chargeRecordRepository;
            _applyRepository = applyRepository;
            _customerRepository = customerRepository;
        }


        #region 实体的自定义扩展方法
        private IQueryable<ChargeRecord> ChargeRecordRepositoryAsNoTrack => _chargeRecordRepository.GetAll().AsNoTracking();


        #endregion


        #region 充值记录管理

        /// <summary>
        /// 根据查询条件获取充值记录分页列表
        /// </summary>
        public async Task<PagedResultDto<ChargeRecordListDto>> GetPagedChargeRecordsAsync(GetChargeRecordInput input)
        {

            var query = ChargeRecordRepositoryAsNoTrack;
            query = query.WhereIf(!input.Name.IsNullOrWhiteSpace(), c => c.CustomerName.Contains(input.Name))
                .WhereIf(input.Money.HasValue, c => c.ChargeMoney == input.Money.Value)
                .WhereIf(input.Start.HasValue, c => c.CreationTime >= input.Start.Value)
                .WhereIf(input.End.HasValue, c => c.CreationTime > input.End.Value);
            var chargeRecordCount = await query.CountAsync();

            var chargeRecords = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var chargeRecordListDtos = chargeRecords.MapTo<List<ChargeRecordListDto>>();
            return new PagedResultDto<ChargeRecordListDto>(
            chargeRecordCount,
            chargeRecordListDtos
            );
        }

        /// <summary>
        /// 根据查询条件获取充值记录分页列表
        /// </summary>
        public async Task<PagedResultDto<ApplyChargeListDto>> GetPagedApplyChargesAsync(GetChargeRecordInput input)
        {

         var   query = _applyRepository.GetAll().WhereIf(!input.Name.IsNullOrWhiteSpace(), c => c.CustomerName.Contains(input.Name))
                .WhereIf(input.Money.HasValue, c => c.ChargeMoney == input.Money.Value)
                .WhereIf(input.Start.HasValue, c => c.CreationTime >= input.Start.Value)
                .WhereIf(input.End.HasValue, c => c.CreationTime > input.End.Value);
            var chargeRecordCount = await query.CountAsync();

            var chargeRecords = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var chargeRecordListDtos = chargeRecords.MapTo<List<ApplyChargeListDto>>();
            return new PagedResultDto<ApplyChargeListDto>(
            chargeRecordCount,
            chargeRecordListDtos
            );
        }
        /// <summary>
        /// 删除充值申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteApplyChargeAsync(EntityDto<int> input)
        {
            await _applyRepository.DeleteAsync(input.Id);
        }


        /// <summary>
        /// 用户充值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ChargeCustomer(ChargeInput input)
        {
            var current = await AbpSession.Current();
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.Id == input.Id);
            if (customer == null) throw new UserFriendlyException("当前客户信息不存在");
            customer.Balance += input.Money;
            await _chargeRecordRepository.InsertAsync(new ChargeRecord()
            {
                ActionName = current.Name,
                ChargeMoney = input.Money,
                CustomerId = customer.Id,
                CustomerName = customer.CompanyName
            });
        }
        /// <summary>
        /// 用户完成充值申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ChargeApplyCustomer(ChargeInput input)
        {
            var current = await AbpSession.Current();
            var apply = await _applyRepository.FirstOrDefaultAsync(input.Id);
            if (apply == null) throw new UserFriendlyException("充值申请不存在");
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.Id == apply.CustomerId);
            if (customer == null) throw new UserFriendlyException("当前客户信息不存在");
            customer.Balance += input.Money;
            await _chargeRecordRepository.InsertAsync(new ChargeRecord()
            {
                ActionName = current.Name,
                ChargeMoney = input.Money,
                CustomerId = customer.Id,
                CustomerName = customer.CompanyName
            });
            apply.State = true;
            apply.ActrueMoney = input.Money;
            apply.ActionName = current.Name;
        }
        #endregion










    }
}
