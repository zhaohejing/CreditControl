
// 项目展示地址:"http://www.ddxc.org/"
 // 如果你有什么好的建议或者觉得可以加什么功能，请加QQ群：104390185大家交流沟通
// 项目展示地址:"http://www.yoyocms.com/"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
//<Author-作者>角落的白板笔</Author-作者>
// Copyright © YoYoCms@中国.2017-08-09T16:04:15. All Rights Reserved.
//<生成时间>2017-08-09T16:04:15</生成时间>
using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Navigations;

namespace YT.Navigations.Dtos
{
	/// <summary>
    /// 菜单列表Dto
    /// </summary>
    [AutoMapFrom(typeof(Menu))]
    public class MenuListDto : EntityDto<int>
    {
        /// <summary>
        /// 菜单名称(唯一)
        /// </summary>
        [DisplayName("菜单名称(唯一)")]
        public      string Name { get; set; }
        /// <summary>
        /// 菜单显示名称
        /// </summary>
        [DisplayName("菜单显示名称")]
        public      string DisplayName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public      int Sort { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [DisplayName("图标")]
        public string Icon { get; set; }
        /// <summary>
        /// 是否系统
        /// </summary>
        [DisplayName("是否系统")]
        public      bool IsStatic { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        [DisplayName("是否激活")]
        public      bool IsActive { get; set; }
        /// <summary>
        /// 上级菜单
        /// </summary>
        [DisplayName("上级菜单")]
        public      int? ParentId { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 需要的权限名称
        /// </summary>
        public string RequiredPermissions { get; set; }
    }
}
