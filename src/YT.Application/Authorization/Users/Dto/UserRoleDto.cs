using Abp.Application.Services.Dto;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// �û���ɫdto
/// </summary>
    public class UserRoleDto
    {/// <summary>
    /// ��ɫid
    /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// /��ɫ��
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// ��ʾ��
        /// </summary>
        public string RoleDisplayName { get; set; }
        /// <summary>
        /// �Ƿ��ѷ���
        /// </summary>
        public bool IsAssigned { get; set; }
    }
}