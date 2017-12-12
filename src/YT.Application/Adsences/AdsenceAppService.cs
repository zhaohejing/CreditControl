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
using YT.Adsences.Dtos;
using YT.Models;

namespace YT.Adsences
{
    /// <summary>
    /// 公告管理服务实现
    /// </summary>


    public class AdsenceAppService : YtAppServiceBase, IAdsenceAppService
    {
        private readonly IRepository<Adsence, int> _adsenceRepository;
        /// <summary>
        /// 构造方法
        /// </summary>
        public AdsenceAppService(IRepository<Adsence, int> adsenceRepository
  )
        {
            _adsenceRepository = adsenceRepository;

        }


        #region 实体的自定义扩展方法
        private IQueryable<Adsence> AdsenceRepositoryAsNoTrack => _adsenceRepository.GetAll().AsNoTracking();


        #endregion


        #region 公告管理管理

        /// <summary>
        /// 根据查询条件获取公告管理分页列表
        /// </summary>
        public async Task<PagedResultDto<AdsenceListDto>> GetPagedAdsencesAsync(GetAdsenceInput input)
        {

            var query = AdsenceRepositoryAsNoTrack;
            query = query.WhereIf(!input.Filter.IsNullOrWhiteSpace(), c => c.Title.Contains(input.Filter));

            var adsenceCount = await query.CountAsync();

            var adsences = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var adsenceListDtos = adsences.MapTo<List<AdsenceListDto>>();
            return new PagedResultDto<AdsenceListDto>(
            adsenceCount,
            adsenceListDtos
            );
        }

        /// <summary>
        /// 通过Id获取公告管理信息进行编辑或修改 
        /// </summary>
        [AbpAuthorize]

        public async Task<GetAdsenceForEditOutput> GetAdsenceForEditAsync(NullableIdDto<int> input)
        {
            var output = new GetAdsenceForEditOutput();

            AdsenceEditDto adsenceEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _adsenceRepository.GetAsync(input.Id.Value);
                adsenceEditDto = entity.MapTo<AdsenceEditDto>();
            }
            else
            {
                adsenceEditDto = new AdsenceEditDto();
            }

            output.Adsence = adsenceEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取公告管理ListDto信息
        /// </summary>
        public async Task<AdsenceListDto> GetAdsenceByIdAsync(EntityDto<int> input)
        {
            var entity = await _adsenceRepository.GetAsync(input.Id);

            return entity.MapTo<AdsenceListDto>();
        }


        /// <summary>
        /// 发布公告
        /// </summary>
        [AbpAuthorize]

        public async Task PublicAdsenceAsync(EntityDto<int> input)
        {
            var entity = await _adsenceRepository.GetAsync(input.Id);
            entity.IsActive = true;
        }
        /// <summary>
        /// 新增或更改公告管理
        /// </summary>
        [AbpAuthorize]

        public async Task CreateOrUpdateAdsenceAsync(CreateOrUpdateAdsenceInput input)
        {
            if (input.AdsenceEditDto.Id.HasValue)
            {
                await UpdateAdsenceAsync(input.AdsenceEditDto);
            }
            else
            {
                await CreateAdsenceAsync(input.AdsenceEditDto);
            }
        }

        /// <summary>
        /// 新增公告管理
        /// </summary>
        protected virtual async Task<AdsenceEditDto> CreateAdsenceAsync(AdsenceEditDto input)
        {

            var entity = input.MapTo<Adsence>();

            entity = await _adsenceRepository.InsertAsync(entity);
            return entity.MapTo<AdsenceEditDto>();
        }

        /// <summary>
        /// 编辑公告管理
        /// </summary>
        protected virtual async Task UpdateAdsenceAsync(AdsenceEditDto input)
        {

            var entity = await _adsenceRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _adsenceRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除公告管理
        /// </summary>
        [AbpAuthorize]
        public async Task DeleteAdsenceAsync(EntityDto<int> input)
        {
            await _adsenceRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除公告管理
        /// </summary>
        [AbpAuthorize]
        public async Task BatchDeleteAdsenceAsync(List<int> input)
        {
            await _adsenceRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion










    }
}
