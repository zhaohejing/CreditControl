using System;
using System.ComponentModel;
using Abp.AutoMapper;
using YT.Models;

namespace YT.ChargeRecords.Dtos
{
    /// <summary>
    /// 充值记录编辑用Dto
    /// </summary>
    [AutoMap(typeof(ChargeRecord))]
    public class ChargeRecordEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public int? Id { get; set; }

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
        /// 操作时间
        /// </summary>
        [DisplayName("操作时间")]
        public DateTime ActionTime { get; set; }

    }
}
