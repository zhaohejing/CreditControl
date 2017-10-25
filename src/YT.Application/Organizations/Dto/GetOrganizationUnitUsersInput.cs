using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;
using YT.Dto;

namespace YT.Organizations.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class GetOrganizationUnitUsersInput : PagedAndSortedInputDto, IShouldNormalize
    { /// <summary>
      /// 
      /// </summary>
        [Range(1, long.MaxValue)]
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "user.Name, user.Surname";
            }
            else if (Sorting.Contains("userName"))
            {
                Sorting = Sorting.Replace("userName", "user.userName");
            }
            else if (Sorting.Contains("addedTime"))
            {
                Sorting = Sorting.Replace("addedTime", "uou.creationTime");
            }
        }
    }
}