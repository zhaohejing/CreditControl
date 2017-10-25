
// 项目展示地址:"http://www.ddxc.org/"
// 如果你有什么好的建议或者觉得可以加什么功能，请加QQ群：104390185大家交流沟通
// 项目展示地址:"http://www.yoyocms.com/"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>角落的白板笔</Author-作者>
// Copyright © YoYoCms@中国.2017-08-09T16:04:14. All Rights Reserved.
//<生成时间>2017-08-09T16:04:14</生成时间>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using YT.Navigations;

namespace YT.Navigations.Dtos
{
    /// <summary>
    /// 菜单编辑用Dto
    /// </summary>
    [AutoMap(typeof(Menu))]
    public class MenuEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public int? Id { get; set; }

        /// <summary>
        /// 菜单名称(唯一)
        /// </summary>
        [DisplayName("菜单名称(唯一)")]
        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        /// <summary>
        /// 菜单显示名称
        /// </summary>
        [DisplayName("菜单显示名称")]
        [MaxLength(120)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 元数据
        /// </summary>
        [DisplayName("元数据")]
        public string MetaData { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [DisplayName("图标")]
        public string Icon { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        [DisplayName("链接")]
        public string Url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        [DisplayName("是否激活")]
        public bool IsActive { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        [DisplayName("上级菜单")]
        public int? ParentId { get; set; }
        /// <summary>
        /// 是否需要验证
        /// </summary>
        public bool RequiresAuthentication { get; set; }
       /// <summary>
       /// 需求权限
       /// </summary>
        public string RequiredPermissions { get; set; }

        /// <summary>
        /// 已授权权限
        /// </summary>
        public List<string> GrantedPermissionNames { get; set; }
    }
}
