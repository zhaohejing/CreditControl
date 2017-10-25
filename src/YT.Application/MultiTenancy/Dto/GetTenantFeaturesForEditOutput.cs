using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace YT.MultiTenancy.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class GetTenantFeaturesForEditOutput
    { /// <summary>
      /// 
      /// </summary>
        public List<NameValueDto> FeatureValues { get; set; }
       
      //  public List<FlatFeatureDto> Features { get; set; }
    }
}