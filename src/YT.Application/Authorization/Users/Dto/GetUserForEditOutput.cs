using System;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// 获取用户编辑信息
/// </summary>
    public class GetUserForEditOutput
    {/// <summary>
    /// 头像
    /// </summary>
        public Guid? ProfilePictureId { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserEditDto User { get; set; }
        /// <summary>
        /// 角色集合
        /// </summary>
        public UserRoleDto[] Roles { get; set; }
    }
}