using Abp.Runtime.Validation;
using YT.Dto;

namespace YT.MultiTenancy.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class GetTenantsInput : PagedAndSortedInputDto, IShouldNormalize
    { /// <summary>
      /// 
      /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "TenancyName";
            }

            Sorting = Sorting.Replace("editionDisplayName", "Edition.DisplayName");
        }
    }
}

