using System.ComponentModel.DataAnnotations;
using Abp.Auditing;

namespace YT.WebApi.Models
{
    public class ResetPasswordFormViewModel
    {
        /// <summary>
        /// Encrypted tenant id.
        /// </summary>
      //  public string TenantId { get; set; }
        /// <summary>
        /// Encrypted user id.
        /// </summary>
        [Required]
        public string UserId { get; set; }
        /// <summary>
        /// ĞÂÃÜÂë
        /// </summary>
        [Required]
        public string NewPassword { get; set; }
        /// <summary>
        /// Ô­Ê¼ÃÜÂë
        /// </summary>
        [Required]
        [DisableAuditing]
        public string Password { get; set; }
    }
}