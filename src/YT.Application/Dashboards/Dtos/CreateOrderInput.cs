using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
