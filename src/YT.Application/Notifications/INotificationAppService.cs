using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Notifications.Dto;

namespace YT.Notifications
{ /// <summary>
  /// 
  /// </summary>
    public interface INotificationAppService : IApplicationService
    {
        /// <summary>
        /// 获取用户的通知消息
        /// </summary>
        Task<GetNotificationsOutput> GetUserNotifications(GetUserNotificationsInput input);
        /// <summary>
        /// 设置所有消息已读
        /// </summary>
        Task SetAllNotificationsAsRead();
        /// <summary>
        /// 设置指定消息已读
        /// </summary>
        Task SetNotificationAsRead(EntityDto<Guid> input);
        /// <summary>
        /// 获取 用户消息设置
        /// </summary>
        Task<GetNotificationSettingsOutput> GetNotificationSettings();
        /// <summary>
        /// 更新用户消息设置
        /// </summary>
        Task UpdateNotificationSettings(UpdateNotificationSettingsInput input);
    }
}