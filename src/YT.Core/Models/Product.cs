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

    }
    /// <summary>
    /// 订单项
    /// </summary>
    [Table("orderitem")]
    public class OrderItem : CreationAuditedEntity, ISoftDelete
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public int TotalPrice { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("OrderItemId")]
        public virtual ICollection<CustomerForm> CustomerForms { get; set; }
    }

    public class CustomerForm : CreationAuditedEntity, ISoftDelete
    {
        public int OrderItemId { get; set; }
        public virtual OrderItem OrderItem { get; set; }
        /// <summary>
        /// 企业名
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        public string Industry { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brands { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        public string LegalPerson { get; set; }
        /// <summary>
        /// 法人手机
        /// </summary>
        public string LegalMobile { get; set; }
        /// <summary>
        /// 品牌负责人
        /// </summary>
        public string BrandsPerson { get; set; }
        /// <summary>
        /// 品牌负责人手机
        /// </summary>
        public string BrandsMobile { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostNum { get; set; }
        /// <summary>
        /// 近三年描述
        /// </summary>
        public string MajorSecret { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public Guid? License { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public Guid? IdentityCard { get; set; }
        /// <summary>
        /// 许可正
        /// </summary>
        public Guid? PermitCard { get; set; }
        /// <summary>
        /// 公司概述
        /// </summary>
        public string CompanyOverview { get; set; }
        /// <summary>
        /// 公司发展历程
        /// </summary>
        public string CompanyHistory { get; set; }
        /// <summary>
        /// 领导人履历
        /// </summary>
        public string LeadershipResume { get; set; }

        /// <summary>
        /// 公司产品
        /// </summary>
        public string CompanyProduct { get; set; }
        /// <summary>
        /// 相关专利
        /// </summary>
        public string RelevantPatent { get; set; }
        /// <summary>
        /// 公司个人荣誉
        /// </summary>
        public string Companyhonor { get; set; }
        /// <summary>
        /// 公益事业
        /// </summary>
        public string PublicWelfareUndertakings { get; set; }
        /// <summary>
        /// 其他说名
        /// </summary>
        public string Other { get; set; }

        public bool IsDeleted { get; set; }
    }
}
