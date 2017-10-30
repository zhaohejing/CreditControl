using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Models;

namespace YT.Products.Dtos
{
	/// <summary>
    /// 产品列表Dto
    /// </summary>
    [AutoMapFrom(typeof(Product))]
    public class ProductListDto : EntityDto<int>
    {
        /// <summary>
        /// 价格
        /// </summary>
        [DisplayName("价格")]
        public      int Price { get; set; }
        /// <summary>
        /// 二级分类
        /// </summary>
        [DisplayName("二级分类")]
        public      string LevelTwoName { get; set; }
        /// <summary>
        /// 一级分类
        /// </summary>
        public      string LevelOneName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public      string Description { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        public      string Content { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public      bool IsActive { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [DisplayName("图片")]
        public      Guid? Profile { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public      DateTime CreationTime { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
    }
}
