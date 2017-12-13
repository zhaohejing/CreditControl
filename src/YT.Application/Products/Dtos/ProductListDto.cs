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
        /// 是否需要填写form表单
        /// </summary>
        [DisplayName("是否需要填写form表单")]
        public bool RequireForm { get; set; }
        /// <summary>
        /// 二级分类
        /// </summary>
        [DisplayName("二级分类")]
        public string LevelTwoName { get; set; }
        /// <summary>
        /// 一级分类
        /// </summary>
        public string LevelOneName { get; set; }
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
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
    }
    /// <summary>
    /// 产品详情Dto
    /// </summary>
    [AutoMapFrom(typeof(Product))]
    public class ProductDetail : EntityDto
    {
        /// <summary>
        /// 是否需要填写form表单
        /// </summary>
        [DisplayName("是否需要填写form表单")]
        public bool RequireForm { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [DisplayName("价格")]
        public double Price { get; set; }
        /// <summary>
        /// 二级分类
        /// </summary>
        [DisplayName("二级分类")]
        public string LevelTwoName { get; set; }
        /// <summary>
        /// 一级分类
        /// </summary>
        public string LevelOneName { get; set; }
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
        /// 图片
        /// </summary>
        [DisplayName("图片")]
        public string ProfileUrl { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>

        public int Count { get; set; } = 1;
        /// <summary>
        /// 是否可以购买
        /// </summary>
        public bool CanBuy { get; set; }
    }
    /// <summary>
    /// 客户已有订单
    /// </summary>
    public class OrderProductDetail
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNum { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Cate { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Profile { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public bool? State { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// 资料id
        /// </summary>
        public int? FormId { get; set; }
        /// <summary>
        /// 是否需要表单
        /// </summary>
        public bool? RequireForm { get; set; }
    }
}
