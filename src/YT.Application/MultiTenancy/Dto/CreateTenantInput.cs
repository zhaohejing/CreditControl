using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.MultiTenancy;
using YT.Authorization.Users;
using YT.Managers.MultiTenancy;
using YT.Managers.Users;

namespace YT.MultiTenancy.Dto
{/// <summary>
 /// 
 /// </summary>
    public class CreateTenantInput
    {/// <summary>
     /// 
     /// </summary>
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(Tenant.TenancyNameRegex)]
        public string TenancyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(User.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(User.MaxPasswordLength)]
        public string AdminPassword { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(AbpTenantBase.MaxConnectionStringLength)]
        public string ConnectionString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool ShouldChangePasswordOnNextLogin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool SendActivationEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? EditionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }
    }
}