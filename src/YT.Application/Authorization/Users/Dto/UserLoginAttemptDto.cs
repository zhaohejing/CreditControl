using System;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// 用户登录信息dto
/// </summary>

    [AutoMap(typeof(UserLoginAttempt))]
    public class UserLoginAttemptDto
    {/// <summary>
    /// 租户名
    /// </summary>
        public string TenancyName { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string UserNameOrEmail { get; set; }
        /// <summary>
        /// ip
        /// </summary>
        public string ClientIpAddress { get; set; }
        /// <summary>
        /// 客户端名
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
