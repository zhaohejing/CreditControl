using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace YT.Models
{
    /// <summary>
    /// 产品
    /// </summary>
    [Table("product")]
    public class Product : CreationAuditedEntity, ISoftDelete, IPassivable
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// 一级分类
        /// </summary>
        public int LevelOneId { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public virtual Category LevelOne { get; set; }
        /// <summary>
        /// 二级分类
        /// </summary>
        public int LevelTwoId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Category LevelTwo { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public Guid? Profile { get; set; }
    }

    /// <summary>
    /// 分类
    /// </summary>
    [Table("category")]
    public class Category : CreationAuditedEntity, ISoftDelete
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual Category Parent { get; set; }

        [ForeignKey("ParentId")]
        public virtual ICollection<Category> Childern { get; set; }

        public bool IsDeleted { get; set; }
    }
    /// <summary>
    /// 订单
    /// </summary>
    [Table("order")]
    public class Order : CreationAuditedEntity, ISoftDelete
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNum { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 客户详情
        /// </summary>
        public virtual Customer Customer { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool? State { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("OrderId")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public int? FormId { get; set; }
    }
    /// <summary>
    /// 订单项
    /// </summary>
    [Table("orderitem")]
    public class OrderItem : CreationAuditedEntity, ISoftDelete
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public int TotalPrice { get; set; }
        public bool IsDeleted { get; set; }
    }
}
