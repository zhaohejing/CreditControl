using System.Collections.Generic;
using Abp.Application.Services.Dto;
using YT.Authorization.Permissions.Dto;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// ��ȡ�û�Ȩ����Ϣ
/// </summary>
    public class GetUserPermissionsForEditOutput
    {/// <summary>
    /// Ȩ�޼���
    /// </summary>
        public UserEditDto UserDto { get; set; }
        /// <summary>
        /// /��ȨȨ��
        /// </summary>
        public List<string> GrantedPermissionNames { get; set; }
    }
}