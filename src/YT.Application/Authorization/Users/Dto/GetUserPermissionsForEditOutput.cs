using System.Collections.Generic;
using Abp.Application.Services.Dto;
using YT.Authorization.Permissions.Dto;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// 获取用户权限信息
/// </summary>
    public class GetUserPermissionsForEditOutput
    {/// <summary>
    /// 权限集合
    /// </summary>
        public UserEditDto UserDto { get; set; }
        /// <summary>
        /// /授权权限
        /// </summary>
        public List<string> GrantedPermissionNames { get; set; }
    }
}