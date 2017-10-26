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
}
