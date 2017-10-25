using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YT.Dto
{/// <summary>
/// ��ҳdto
/// </summary>
    public class PagedInputDto : IPagedResultRequest
    {/// <summary>
    /// Ҳ����
    /// </summary>
        [Range(1, AppConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }
        /// <summary>
        /// ctor
        /// </summary>
        public PagedInputDto()
        {
            MaxResultCount = AppConsts.DefaultPageSize;
        }
    }
}