using Abp.Runtime.Validation;
using YT.Dto;

namespace YT.Customers.Dtos
{
	/// <summary>
    /// 客户信息查询Dto
    /// </summary>
    public class GetCustomerInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

		/// <summary>
	    /// 模糊查询参数
		/// </summary>
		public string CompanyName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
		public string Contact { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
		public string Mobile { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

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
