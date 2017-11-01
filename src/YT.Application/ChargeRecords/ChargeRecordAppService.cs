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
using YT.ChargeRecords.Dtos;
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

       /// <summary>
       /// ctor
       /// </summary>
       /// <param name="chargeRecordRepository"></param>
       /// <param name="applyRepository"></param>
        public ChargeRecordAppService(IRepository<ChargeRecord, int> chargeRecordRepository,
            IRepository<ApplyCharge, int> applyRepository)
        {
            _chargeRecordRepository = chargeRecordRepository;
            _applyRepository = applyRepository;
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

        #endregion










    }
}
