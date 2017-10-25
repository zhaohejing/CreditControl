using Abp.Application.Services.Dto;

namespace YT.Dto
{/// <summary>
/// ��ҳdtp
/// </summary>
    public class PagedAndSortedInputDto : PagedInputDto, ISortedResultRequest
    {/// <summary>
    /// �����ֶ�
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