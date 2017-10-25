using System.ComponentModel.DataAnnotations;

namespace YT.WebApi.Models
{
    public class LoginModel
    {

        [Required]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
    /// <summary>
    /// ÊÖ»úµÇÂ¼
    /// </summary>
    public class MobileLoginModel
    {
        [Required]
        public string Mobile { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
