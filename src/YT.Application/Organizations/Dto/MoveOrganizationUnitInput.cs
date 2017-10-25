using System.ComponentModel.DataAnnotations;

namespace YT.Organizations.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class MoveOrganizationUnitInput
    { /// <summary>
      /// 
      /// </summary>
        [Range(1, long.MaxValue)]
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? NewParentId { get; set; }
    }
}