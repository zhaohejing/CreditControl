using System;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// ��ȡ�û��༭��Ϣ
/// </summary>
    public class GetUserForEditOutput
    {/// <summary>
    /// ͷ��
    /// </summary>
        public Guid? ProfilePictureId { get; set; }
        /// <summary>
        /// �û���Ϣ
        /// </summary>
        public UserEditDto User { get; set; }
        /// <summary>
        /// ��ɫ����
        /// </summary>
        public UserRoleDto[] Roles { get; set; }
    }
}