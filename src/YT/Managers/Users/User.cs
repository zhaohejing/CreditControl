using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using Microsoft.AspNet.Identity;

namespace YT.Managers.Users
{
    /// <summary>
    /// �û���
    /// </summary>
   [Table("milk_users")]
    public  class User : AbpUser<User>
    {
        #region ��̬����
        /// <summary>
        /// �˻���С��������
        /// </summary>
        public const int MinPlainPasswordLength = 6;
        /// <summary>
        /// �ֻ���󳤶�
        /// </summary>
        public const int MaxPhoneNumberLength = 24;
        public const string DefaultPassword = "123456";
        #endregion

        /// <summary>
        /// ͷ��·��id
        /// </summary>
        public Guid? ProfilePictureId { get; set; }
        #region ���������ֶ�
        /// <summary>
        /// �����ǳ���ʾ
        /// </summary>
        private new string Surname { get; set; }
        private new string PasswordResetCode { get; set; }
        //
        // ժҪ:
        //     Is the Abp.Authorization.Users.AbpUserBase.EmailAddress confirmed.
        private new bool IsEmailConfirmed { get; set; }
        //
        // ժҪ:
        //     Is the Abp.Authorization.Users.AbpUserBase.PhoneNumber confirmed.
        private new bool IsPhoneNumberConfirmed { get; set; }
        /// <summary>
        /// ����Ǳ���
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public override string EmailAddress { get; set; }
        //
        // ժҪ:
        //     Gets or sets the lockout enabled.
        private new bool IsLockoutEnabled { get; set; }

        //
        // ժҪ:
        //     Is two factor auth enabled.
        private new bool IsTwoFactorEnabled { get; set; }
        private  new string AuthenticationSource { get; set; }
        private  new string EmailConfirmationCode { get; set; }
        private new DateTime LockoutEndDateUtc { get; set; }
        #endregion

        #region ctors
        /// <summary>
        /// Creates admin <see cref="User"/> for a tenant.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="emailAddress">Email address</param>
        /// <param name="password">Password</param>
        /// <returns>Created <see cref="User"/> object</returns>
        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string password)
        {
            return new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
               // Surname = AdminUserName,
                EmailAddress = emailAddress,
                Password = new PasswordHasher().HashPassword(password)
            };
        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }
        //Can add application specific user properties here
        /// <summary>
        /// ����
        /// </summary>
        public User()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        public void Unlock()
        {
        }
        #endregion
      
    }
}