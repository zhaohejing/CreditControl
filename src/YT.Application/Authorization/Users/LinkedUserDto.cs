using System;
using Abp.Application.Services.Dto;

namespace YT.Authorization.Users
{/// <summary>
/// 
/// </summary>
    public class LinkedUserDto : EntityDto<long>
    {/// <summary>
    /// 
    /// </summary>
        public int? TenantId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TenancyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="multiTenancyEnabled"></param>
        /// <returns></returns>
        public object GetShownLoginName(bool multiTenancyEnabled)
        {
            if (!multiTenancyEnabled)
            {
                return Username;
            }

            return string.IsNullOrEmpty(TenancyName)
                ? ".\\" + Username
                : TenancyName + "\\" + Username;
        }
    }
}