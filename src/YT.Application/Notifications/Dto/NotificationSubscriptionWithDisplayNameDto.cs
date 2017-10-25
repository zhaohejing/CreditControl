using Abp.AutoMapper;
using Abp.Notifications;

namespace YT.Notifications.Dto
{ /// <summary>
  /// 
  /// </summary>
    [AutoMapFrom(typeof(NotificationDefinition))]
    public class NotificationSubscriptionWithDisplayNameDto : NotificationSubscriptionDto
    { /// <summary>
      /// 
      /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }
}