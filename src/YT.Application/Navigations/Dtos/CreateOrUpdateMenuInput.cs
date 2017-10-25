// 项目展示地址:"http://www.ddxc.org/"
 // 如果你有什么好的建议或者觉得可以加什么功能，请加QQ群：104390185大家交流沟通
// 项目展示地址:"http://www.yoyocms.com/"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>角落的白板笔</Author-作者>
// Copyright © YoYoCms@中国.2017-08-09T16:04:16. All Rights Reserved.
//<生成时间>2017-08-09T16:04:16</生成时间>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using YT.Dto;
using YT.Navigations;

namespace YT.Navigations.Dtos
{
    /// <summary>
    /// 菜单新增和编辑时用Dto
    /// </summary>
    
    public class CreateOrUpdateMenuInput  
    {
    /// <summary>
    /// 菜单编辑Dto
    /// </summary>
		public MenuEditDto  MenuEditDto {get;set;}
        /// <summary>
        /// 权限类型
        /// </summary>
        public List<DictionaryDto> PermissionTypes { get; set; }
    }
}
