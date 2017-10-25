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
        /// ��ȡ�û���֪ͨ��Ϣ
        /// </summary>
        Task<GetNotificationsOutput> GetUserNotifications(GetUserNotificationsInput input);
        /// <summary>
        /// ����������Ϣ�Ѷ�
        /// </summary>
        Task SetAllNotificationsAsRead();
        /// <summary>
        /// ����ָ����Ϣ�Ѷ�
        /// </summary>
        Task SetNotificationAsRead(EntityDto<Guid> input);
        /// <summary>
        /// ��ȡ �û���Ϣ����
        /// </summary>
        Task<GetNotificationSettingsOutput> GetNotificationSettings();
        /// <summary>
        /// �����û���Ϣ����
        /// </summary>
        Task UpdateNotificationSettings(UpdateNotificationSettingsInput input);
    }
}