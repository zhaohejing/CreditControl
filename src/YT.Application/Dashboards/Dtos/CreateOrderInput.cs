using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using YT.Models;

namespace YT.Dashboards.Dtos
{
    /// <summary>
    /// 创建订单
    /// </summary>
  public  class CreateOrderInput
    {
        /// <summary>
        /// 客户id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 份数
        /// </summary>
        public int Count { get; set; }
    }
    /// <summary>
    /// 充值申请
    /// </summary>
    [AutoMap(typeof(ApplyCharge))]
    public class ApplyChargeInput
    {
        /// <summary>
        /// key
        /// </summary>
        public int? Id { get; set; }
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
        /// 操作姓名
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool? State { get; set; }
    }
}
