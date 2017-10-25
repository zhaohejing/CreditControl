using Abp.Application.Services.Dto;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// 用户角色dto
/// </summary>
    public class UserRoleDto
    {/// <summary>
    /// 角色id
    /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// /角色名
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        public string RoleDisplayName { get; set; }
        /// <summary>
        /// 是否已分配
        /// </summary>
        public bool IsAssigned { get; set; }
    }
}