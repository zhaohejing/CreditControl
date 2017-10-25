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
    /// 客户表
    /// </summary>
    [Table("milk_customer")]
  public  class Customer :CreationAuditedEntity,ISoftDelete,IPassivable
    {
        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// 是否可以领取吸管
        /// </summary>
        public bool CanPickUpStraw { get; set; } = false;
        /// <summary>
        /// 奶瓶数量
        /// </summary>
        public int BottleCount { get; set; } 

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 点位信息(json)
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 可能会有推广员
        /// </summary>
        public int? PromoterId { get; set; }
        /// <summary>
        /// 推广员信息
        /// </summary>
        public virtual  Promoter Promoter { get; set; }
        /// <summary>
        /// 用户微信认证
        /// </summary>
        public string UserKey { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public int Balance { get; set; } = 0;

        public int? ProveId { get; set; }
        public virtual Area Prove { get; set; }
        public int? CityId { get; set; }
        public virtual Area City { get; set; }
        public int? AreaId { get; set; }
        public virtual Area Area { get; set; }
        /// <summary>
        /// 可能会有为显卡
        /// </summary>
        public  int? SpecialId { get; set; }
        public  virtual  SpecialCard Special { get; set; }
    }
    /// <summary>
    /// 用户吸管领取记录
    /// </summary>
    [Table("milk_straw")]
    public class Straw : CreationAuditedEntity
    {
        public int CustomerId { get; set; } 
    }
    /// <summary>
    /// 推广员管理
    /// </summary>
    [Table("milk_promoters")]
    public  class  Promoter:CreationAuditedEntity, ISoftDelete, IPassivable
    {
        /// <summary>
        /// 推广员姓名
        /// </summary>
        public string PromoterName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 唯一编码
        /// </summary>
        public string OnlyCode { get; set; }
        /// <summary>
        /// 分享二维码
        /// </summary>
        public string ShareUrl { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
  
    }

    /// <summary>
    /// 省市区表
    /// </summary>
    [Table("milk_areas")]
    public class Area:CreationAuditedEntity
    {
        /// <summary>
        /// 名称  
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 区域名
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 上级
        /// </summary>
        public virtual  Area Parent { get; set; }
    }


}
