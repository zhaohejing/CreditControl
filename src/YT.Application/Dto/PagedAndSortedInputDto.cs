using Abp.Application.Services.Dto;

namespace YT.Dto
{/// <summary>
/// ·ÖÒ³dtp
/// </summary>
    public class PagedAndSortedInputDto : PagedInputDto, ISortedResultRequest
    {/// <summary>
    /// ÅÅÐò×Ö¶Î
    /// </summary>
        public string Sorting { get; set; }
        /// <summary>
        /// ctor
        /// </summary>
        public PagedAndSortedInputDto()
        {
            MaxResultCount = AppConsts.DefaultPageSize;
        }
    }
}