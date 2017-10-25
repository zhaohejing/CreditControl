using System.Collections.Generic;

namespace YT.Notifications.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class UpdateNotificationSettingsInput
    { /// <summary>
      /// 
      /// </summary>
        public bool ReceiveNotifications { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<NotificationSubscriptionDto> Notifications { get; set; }
    }
}