using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using YT.Dto;
using YT.Models;
using YT.Promoters.Dtos;
using YT.Promoters.Exporting;
namespace YT.Promoters
{
    /// <summary>
    /// 推广员管理服务实现
    /// </summary>
    [AbpAuthorize]
    public class PromoterAppService : YtAppServiceBase, IPromoterAppService
    {
        private readonly IRepository<Promoter, int> _promoterRepository;
        private readonly IPromoterListExcelExporter _promoterListExcelExporter;
        /// <summary>
        /// 构造方法
        /// </summary>
        public PromoterAppService(IRepository<Promoter, int> promoterRepository,
              IPromoterListExcelExporter promoterListExcelExporter)
        {
            _promoterRepository = promoterRepository;
            _promoterListExcelExporter = promoterListExcelExporter;
        }
        #region 实体的自定义扩展方法
        /// <summary>
        /// 
        /// </summary>
        private IQueryable<Promoter> PromoterRepositoryAsNoTrack => _promoterRepository.GetAll().AsNoTracking();
        #endregion
        #region 推广员管理管理
        /// <summary>
        /// 根据查询条件获取推广员管理分页列表
        /// </summary>
        public async Task<PagedResultDto<PromoterListDto>> GetPagedPromotersAsync(GetPromoterInput input)
        {
            var query = PromoterRepositoryAsNoTrack;
            query = query.WhereIf(!input.Name.IsNullOrWhiteSpace(), c => c.PromoterName.Contains(input.Name))
                .WhereIf(!input.Mobile.IsNullOrWhiteSpace(), c => c.Mobile.Contains(input.Mobile));
            var promoterCount = await query.CountAsync();
            var promoters = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();
            var promoterListDtos = promoters.MapTo<List<PromoterListDto>>();
            return new PagedResultDto<PromoterListDto>(
            promoterCount,
            promoterListDtos
            );
        }

        /// <summary>
        /// 通过Id获取推广员管理信息进行编辑或修改 
        /// </summary>
        public async Task<GetPromoterForEditOutput> GetPromoterForEditAsync(NullableIdDto<int> input)
        {
            var output = new GetPromoterForEditOutput();

            PromoterEditDto promoterEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _promoterRepository.GetAsync(input.Id.Value);
                promoterEditDto = entity.MapTo<PromoterEditDto>();
            }
            else
            {
                promoterEditDto = new PromoterEditDto();
            }

            output.Promoter = promoterEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取推广员管理ListDto信息
        /// </summary>
        public async Task<PromoterListDto> GetPromoterByIdAsync(EntityDto<int> input)
        {
            var entity = await _promoterRepository.GetAsync(input.Id);

            return entity.MapTo<PromoterListDto>();
        }







        /// <summary>
        /// 新增或更改推广员管理
        /// </summary>
        public async Task CreateOrUpdatePromoterAsync(CreateOrUpdatePromoterInput input)
        {
            if (input.PromoterEditDto.Id.HasValue)
            {
                await UpdatePromoterAsync(input.PromoterEditDto);
            }
            else
            {
                await CreatePromoterAsync(input.PromoterEditDto);
            }
        }

        /// <summary>
        /// 新增推广员管理
        /// </summary>
        protected virtual async Task<PromoterEditDto> CreatePromoterAsync(PromoterEditDto input)
        {

            var entity = input.MapTo<Promoter>();
            entity.OnlyCode = Guid.NewGuid().ToString("N");
            entity = await _promoterRepository.InsertAsync(entity);
            return entity.MapTo<PromoterEditDto>();
        }

        /// <summary>
        /// 编辑推广员管理
        /// </summary>
        protected virtual async Task UpdatePromoterAsync(PromoterEditDto input)
        {
            if (input.Id != null)
            {
                var entity = await _promoterRepository.GetAsync(input.Id.Value);
                input.MapTo(entity);
                await _promoterRepository.UpdateAsync(entity);
            }
        }

        /// <summary>
        /// 删除推广员管理
        /// </summary>
        public async Task DeletePromoterAsync(EntityDto<int> input)
        {
            await _promoterRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除推广员管理
        /// </summary>
        public async Task BatchDeletePromoterAsync(List<int> input)
        {
            await _promoterRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion
        #region 推广员管理的Excel导出功能
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public async Task<FileDto> GetPromoterToExcel()
        {
            var entities = await _promoterRepository.GetAll().ToListAsync();
            var dtos = entities.MapTo<List<PromoterListDto>>();
            var fileDto = _promoterListExcelExporter.ExportPromoterToFile(dtos);
            return fileDto;
        }
        #endregion
    }
}
