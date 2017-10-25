                           
 
using System;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using YT.Dto;
namespace YT.Customers.Dtos
{
	/// <summary>
    /// 客户表查询Dto
    /// </summary>
    public class GetCustomerInput : PagedAndSortedInputDto, IShouldNormalize
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
        /// 推广员姓名
        /// </summary>
        public string PromoterName { get; set; }

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

    /// <summary>
    /// 充值input
    /// </summary>
    public class CustomerChargeInput
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 金钱
        /// </summary>
        public int Money { get; set; }
        /// <summary>
        /// 卡编号
        /// </summary>
        public string CardCode { get; set; }
    }
}
