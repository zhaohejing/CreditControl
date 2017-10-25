using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using YT.Models;

namespace YT.Mobiles.Dtos
{
    /// <summary>
    /// 用户key实体
    /// </summary>
    public class UserKeyModel
    {
        /// <summary>
        /// openId
        /// </summary>
        public string OpenId { get; set; }
    }
  
    /// <summary>
    /// 用户key实体
    /// </summary>
    public class DealBottleModel:EntityDto
    {
        /// <summary>
        /// openId
        /// </summary>
        public int DealCount { get; set; }
    }
    /// <summary>
    /// 登陆帮助类
    /// </summary>
    public class LoginOpenIdModel
    {

        /// <summary>
        /// 客户登陆名
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// openId
        /// </summary>
        public string OpenId { get; set; }
    }
    /// <summary>
    /// 登陆帮助类
    /// </summary>
    public class LoginSpecialModel
    {
        /// <summary>
        /// 客户登陆名
        /// </summary>
        public string CardCode { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceCode { get; set; }
    }
    /// <summary>
    /// 注册model
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 客户名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// openId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
    }
    /// <summary>
    /// 绑定唯鲜卡input
    /// </summary>
    public class BindSpecialCardModel
    {
        /// <summary>
        /// 唯鲜卡号
        /// </summary>
        public string CardCode { get; set; }
        /// <summary>
        /// 唯鲜卡密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户openid
        /// </summary>
        public string OpenId { get; set; }
    }
    /// <summary>
    /// 判断是否可以取奶参数
    /// </summary>
    public class LoginKeyInput:EntityDto
    {
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceCode { get; set; }
    }
}
