using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YT.Dto
{/// <summary>
/// 分页dto
/// </summary>
    public class PagedInputDto : IPagedResultRequest
    {/// <summary>
    /// 也容量
    /// </summary>
        [Range(1, AppConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }
        /// <summary>
        /// 跳过条数
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