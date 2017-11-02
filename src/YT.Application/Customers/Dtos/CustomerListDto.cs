using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Models;

namespace YT.Customers.Dtos
{
	/// <summary>
    /// 客户信息列表Dto
    /// </summary>
    [AutoMapFrom(typeof(Customer))]
    public class CustomerListDto 
    {
        /// <summary>
        /// key
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 公司名
        /// </summary>
        [DisplayName("公司名")]
        public      string CompanyName { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        [DisplayName("省")]
        public      string Provence { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        [DisplayName("市")]
        public      string City { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [DisplayName("地址")]
        public      string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        public      string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("联系电话")]
        public      string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        public      string Email { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        public      string Account { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        [DisplayName("营业执照")]
        public      string LicenseUrl { get; set; }
        /// <summary>
        /// 资质
        /// </summary>
        public      Guid? License { get; set; }
        /// <summary>
        /// 身份证正面
        /// </summary>
        [DisplayName("身份证正面")]
        public      string IdentityCardUrl { get; set; }
        /// <summary>
        /// card
        /// </summary>
        public Guid? IdentityCard { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        [DisplayName("余额")]
        public      int Balance { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public      bool? State { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public      bool IsActive { get; set; }

  
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public      DateTime CreationTime { get; set; }
    }
}

