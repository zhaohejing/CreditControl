using Abp.Configuration;

namespace YT.Timing.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class GetTimezoneComboboxItemsInput
    { /// <summary>
      /// 
      /// </summary>
        public SettingScopes DefaultTimezoneScope;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedTimezoneId { get; set; }
    }
}
