using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YT.Dto
{/// <summary>
/// ������ҳdto
/// </summary>
    public class PagedAndFilteredInputDto : IPagedResultRequest
    {/// <summary>
    /// ҳ����
    /// </summary>
        [Range(1, AppConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }
        /// <summary>
        /// ��������
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