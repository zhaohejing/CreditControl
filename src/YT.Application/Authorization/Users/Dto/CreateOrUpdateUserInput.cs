using System.ComponentModel.DataAnnotations;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// �����༭�û� input
/// </summary>
    public class CreateOrUpdateUserInput
    {/// <summary>
    /// �û���Ϣ
    /// </summary>
        [Required]
        public UserEditDto User { get; set; }
        /// <summary>
        /// ��ɫ���Ƽ���
        /// </summary>
        [Required]
        public string[] AssignedRoleNames { get; set; }
 
        /// <summary>
        /// �Ƿ����� �������
        /// </summary>
        public bool SetDefaultPassword { get; set; }
    }
}