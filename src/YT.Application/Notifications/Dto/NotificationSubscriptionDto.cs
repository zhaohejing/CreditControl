using System.ComponentModel.DataAnnotations;
using Abp.Notifications;

namespace YT.Notifications.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class NotificationSubscriptionDto
    { /// <summary>
      /// 
      /// </summary>
        [Required]
        [MaxLength(NotificationInfo.MaxNotificationNameLength)]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSubscribed { get; set; }
    }
}