using System.ComponentModel.DataAnnotations;
using Abp.Organizations;

namespace YT.Organizations.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class UpdateOrganizationUnitInput
    { /// <summary>
      /// 
      /// </summary>
        [Range(1, long.MaxValue)]
        public long Id { get; set; }
        /// <summary>
        /// ��ʾ����
        /// </summary>
        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }
        /// <summary>
        /// ��֯��������
        /// </summary>
        [Required]
        public OrganizationType Type { get; set; }
    }
}