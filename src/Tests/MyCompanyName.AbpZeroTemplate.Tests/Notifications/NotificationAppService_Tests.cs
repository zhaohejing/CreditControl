using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.TestBase;
using YT.Notifications;
using Shouldly;
using Xunit;
using YT.Managers.Roles.RoleDefaults;
using YT.Managers.Users;

namespace YT.Tests.Notifications
{
    public class NotificationAppService_Tests : AppTestBase
    {
        private readonly INotificationAppService _notificationAppService;
        private readonly IAppNotifier _appNotifier;

        public NotificationAppService_Tests()
        {
            _appNotifier = Resolve<IAppNotifier>();
            _notificationAppService = Resolve<INotificationAppService>();
        }
        [Fact]
        public async Task Test_Should_Create_Notification()
        {
            LoginAsHostAdmin();
            
            var user = UsingDbContext(context => 
            context.Users.FirstOrDefault(u => u.TenantId == AbpSession.TenantId && u.UserName == AbpUserBase.AdminUserName));

          //  var user =await _useRepository.FirstOrDefaultAsync(AbpSession.UserId.Value);
           await  _appNotifier.WelcomeToTheApplicationAsync(user);
        }

        [Fact]
        public async Task Test_ChangeNotificationSettings()
        {
            var settings = await _notificationAppService.GetNotificationSettings();
            settings.ReceiveNotifications.ShouldBe(true);
            settings.Notifications.Count.ShouldBeGreaterThan(0);
        }
    }
}
