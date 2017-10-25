

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using YT.Models;
namespace YT.Cards.Dtos
{
    /// <summary>
    /// 充值卡编辑用Dto
    /// </summary>
    [AutoMap(typeof(Card))]
    public class CardEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public int? Id { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [DisplayName("金额")]
        [Required]
        public int Money { get; set; }

        /// <summary>
        /// 是否已使用
        /// </summary>
        [DisplayName("是否已使用")]
        public bool IsUsed { get; set; }

    }
}
