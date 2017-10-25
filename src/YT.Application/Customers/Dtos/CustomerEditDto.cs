                          
  
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using YT.Models;
namespace YT.Customers.Dtos
{
    /// <summary>
    /// 客户表编辑用Dto
    /// </summary>
    [AutoMap(typeof(Customer))]
    public class CustomerEditDto 
    {

	/// <summary>
    ///   主键Id
    /// </summary>
    [DisplayName("主键Id")]
	public int? Id{get;set;}

        /// <summary>
        /// 余额
        /// </summary>
        [DisplayName("余额")]
        public decimal Balance { get; set; } = 0.00M;
        /// <summary>
        /// 账户
        /// </summary>
        [DisplayName("账户")]
        [Required]
        public   string  Account { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        [DisplayName("客户姓名")]
        public   string  CustomerName { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [DisplayName("手机")]
        public   string  Mobile { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [DisplayName("生日")]
        public   DateTime?  BirthDay { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public   Gender  Gender { get; set; }

        /// <summary>
        /// 点位信息(json)
        /// </summary>
        [DisplayName("点位信息(json)")]
        public   string  Position { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [DisplayName("验证码")]
        public   string  VilidateCode { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        public   string  Password { get; set; }

        /// <summary>
        /// 可能会有推广员
        /// </summary>
        [DisplayName("可能会有推广员")]
        public   int?  PromoterId { get; set; }

        /// <summary>
        /// 用户微信认证
        /// </summary>
        [DisplayName("用户微信认证")]
        public   string  UserKey { get; set; }
        /// <summary>
        /// 充值卡
        /// </summary>
        public string Card { get; set; }
        /// <summary>
        /// 可能会有为显卡
        /// </summary>
        public int? SpecialId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public   bool  IsActive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public   int?  ProveId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public   int?  CityId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public   int?  AreaId { get; set; }

    }
}
