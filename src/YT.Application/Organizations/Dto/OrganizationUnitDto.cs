using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;

namespace YT.Organizations.Dto
{ /// <summary>
  /// 
  /// </summary>
    [AutoMapFrom(typeof(OrganizationUnit))]
    public class OrganizationUnitDto : AuditedEntityDto<long>
    { /// <summary>
      /// 
      /// </summary>
        public long? ParentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MemberCount { get; set; }
   
    }
}