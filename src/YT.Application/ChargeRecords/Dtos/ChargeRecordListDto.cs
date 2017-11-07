using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Models;

namespace YT.ChargeRecords.Dtos
{
    /// <summary>
    /// 充值记录列表Dto
    /// </summary>
    [AutoMapFrom(typeof(ChargeRecord))]
    public class ChargeRecordListDto : EntityDto<int>
    {
        /// <summary>
        /// 客户id
        /// </summary>
        [DisplayName("客户id")]
        public int CustomerId { get; set; }
        /// <summary>
        /// 客户名
        /// </summary>
        [DisplayName("客户名")]
        public string CustomerName { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        [DisplayName("充值金额")]
        public int ChargeMoney { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [DisplayName("操作人")]
        public string ActionName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }
    }
    /// <summary>
    /// 充值申请记录
    /// </summary>
    [AutoMapFrom(typeof(ApplyCharge))]
    public class ApplyChargeListDto : EntityDto<int>
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
        public int? ActrueMoney { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool? State { get; set; }
    }
}
