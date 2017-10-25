using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Timing.Dto;

namespace YT.Timing
{ /// <summary>
  /// 
  /// </summary>
    public interface ITimingAppService : IApplicationService
    { /// <summary>
      /// 
      /// </summary>
        Task<ListResultDto<NameValueDto>> GetTimezones(GetTimezonesInput input);
        /// <summary>
        /// 
        /// </summary>
        Task<List<ComboboxItemDto>> GetTimezoneComboboxItems(GetTimezoneComboboxItemsInput input);
    }
}
