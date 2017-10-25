using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YT.MultiTenancy.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class UpdateTenantFeaturesInput
    { /// <summary>
      /// 
      /// </summary>
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public List<NameValueDto> FeatureValues { get; set; }
    }
}