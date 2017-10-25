                            
using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Models;
namespace YT.Customers.Dtos
{
	/// <summary>
    /// 客户表列表Dto
    /// </summary>
    [AutoMapFrom(typeof(Customer))]
    public class CustomerListDto : EntityDto<int>
    {
        /// <summary>
        /// 账户
        /// </summary>
        [DisplayName("账户")]
        public      string Account { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        [DisplayName("客户姓名")]
        public      string CustomerName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [DisplayName("手机")]
        public      string Mobile { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [DisplayName("生日")]
        public      DateTime? BirthDay { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public     Gender Gender { get; set; }
        /// <summary>
        /// 推广员姓名
        /// </summary>
        public string PromoterName { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public int Balance { get; set; } 
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public      bool IsActive { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public      DateTime CreationTime { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [DisplayName("头像")]
        public  string Avatar { get; set; }
        /// <summary>
        /// 是否可以领取吸管
        /// </summary>
        [DisplayName("是否可以领取吸管")]
        public bool CanPickUpStraw { get; set; } = false;
        /// <summary>
        /// 奶瓶数量
        /// </summary>
        [DisplayName("奶瓶数量")]
        public int BottleCount { get; set; }
    }
}
