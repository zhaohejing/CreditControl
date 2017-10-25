using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Managers.MultiTenancy;

namespace YT.MultiTenancy.Dto
{ /// <summary>
  /// 
  /// </summary>
    [AutoMap(typeof (Tenant))]
    public class TenantEditDto : EntityDto
    { /// <summary>
      /// 
      /// </summary>
        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? EditionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }
    }
}