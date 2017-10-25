                           
 
using System;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using YT.Dto;


namespace YT.Promoters.Dtos
{
	/// <summary>
    /// 推广员管理查询Dto
    /// </summary>
    public class GetPromoterInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

		/// <summary>
	    /// 模糊查询参数
		/// </summary>
		public string Name { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
		public string Mobile { get; set; }
        /// <summary>
        /// 用于排序的默认值
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
			
		
                Sorting = "Id";
            }
        }
    }
}
