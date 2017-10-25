using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YT.Authorization.Roles.Dto
{/// <summary>
/// ������ɫdto
/// </summary>
    public class CreateOrUpdateRoleInput 
    {/// <summary>
    /// ��ɫ��Ϣ
    /// </summary>
        [Required]
        public RoleEditDto Role { get; set; }
        /// <summary>
        /// Ȩ����Ϣ
        /// </summary>
        [Required]
        public List<string> GrantedPermissionNames { get; set; }
    }
}