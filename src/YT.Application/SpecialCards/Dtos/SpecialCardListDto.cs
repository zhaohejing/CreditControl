                            
using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Models;
#region 代码生成器相关信息_ABP Code Generator Info
//你好，我是ABP代码生成器的作者,欢迎您使用该工具，目前接受付费定制该工具，有需要的可以联系我
//我的邮箱:werltm@hotmail.com
// 官方网站:"http://www.yoyocms.com"
// 交流QQ群：104390185  
//微信公众号：角落的白板报
// 演示地址:"vue版本：http://vue.yoyocms.com angularJs版本:ng1.yoyocms.com"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>梁桐铭 ,微软MVP</Author-作者>
// Copyright © YoYoCms@China.2017-09-22T14:33:12. All Rights Reserved.
//<生成时间>2017-09-22T14:33:12</生成时间>
#endregion
namespace YT.SpecialCards.Dtos
{
	/// <summary>
    /// 唯鲜卡列表Dto
    /// </summary>
    [AutoMapFrom(typeof(SpecialCard))]
    public class SpecialCardListDto : EntityDto<int>
    {
        /// <summary>
        /// 卡号
        /// </summary>
        [DisplayName("卡号")]
        public      string CardCode { get; set; }
        /// <summary>
        /// 卡密码
        /// </summary>
        [DisplayName("卡密码")]
        public      string Password { get; set; }
        /// <summary>
        /// 启用禁用
        /// </summary>
        [DisplayName("启用禁用")]
        public      bool IsActive { get; set; }
        /// <summary>
        /// 是否已使用
        /// </summary>
        [DisplayName("是否已使用")]
        public      bool IsUsed { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public      DateTime CreationTime { get; set; }
    }
}
