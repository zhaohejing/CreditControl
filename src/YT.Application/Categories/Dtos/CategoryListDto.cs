using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Models;

namespace YT.Categories.Dtos
{
	/// <summary>
    /// 分类列表Dto
    /// </summary>
    [AutoMapFrom(typeof(Category))]
    public class CategoryListDto : EntityDto<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public      string Name { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public      string ParentName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public      DateTime CreationTime { get; set; }
    }
}
