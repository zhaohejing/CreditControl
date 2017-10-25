using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YT.Authorization.Users.Profile.Dto
{/// <summary>
/// 更新用户头像信息
/// </summary>
    public class UpdateProfilePictureInput
    {/// <summary>
    /// 
    /// </summary>
        [Required]
        [MaxLength(400)]
        public string FileName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Height { get; set; }
    }
}