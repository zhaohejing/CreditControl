using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// �����û�Ȩ����Ϣ
/// </summary>
    public class UpdateUserPermissionsInput 
    {/// <summary>
    /// �û�id
    /// </summary>
        [Range(1, int.MaxValue)]
        public long Id { get; set; }
        /// <summary>
        /// Ȩ���б�
        /// </summary>
        [Required]
        public List<string> GrantedPermissionNames { get; set; }
    }
}