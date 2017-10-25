using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using YT.Managers.MultiTenancy;

namespace YT.MultiTenancy.Dto
{ /// <summary>
  /// 
  /// </summary>
    [AutoMapFrom(typeof (Tenant))]
    public class TenantListDto : EntityDto, IPassivable, IHasCreationTime
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
        public string ConnectionString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}