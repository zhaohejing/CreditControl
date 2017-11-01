﻿using System;
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
}