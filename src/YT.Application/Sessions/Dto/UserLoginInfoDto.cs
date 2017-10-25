using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Authorization.Users;
using YT.Managers.Users;

namespace YT.Sessions.Dto
{/// <summary>
 /// 
 /// </summary>
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {/// <summary>
     /// 
     /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProfilePictureId { get; set; }
    }
}
