using System;
using Abp.Runtime.Validation;
using YT.Dto;

namespace YT.ChargeRecords.Dtos
{
    /// <summary>
    /// 充值记录查询Dto
    /// </summary>
    public class GetChargeRecordInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

        /// <summary>
        /// 模糊查询参数
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public int? Money { get; set; }
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

    /// <summary>
    /// 充值记录查询Dto
    /// </summary>
    public class GetHaveProductInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

        /// <summary>
        /// 模糊查询参数
        /// </summary>
        public int CustomerId { get; set; }
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
