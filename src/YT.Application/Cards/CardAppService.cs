using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using YT.Authorizations.PermissionDefault;
using YT.Cards.Dtos;
using YT.Cards.Exporting;
using YT.Dto;
using YT.Models;



namespace YT.Cards
{
    /// <summary>
    /// 充值卡服务实现
    /// </summary>
    [AbpAuthorize]
    public class CardAppService : YtAppServiceBase, ICardAppService
    {
        private readonly IRepository<Card, int> _cardRepository;
        private readonly ICardListExcelExporter _cardListExcelExporter;
        /// <summary>
        /// 构造方法
        /// </summary>
        public CardAppService(IRepository<Card, int> cardRepository
      , ICardListExcelExporter cardListExcelExporter)
        {
            _cardRepository = cardRepository;
            _cardListExcelExporter = cardListExcelExporter;
        }


        #region 实体的自定义扩展方法
        /// <summary>
        /// 
        /// </summary>
        private IQueryable<Card> CardRepositoryAsNoTrack => _cardRepository.GetAll().AsNoTracking();


        #endregion


        #region 充值卡管理

        /// <summary>
        /// 根据查询条件获取充值卡分页列表
        /// </summary>
        public async Task<PagedResultDto<CardListDto>> GetPagedCardsAsync(GetCardInput input)
        {

            var query = CardRepositoryAsNoTrack;

            query = query.WhereIf(!input.Code.IsNullOrWhiteSpace(), c => c.CardCode.Contains(input.Code))
                .WhereIf(input.State.HasValue, c => c.IsUsed == input.State.Value)
                .WhereIf(input.Rmb.HasValue, c => c.Money ==input.Rmb.Value*100);
            var cardCount = await query.CountAsync();
            var cards = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();
            var cardListDtos = cards.MapTo<List<CardListDto>>();
            return new PagedResultDto<CardListDto>(
            cardCount,
            cardListDtos
            );
        }
        /// <summary>
        /// 通过Id获取充值卡信息进行编辑或修改 
        /// </summary>
        public async Task<GetCardForEditOutput> GetCardForEditAsync(NullableIdDto<int> input)
        {
            var output = new GetCardForEditOutput();
            CardEditDto cardEditDto;
            if (input.Id.HasValue)
            {
                var entity = await _cardRepository.GetAsync(input.Id.Value);
                cardEditDto = entity.MapTo<CardEditDto>();
            }
            else
            {
                cardEditDto = new CardEditDto();
            }
            output.Card = cardEditDto;
            return output;
        }
        /// <summary>
        /// 通过指定id获取充值卡ListDto信息
        /// </summary>
        public async Task<CardListDto> GetCardByIdAsync(EntityDto<int> input)
        {
            var entity = await _cardRepository.GetAsync(input.Id);
            return entity.MapTo<CardListDto>();
        }
        /// <summary>
        /// 新增或更改充值卡
        /// </summary>
        public async Task CreateOrUpdateCardAsync(CreateOrUpdateCardInput input)
        {
            if (input.CardEditDto.Id.HasValue)
            {
                await UpdateCardAsync(input.CardEditDto);
            }
            else
            {
                await CreateCardAsync(input.CardEditDto);
            }
        }
        /// <summary>
        /// 新增充值卡
        /// </summary>
        protected virtual async Task<CardEditDto> CreateCardAsync(CardEditDto input)
        {
            var entity = input.MapTo<Card>();
            entity.CardCode = Guid.NewGuid().ToString("N");
            entity = await _cardRepository.InsertAsync(entity);
            return entity.MapTo<CardEditDto>();
        }

        /// <summary>
        /// 编辑充值卡
        /// </summary>
        protected virtual async Task UpdateCardAsync(CardEditDto input)
        {
            if (input.Id != null)
            {
                var entity = await _cardRepository.GetAsync(input.Id.Value);
                input.MapTo(entity);
                await _cardRepository.UpdateAsync(entity);
            }
        }

        /// <summary>
        /// 删除充值卡
        /// </summary>
        public async Task DeleteCardAsync(EntityDto<int> input)
        {
            await _cardRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除充值卡
        /// </summary>
        public async Task BatchDeleteCardAsync(List<int> input)
        {
            await _cardRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion
        #region 充值卡的Excel导出功能
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public async Task<FileDto> GetCardToExcel()
        {
            var entities = await _cardRepository.GetAll().ToListAsync();
            var dtos = entities.MapTo<List<CardListDto>>();
            var fileDto = _cardListExcelExporter.ExportCardToFile(dtos);
            return fileDto;
        }


        #endregion










    }
}
