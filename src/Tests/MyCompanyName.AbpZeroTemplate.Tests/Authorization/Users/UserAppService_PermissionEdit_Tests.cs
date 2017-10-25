using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using YT.Authorization;
using YT.Authorization.Users;
using YT.Authorization.Users.Dto;
using Shouldly;
using Xunit;
using YT.Managers.Users;

namespace YT.Tests.Authorization.Users
{
    public class UserAppService_PermissionEdit_Tests : UserAppServiceTestBase
    {
        [Fact]
        public async Task Should_Get_User_Permissions()
        {
            //Arrange
            var admin = await GetUserByUserNameAsync(User.AdminUserName);

            //Act
            var output = await UserAppService.GetUserPermissionsForEdit(new EntityDto<long>(admin.Id));
            
            //Assert
            output.GrantedPermissionNames.ShouldNotBe(null);
          //  output.Permissions.ShouldNotBe(null);
        }

        [Fact]
        public async Task Should_Update_User_Permissions()
        {
            //Arrange
            var admin = await GetUserByUserNameAsync(User.AdminUserName);
            var permissions = Resolve<IPermissionManager>()
                .GetAllPermissions()
                .Where(p => p.MultiTenancySides.HasFlag(AbpSession.MultiTenancySide))
                .ToList();
            
            //Act
            await UserAppService.UpdateUserPermissions(
                new UpdateUserPermissionsInput
                {
                    Id = admin.Id,
                    GrantedPermissionNames = permissions.Select(p => p.Name).ToList()
                });

            //Assert
            var userManager = Resolve<UserManager>();
            foreach (var permission in permissions)
            {
                (await userManager.IsGrantedAsync(admin, permission)).ShouldBe(true);
            }
        }

      
    }
}