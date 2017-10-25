using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YT.Dto
{/// <summary>
/// 基础分页dto
/// </summary>
    public class PagedAndFilteredInputDto : IPagedResultRequest
    {/// <summary>
    /// 页容量
    /// </summary>
        [Range(1, AppConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }
        /// <summary>
        /// 跳过条数
        /// </summary>
        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }
        /// <summary>
        /// 过滤条件
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// ctor
        /// </summary>
        public PagedAndFilteredInputDto()
        {
            MaxResultCount = AppConsts.DefaultPageSize;
        }
    }
}