using System;
using Abp.Runtime.Validation;
using YT.Dto;

namespace YT.Products.Dtos
{
	/// <summary>
    /// 产品查询Dto
    /// </summary>
    public class GetProductInput : PagedAndSortedInputDto, IShouldNormalize
    {
		/// <summary>
	    /// 模糊查询参数
		/// </summary>
		public string Name { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
		public bool? State { get; set; }
        /// <summary>
        /// 一级
        /// </summary>
		public int? LevelOne { get; set; }
        /// <summary>
        /// 二级
        /// </summary>
		public int? LevelTwo { get; set; }

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
    /// 获取订单
    /// </summary>
    public class GetOrderInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
		public string Product { get; set; }
        /// <summary>
        /// 代理商
        /// </summary>
		public string Account { get; set; }
        /// <summary>
        /// 表单信息
        /// </summary>
		public bool? RequireForm { get; set; }
        /// <summary>
        /// 是否完成
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 开始
        /// </summary>
		public DateTime? Start { get; set; }
        /// <summary>
        /// 截至
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
}
