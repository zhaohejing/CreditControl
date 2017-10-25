using System.Collections.Generic;

namespace YT.Notifications.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class GetNotificationSettingsOutput
    { /// <summary>
      /// 
      /// </summary>
        public bool ReceiveNotifications { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<NotificationSubscriptionWithDisplayNameDto> Notifications { get; set; }
    }
}