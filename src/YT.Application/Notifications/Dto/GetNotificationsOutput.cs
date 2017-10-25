using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Notifications;

namespace YT.Notifications.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class GetNotificationsOutput : PagedResultDto<UserNotification>
    { /// <summary>
      /// 
      /// </summary>
        public int UnreadCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public GetNotificationsOutput(int totalCount, int unreadCount, List<UserNotification> notifications)
            :base(totalCount, notifications)
        {
            UnreadCount = unreadCount;
        }
    }
}