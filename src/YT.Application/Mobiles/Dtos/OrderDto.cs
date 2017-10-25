

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using YT.Dto;
using YT.Models;
#region 代码生成器相关信息_ABP Code Generator Info
//你好，我是ABP代码生成器的作者,欢迎您使用该工具，目前接受付费定制该工具，有需要的可以联系我
//我的邮箱:werltm@hotmail.com
// 官方网站:"http://www.yoyocms.com"
// 交流QQ群：104390185  
//微信公众号：角落的白板报
// 演示地址:"vue版本：http://vue.yoyocms.com angularJs版本:ng1.yoyocms.com"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>梁桐铭 ,微软MVP</Author-作者>
// Copyright © YoYoCms@China.2017-09-27T11:06:51. All Rights Reserved.
//<生成时间>2017-09-27T11:06:51</生成时间>
#endregion
namespace YT.Mobiles.Dtos
{
    /// <summary>
    /// 充值记录dto
    /// </summary>
    public class ChargeRecordDto
    {
        /// <summary>
        /// key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 充值金额 单位 分
        /// </summary>
        public int ReCharge { get; set; }
        /// <summary>
        /// 充值类型
        /// </summary>
        public ReChargeType ReChargeType { get; set; }

    }

    /// <summary>
    /// 用户时间段订单input
    /// </summary>
    public class UserOrderRangeModel : UserKeyModel
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// 截至时间
        /// </summary>
        public DateTime End { get; set; }
    }
    /// <summary>
    /// 用户充值记录查询参数
    /// </summary>
    public class UserChargeModel :  PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary>
        /// openId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 充值类型
        /// </summary>
        public ReChargeType? ReChargeType { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? Start { get; set; }
        /// <summary>
        /// 截至时间
        /// </summary>
        public DateTime? End { get; set; }
        /// <summary>
        /// init
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
    /// 用户核销商品input
    /// </summary>

    public class UserUpdateOrderModel : EntityDto
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int OrderItemId { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceCode { get; set; }
    }
    /// <summary>
    /// 用户预定商品
    /// </summary>
    public class OrderProductModel : UserKeyModel
    {
        /// <summary>
        /// 订单集合
        /// </summary>
        public  List<OrderInput> Orders { get; set; }

    }
    /// <summary>
    /// 订单输入项
    /// </summary>
    public class OrderInput
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public Guid? Order { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 商品项
        /// </summary>
        public  List<ProductItem> Products { get; set; }
    }
    /// <summary>
    /// 商品项
    /// </summary>
    public class ProductItem
    {
        /// <summary>
        /// efan商品id'
        /// </summary>
        public int CommodityId { get; set; }
        /// <summary>
        /// efan 商品名
        /// </summary>
        public string Commodity { get; set; }
        /// <summary>
        /// 话费
        /// </summary>
        public int Cost { get; set; }
    }
    /// <summary>
    /// 订单编辑用Dto
    /// </summary>
    [AutoMap(typeof(Order))]
    public class OrderDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public int? Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [DisplayName("订单编号")]
        public Guid OrderNum { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public int OrderState { get; set; }
        /// <summary>
        /// 购买人客户id
        /// </summary>
        [DisplayName("购买人客户id")]
        public int CustomerId { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        [DisplayName("客户姓名")]
        public string CustomerName { get; set; }
        /// <summary>
        /// 订单下子项
        /// </summary>
        [DisplayName("订单下子项")]
        public virtual List<OrderItemDto> OrderItems { get; set; }
        /// <summary>
        /// 预定时间
        /// </summary>
        [DisplayName("预定时间")]
        public DateTime OrderTime { get; set; }
    }
    /// <summary>
    /// 订单子项dto
    /// </summary>
    [AutoMap(typeof(OrderItem))]
    public class OrderItemDto
    {
        /// <summary>
        /// key
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 订单id'
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// efan商品id'
        /// </summary>
        public int CommodityId { get; set; }
        /// <summary>
        /// efan 商品名
        /// </summary>
        public string Commodity { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderState OrderState { get; set; }
        /// <summary>
        /// 话费
        /// </summary>
        public int Cost { get; set; }
        /// <summary>
        /// efan订单
        /// </summary>
        public string EfanOrder { get; set; }
        /// <summary>
        /// 取货时间
        /// </summary>
        public DateTime? PickTime { get; set; }
    }
}
