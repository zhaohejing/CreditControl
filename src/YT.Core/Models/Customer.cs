using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace YT.Models
{
    /// <summary>
    /// 客户信息
    /// </summary>
    [Table("customer")]
   public class Customer:CreationAuditedEntity,ISoftDelete
    {
        /// <summary>
        /// 公司名
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Provence { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Required,MaxLength(200)]

        public string Email { get; set; }
        /// <summary>
        /// 邮箱验证码
        /// </summary>
        public string EmailCode { get; set; }
        /// <summary>
        /// 代码发送时间
        /// </summary>
        public DateTime? CodeTime { get; set; }
        [Required, MaxLength(200)]
        public string Account { get; set; }
        [Required, MaxLength(200)]

        public string Password { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public Guid? License { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public Guid? IdentityCard { get; set; }
       

        /// <summary>
        /// 余额
        /// </summary>
        public double Balance { get; set; } = 0;
        /// <summary>
        /// 状态
        /// </summary>
        public bool? State { get; set; }

        public bool IsDeleted { get; set; }
        /// <summary>
        /// 审核意见
        /// </summary>
        public string AuditOpinion { get; set; }
    }
    /// <summary>
    /// 用户折扣价格设置
    /// </summary>
    [Table("CustomerPreferencePrice")]
    public class CustomerPreferencePrice : CreationAuditedEntity, ISoftDelete
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int CustomerId { get; set; }
        public virtual  Customer Customer { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public  int ProductId { get; set; }
        /// <summary>
        /// 产品dto
        /// </summary>
        public  virtual  Product Product { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public double Price { get; set; }

        public bool IsDeleted { get; set; }
    }
    /// <summary>
    /// 用户消费记录
    /// </summary>
    [Table("CustomerCost")]
    public class CustomerCost : CreationAuditedEntity, ISoftDelete
    {
        /// <summary>
        /// 金额
        /// </summary>
        public double Balance { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 消费
        /// </summary>
        public double Cost { get; set; }
        /// <summary>
        /// 剩余金额
        /// </summary>
        public double CurrentBalance { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
