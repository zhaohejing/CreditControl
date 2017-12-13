using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Models;

namespace YT.Adsences.Dtos
{
	/// <summary>
    /// 公告管理列表Dto
    /// </summary>
    [AutoMapFrom(typeof(Adsence))]
    public class AdsenceListDto : EntityDto<int>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public      string Title { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        public      bool IsActive { get; set; }
        ///内容
        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        public string Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public      DateTime CreationTime { get; set; }
    }
}
