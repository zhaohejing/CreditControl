using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using YT.Models;

namespace YT.Adsences.Dtos
{
    /// <summary>
    /// 公告管理编辑用Dto
    /// </summary>
    [AutoMap(typeof(Adsence))]
    public class AdsenceEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public int? Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        public string Title { get; set; }
        ///内容
        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        public string Content { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>

        public bool IsActive { get; set; }

    }
}
