using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// 更新用户权限信息
/// </summary>
    public class UpdateUserPermissionsInput 
    {/// <summary>
    /// 用户id
    /// </summary>
        [Range(1, int.MaxValue)]
        public long Id { get; set; }
        /// <summary>
        /// 权限列表
        /// </summary>
        [Required]
        public List<string> GrantedPermissionNames { get; set; }
    }
}