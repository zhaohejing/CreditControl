using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Models;

namespace YT.Products.Dtos
{
    /// <summary>
    /// 订单集合
    /// </summary>
    [AutoMapFrom(typeof(Order))]
    public class OrderListDto : EntityDto<int>
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNum { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int TotalPrice { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// 个数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 公司名
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 客户详情
        /// </summary>
        public  string Mobile { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool? State { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// dto
        /// </summary>
        public  string ProductName { get; set; }
        /// <summary>
        /// 资质认证id
        /// </summary>
        public int? FormId { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; }
    }
}
