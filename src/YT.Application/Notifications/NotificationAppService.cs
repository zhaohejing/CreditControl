using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Notifications;
using Abp.Runtime.Session;
using YT.Notifications.Dto;

namespace YT.Notifications
{
    /// <summary>
    /// 通知系统
    /// </summary>
    [AbpAuthorize]
    [DisableAuditing]
    public class NotificationAppService : YtAppServiceBase, INotificationAppService
    {
        private readonly INotificationDefinitionManager _notificationDefinitionManager;
        private readonly IUserNotificationManager _userNotificationManager;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        /// <summary>
        /// ctor
        /// </summary>
        public NotificationAppService(
            INotificationDefinitionManager notificationDefinitionManager,
            IUserNotificationManager userNotificationManager,
            INotificationSubscriptionManager notificationSubscriptionManager)
        {
            _notificationDefinitionManager = notificationDefinitionManager;
            _userNotificationManager = userNotificationManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisableAuditing]
        public async Task<GetNotificationsOutput> GetUserNotifications(GetUserNotificationsInput input)
        {
            var totalCount = await _userNotificationManager.GetUserNotificationCountAsync(
                AbpSession.ToUserIdentifier(), input.State
                );

            var unreadCount = await _userNotificationManager.GetUserNotificationCountAsync(
                AbpSession.ToUserIdentifier(), UserNotificationState.Unread
                );

            var notifications = await _userNotificationManager.GetUserNotificationsAsync(
                AbpSession.ToUserIdentifier(), input.State, input.SkipCount, input.MaxResultCount
                );

            return new GetNotificationsOutput(totalCount, unreadCount, notifications);
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task SetAllNotificationsAsRead()
        {
            await _userNotificationManager.UpdateAllUserNotificationStatesAsync(AbpSession.ToUserIdentifier(), UserNotificationState.Read);
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task SetNotificationAsRead(EntityDto<Guid> input)
        {
            var userNotification = await _userNotificationManager.GetUserNotificationAsync(AbpSession.TenantId, input.Id);
            if (userNotification.UserId != AbpSession.GetUserId())
            {
                throw new ApplicationException(
                    $"给定的消息{input.Id}不属于当前用户{AbpSession.GetUserId()}");
            }

            await _userNotificationManager.UpdateUserNotificationStateAsync(AbpSession.TenantId, input.Id, UserNotificationState.Read);
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task<GetNotificationSettingsOutput> GetNotificationSettings()
        {
            var output = new GetNotificationSettingsOutput
            {
                ReceiveNotifications =
                    await SettingManager.GetSettingValueAsync<bool>(NotificationSettingNames.ReceiveNotifications),
                Notifications = (await _notificationDefinitionManager
                        .GetAllAvailableAsync(AbpSession.ToUserIdentifier()))
                    .Where(nd => nd.EntityType == null) //Get general notifications, not entity related notifications.
                    .MapTo<List<NotificationSubscriptionWithDisplayNameDto>>()
            };
            var subscribedNotifications = (await _notificationSubscriptionManager
                .GetSubscribedNotificationsAsync(AbpSession.ToUserIdentifier()))
                .Select(ns => ns.NotificationName)
                .ToList();

            output.Notifications.ForEach(n => n.IsSubscribed = subscribedNotifications.Contains(n.Name));
            return output;
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task UpdateNotificationSettings(UpdateNotificationSettingsInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), NotificationSettingNames.ReceiveNotifications, input.ReceiveNotifications.ToString());

            foreach (var notification in input.Notifications)
            {
                if (notification.IsSubscribed)
                {
                    await _notificationSubscriptionManager.SubscribeAsync(AbpSession.ToUserIdentifier(), notification.Name);
                }
                else
                {
                    await _notificationSubscriptionManager.UnsubscribeAsync(AbpSession.ToUserIdentifier(), notification.Name);
                }
            }
        }
    }
}