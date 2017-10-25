using System.ComponentModel.DataAnnotations;
using Abp.Organizations;

namespace YT.Organizations.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class CreateOrganizationUnitInput
    { /// <summary>
      /// 
      /// </summary>
        public long? ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }
        /// <summary>
        /// 组织机构类型
        /// </summary>
        [Required]
        public OrganizationType Type { get; set; }
    }
}