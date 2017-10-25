                            
using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Models;
namespace YT.Promoters.Dtos
{
	/// <summary>
    /// 推广员管理列表Dto
    /// </summary>
    [AutoMapFrom(typeof(Promoter))]
    public class PromoterListDto : EntityDto<int>
    {
        /// <summary>
        /// 推广员姓名
        /// </summary>
        [DisplayName("推广员姓名")]
        public      string PromoterName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [DisplayName("手机")]
        public      string Mobile { get; set; }
        /// <summary>
        /// 唯一编码
        /// </summary>
        [DisplayName("唯一编码")]
        public      string OnlyCode { get; set; }
        /// <summary>
        /// 分享二维码
        /// </summary>
        [DisplayName("分享二维码")]
        public      string ShareUrl { get; set; }
        public      bool IsActive { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public      DateTime CreationTime { get; set; }
    }
}
