using Abp.Authorization;
using Abp.Localization;
using Abp.Notifications;

namespace YT.Notifications
{
    public class AppNotificationProvider : NotificationProvider
    {
        public override void SetNotifications(INotificationDefinitionContext context)
        {
            context.Manager.Add(
                new NotificationDefinition(
                    AppNotificationNames.NewUserRegistered,
                    displayName: L("新用户通知")
                   // , permissionDependency: new SimplePermissionDependency()
                    )
                );

            context.Manager.Add(
                new NotificationDefinition(
                    AppNotificationNames.NewTenantRegistered,
                    displayName: L("新租户通知")
                   // , permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenants)
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, YtConsts.LocalizationSourceName);
        }
    }
}
