using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Extensions;

namespace YT.WebApi.Models
{
    public class RegisterViewModel : IValidatableObject
    {
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }
      
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName.IsNullOrEmpty()) yield break;
            if (UserName.IsNullOrWhiteSpace() && Password.IsNullOrWhiteSpace())
            {
                yield return new ValidationResult("Username cannot be an email address unless it's same with your email address !");
            }
        }

    }
}