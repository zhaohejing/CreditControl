using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using YT.Managers.Roles;

namespace YT.Authorization.Roles.Dto
{/// <summary>
 /// 
 /// </summary>
    [AutoMap(typeof(Role))]
    public class RoleEditDto
    {/// <summary>
     /// key
     /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        [Required]
        public string DisplayName { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}