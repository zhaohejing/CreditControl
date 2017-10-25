using System.ComponentModel.DataAnnotations;

namespace YT.Organizations.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class UserToOrganizationUnitInput
    { /// <summary>
      /// 
      /// </summary>
        [Range(1, long.MaxValue)]
        public long UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Range(1, long.MaxValue)]
        public long OrganizationUnitId { get; set; }
    }
}