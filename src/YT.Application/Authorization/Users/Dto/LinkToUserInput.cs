using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// 
/// </summary>
    public class LinkToUserInput 
    {/// <summary>
    /// 
    /// </summary>
        public string TenancyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string UsernameOrEmailAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}