                          
  
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using YT.Models;
namespace YT.Promoters.Dtos
{
    /// <summary>
    /// 推广员管理编辑用Dto
    /// </summary>
    [AutoMap(typeof(Promoter))]
    public class PromoterEditDto 
    {

	/// <summary>
    ///   主键Id
    /// </summary>
    [DisplayName("主键Id")]
	public int? Id{get;set;}

        /// <summary>
        /// 推广员姓名
        /// </summary>
        [DisplayName("推广员姓名")]
        [Required]
        public   string  PromoterName { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [DisplayName("手机")]
        public   string  Mobile { get; set; }

        /// <summary>
        /// 分享二维码
        /// </summary>
        [DisplayName("分享二维码")]
        public   string  ShareUrl { get; set; }

        public   bool  IsActive { get; set; }

    }
}
