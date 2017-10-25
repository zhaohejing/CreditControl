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
    [Table("milk_card")]
    public class Card : CreationAuditedEntity, ISoftDelete
    {
        /// <summary>
        /// 充值卡 卡号
        /// </summary>
        public string CardCode { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public int Money { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 是否已使用
        /// </summary>
        public bool IsUsed { get; set; }
    }
    /// <summary>
    /// 充值记录
    /// </summary>
    [Table("milk_chargerecord")]
    public class ChargeRecord : CreationAuditedEntity
    {
        public ChargeRecord() { }

        public ChargeRecord(int customerId, int reCharge, int? cardId = null)
        {
            CustomerId = CustomerId;
            ReCharge = reCharge;
            CardId = cardId;
            ReChargeType = cardId.HasValue ? ReChargeType.Card : ReChargeType.WeChat;
        }
        /// <summary>
        /// 客户id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 客户实体
        /// </summary>
        /// <summary>
        /// 充值卡id
        /// </summary>
        public int? CardId { get; set; }
     
        /// <summary>
        /// 充值金额 单位 分
        /// </summary>
        public int ReCharge { get; set; }
        /// <summary>
        /// 充值类型
        /// </summary>
        public ReChargeType ReChargeType { get; set; }
    }
    /// <summary>
    /// 唯鲜卡
    /// </summary>
    [Table("milk_specialcard")]
    public class SpecialCard : CreationAuditedEntity, ISoftDelete, IPassivable
    {
        /// <summary>
        /// 卡号  
        /// </summary>
        public string CardCode { get; set; }
        /// <summary>
        /// 卡密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 启用禁用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 是否已使用
        /// </summary>
        public bool IsUsed { get; set; }
    }
    /// <summary>
    /// 群发记录信息
    /// </summary>
    [Table("milk_wechatrecord")]
    public class WeChatRecord : CreationAuditedEntity
    {
        /// <summary>
        /// 群发内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
    }
    /// <summary>
    /// 订单
    /// </summary>
    [Table("milk_order")]
    public class Order : CreationAuditedEntity
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public Guid OrderNum { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public OrderState OrderState { get; set; }
        /// <summary>
        /// 购买人 客户id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 实体
        /// </summary>
        public virtual Customer Customer { get; set; }
        /// <summary>
        /// 订单下子项
        /// </summary>
        [ForeignKey("OrderId")]
        public  virtual  ICollection<OrderItem> OrderItems { get; set; }
        /// <summary>
        /// 预定时间
        /// </summary>
        public  DateTime OrderTime { get; set; }
    }
    /// <summary>
    /// 订单子项
    /// </summary>
    [Table("milk_orderitem")]
    public class OrderItem : CreationAuditedEntity
    {
        /// <summary>
        /// 订单id'
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 订单实体
        /// </summary>
        public virtual  Order Order { get; set; }
        /// <summary>
        /// efan商品id'
        /// </summary>
        public int CommodityId { get; set; }
        /// <summary>
        /// efan 商品名
        /// </summary>
        public  string Commodity { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderState OrderState { get; set; }
        /// <summary>
        /// 话费
        /// </summary>
        public int Cost { get; set; }
        /// <summary>
        /// efan订单
        /// </summary>
        public string EfanOrder { get; set; }
        /// <summary>
        /// 取货时间
        /// </summary>
        public DateTime? PickTime { get; set; }
    }
    /// <summary>
    /// 验证码信息
    /// </summary>
    [Table("milk_vilidatecode")]
    public class VilidateCode : CreationAuditedEntity
    {
        public string Mobile { get; set; }
        public string Code { get; set; }
       
    }
}
