using System.ComponentModel.DataAnnotations;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// 创建编辑用户 input
/// </summary>
    public class CreateOrUpdateUserInput
    {/// <summary>
    /// 用户信息
    /// </summary>
        [Required]
        public UserEditDto User { get; set; }
        /// <summary>
        /// 角色名称集合
        /// </summary>
        [Required]
        public string[] AssignedRoleNames { get; set; }
 
        /// <summary>
        /// 是否设置 随机密码
        /// </summary>
        public bool SetDefaultPassword { get; set; }
    }
}