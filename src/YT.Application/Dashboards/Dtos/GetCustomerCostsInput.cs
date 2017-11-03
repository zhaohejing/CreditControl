using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Models;

namespace YT.Dashboards.Dtos
{
    /// <summary>
    /// 获取用户消费记录
    /// </summary>
   public class GetCustomerCostsInput
    {
        /// <summary>
        /// 类型
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public int CustomerId { get; set; }
    }

    /// <summary>
    /// 消费记录
    /// </summary>
    [AutoMapFrom(typeof(CustomerCost))]
    public class CustomerCostListDto : EntityDto<int>
    {
        /// <summary>
        /// 金额
        /// </summary>
        public int Balance { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 消费
        /// </summary>
        public int Cost { get; set; }
        /// <summary>
        /// 剩余金额
        /// </summary>
        public int CurrentBalance { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
