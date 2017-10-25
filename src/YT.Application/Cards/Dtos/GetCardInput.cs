                           
 
using System;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using YT.Dto;
 

namespace YT.Cards.Dtos
{
	/// <summary>
    /// 充值卡查询Dto
    /// </summary>
    public class GetCardInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

		/// <summary>
	    /// 模糊查询参数
		/// </summary>
		public string Code { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
		public bool? State { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public int? Rmb { get; set; }
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
