using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using YT.Models;

namespace YT.Products.Dtos
{
    /// <summary>
    /// 产品编辑用Dto
    /// </summary>
    [AutoMap(typeof(Product))]
    public class ProductEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public int? Id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [DisplayName("产品名称")]
        [Required]
        public string ProductName { get; set; }

        /// <summary>
        /// 是否需要填写form表单
        /// </summary>
        [DisplayName("是否需要填写form表单")]
        [Required]
        public bool RequireForm { get; set; }

        /// <summary>
        /// 一级分类
        /// </summary>
        [DisplayName("一级分类")]
        public int LevelOneId { get; set; }

        /// <summary>
        /// 二级分类
        /// </summary>
        [DisplayName("二级分类")]
        public int LevelTwoId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Description { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        public string Content { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool IsActive { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [DisplayName("图片")]
        public Guid? Profile { get; set; }

    }
}
