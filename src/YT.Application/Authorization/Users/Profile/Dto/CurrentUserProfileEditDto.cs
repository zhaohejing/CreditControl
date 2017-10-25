using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using YT.Managers.Users;

namespace YT.Authorization.Users.Profile.Dto
{/// <summary>
 /// 
 /// </summary>
    [AutoMap(typeof(User))]
    public class CurrentUserProfileEditDto
    {/// <summary>
     /// 
     /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(User.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Timezone { get; set; }
    }
}