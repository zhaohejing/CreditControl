using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using YT.Managers.Users;

namespace YT.Authorization.Users.Dto
{
    /// <summary>
    /// �û�dto
    /// </summary>
    public class UserEditDto : IPassivable
    {
        /// <summary>
        /// Set null to create a new user. Set user's Id to update a user
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }
    
        /// <summary>
        /// �û���
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }
   
        /// <summary>
        /// �ֻ�
        /// </summary>
        [StringLength(User.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }
        /// <summary>
        ///  ����
        /// </summary>
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsActive { get; set; }

    }
}