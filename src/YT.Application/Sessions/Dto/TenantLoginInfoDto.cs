using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Managers.MultiTenancy;
using YT.MultiTenancy;

namespace YT.Sessions.Dto
{ /// <summary>
  /// 
  /// </summary>
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    { /// <summary>
      /// 
      /// </summary>
        public string TenancyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EditionDisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? LogoId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LogoFileType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? CustomCssId { get; set; }
    }
}