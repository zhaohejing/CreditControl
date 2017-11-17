using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using YT.Models;

namespace YT.CustomerPreferencePrices.Dtos
{
    /// <summary>
    /// 用户折扣价格设置编辑用Dto
    /// </summary>
    [AutoMap(typeof(CustomerPreferencePrice))]
    public class CustomerPreferencePriceEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public int? Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        [DisplayName("用户id")]
        [Required]
        public int CustomerId { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        [DisplayName("产品id")]
        [Required]
        public int ProductId { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        [DisplayName("产品价格")]
        [Required]
        public double Price { get; set; }

    }
}
