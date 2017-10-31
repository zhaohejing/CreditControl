using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using YT.Models;

namespace YT.Customers.Dtos
{
    /// <summary>
    /// 客户信息编辑用Dto
    /// </summary>
    [AutoMap(typeof(Customer))]
    public class CustomerEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public int? Id { get; set; }

        /// <summary>
        /// 公司名
        /// </summary>
        [DisplayName("公司名")]
        public string CompanyName { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [DisplayName("省")]
        public string Provence { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [DisplayName("市")]
        public string City { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [DisplayName("地址")]
        public string Address { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("联系电话")]
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Password { get; set; }
        /// <summary>
        /// 重复输入密码
        /// </summary>
        public string RepeatPassword { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        [DisplayName("营业执照")]
        public Guid? License { get; set; }

        /// <summary>
        /// 身份证正面
        /// </summary>
        [DisplayName("身份证正面")]
        public Guid? IdentityCard { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        [DisplayName("余额")]
        public int Balance { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public bool? State { get; set; }
    }
}
