using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Models;

namespace YT.CustomerPreferencePrices.Dtos
{
	/// <summary>
    /// 用户折扣价格设置列表Dto
    /// </summary>
    [AutoMapFrom(typeof(CustomerPreferencePrice))]
    public class CustomerPreferencePriceListDto 
    {
        /// <summary>
        /// key
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        [DisplayName("用户id")]
        public      int CustomerId { get; set; }
        /// <summary>
        /// 客户名
        /// </summary>
        public      string CustomerName { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string LevelName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public  string Contact { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        [DisplayName("产品id")]
        public      int ProductId { get; set; }
        /// <summary>
        /// 产品dto
        /// </summary>
        [DisplayName("产品dto")]
        public      string ProductName { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        [DisplayName("产品价格")]
        public      double? Price { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public      DateTime CreationTime { get; set; }
    }
}
