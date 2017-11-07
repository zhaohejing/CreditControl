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
    /// 充值记录
    /// </summary>
    [Table("chargerecord")]
   public class ChargeRecord:CreationAuditedEntity
    {
        /// <summary>
        ///客户id
        /// </summary>
        public int CustomerId { get; set; } 
        /// <summary>
        /// 客户名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public int ChargeMoney { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string ActionName { get; set; }
    
    }
    /// <summary>
    /// 充值申请记录
    /// </summary>
    [Table("applycharge")]
    public class ApplyCharge : CreationAuditedEntity,ISoftDelete
    {
        /// <summary>
        ///客户id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 客户名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public int ChargeMoney { get; set; }
        /// <summary>
        /// 实际到账金额
        /// </summary>
        public int?  ActrueMoney { get; set; }
        /// <summary>
        /// 操作姓名
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool? State { get; set; }
        public bool IsDeleted { get; set; }
    }
}
