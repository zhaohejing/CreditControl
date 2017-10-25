using Abp.Notifications;
using YT.Dto;

namespace YT.Notifications.Dto
{ /// <summary>
  /// 
  /// </summary>
    public class GetUserNotificationsInput : PagedInputDto
    { /// <summary>
      /// 
      /// </summary>
        public UserNotificationState? State { get; set; }
    }
}