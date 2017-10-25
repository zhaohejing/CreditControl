using System.Collections.Generic;
using Abp.Application.Services.Dto;
using YT.Authorization.Permissions.Dto;

namespace YT.Authorization.Roles.Dto
{/// <summary>
/// ��ȡ��ɫ���� ��Ȩ����Ϣ
/// </summary>
    public class GetRoleForEditOutput
    {/// <summary>
    /// ��ɫ��Ϣ
    /// </summary>
        public RoleEditDto Role { get; set; }
        /// <summary>
        /// ��Ȩ�б�
        /// </summary>
        public List<string> GrantedPermissionNames { get; set; }
    }
}