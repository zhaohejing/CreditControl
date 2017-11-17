using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using YT.Dto;
using YT.Models;

namespace YT.Dashboards.Dtos
{
    /// <summary>
    /// 按条件获取商品
    /// </summary>
    public class GetProductByFilter
    {
        /// <summary>
        /// kehu id
        /// 
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        public int? CateId { get; set; }
    }

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
    /// 
    /// </summary>
    public class GetCustomerCostsPagesInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数
        /// <summary>
        /// 模糊查询参数
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 开始
        /// </summary>
        public DateTime? Start { get; set; }
        /// <summary>
        /// 结束
        /// </summary>
        public DateTime? End { get; set; }
        /// <summary>
        /// 用于排序的默认值
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime desc";
            }
        }
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
        public double Balance { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 客户名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 消费
        /// </summary>
        public double Cost { get; set; }
        /// <summary>
        /// 剩余金额
        /// </summary>
        public double CurrentBalance { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
