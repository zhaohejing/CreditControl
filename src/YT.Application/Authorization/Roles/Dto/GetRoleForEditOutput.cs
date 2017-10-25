using System.Collections.Generic;
using Abp.Application.Services.Dto;
using YT.Authorization.Permissions.Dto;

namespace YT.Authorization.Roles.Dto
{/// <summary>
/// 获取角色详情 带权限信息
/// </summary>
    public class GetRoleForEditOutput
    {/// <summary>
    /// 角色信息
    /// </summary>
        public RoleEditDto Role { get; set; }
        /// <summary>
        /// 授权列表
        /// </summary>
        public List<string> GrantedPermissionNames { get; set; }
    }
}