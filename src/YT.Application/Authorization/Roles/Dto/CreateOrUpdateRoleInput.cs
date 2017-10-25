using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YT.Authorization.Roles.Dto
{/// <summary>
/// 创建角色dto
/// </summary>
    public class CreateOrUpdateRoleInput 
    {/// <summary>
    /// 角色信息
    /// </summary>
        [Required]
        public RoleEditDto Role { get; set; }
        /// <summary>
        /// 权限信息
        /// </summary>
        [Required]
        public List<string> GrantedPermissionNames { get; set; }
    }
}