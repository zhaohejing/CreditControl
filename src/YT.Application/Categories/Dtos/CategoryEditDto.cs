using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using YT.Models;

namespace YT.Categories.Dtos
{
    /// <summary>
    /// 分类编辑用Dto
    /// </summary>
    [AutoMap(typeof(Category))]
    public class CategoryEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public int? Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public int? ParentId { get; set; }

    }
}
