using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT.Dashboards.Dtos
{
    /// <summary>
    /// 登陆dto
    /// </summary>
  public  class CustomerLoginDto
    {

        /// <summary>
        /// 账户
        /// </summary>
        [Required]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
    /// <summary>
    /// 重置密码链接
    /// </summary>
    public class ResetPasswordInput
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    }
    /// <summary>
    /// 修改密码
    /// </summary>
    public class ChangeCustomerPasswordInput
    {
        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
    /// <summary>
    /// 完成订单
    /// </summary>
    public class CompleteOrderInput
    {
        /// <summary>
        /// 客户id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNum { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; }
    }

  
}
