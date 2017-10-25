using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using YT.EntityFramework;
using YT.Managers;
using YT.Managers.Roles;
using YT.Managers.Roles.RoleDefaults;
using YT.Managers.Users;

namespace YT.Migrations.Seed.Host
{
    public class HostRoleAndUserCreator
    {
        private readonly MilkDbContext _context;

        public HostRoleAndUserCreator(MilkDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateHostRoleAndUsers();
        }

        private void CreateHostRoleAndUsers()
        {
            //Admin role for host

            var adminRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null 
            && r.Name == StaticNames.Role.Admin);
            if (adminRoleForHost == null)
            {
                adminRoleForHost = _context.Roles.Add(new Role(null,
                    StaticNames.Role.Admin, StaticNames.Role.Admin) { IsStatic = true, IsDefault = true });
                _context.SaveChanges();
            }

            //admin user for host

            var adminUserForHost = _context.Users.FirstOrDefault(u => u.TenantId == null
            && u.UserName == AbpUserBase.AdminUserName);
            if (adminUserForHost == null)
            {
                adminUserForHost = _context.Users.Add(
                    new User
                    {
                        TenantId = null,
                        UserName = AbpUserBase.AdminUserName,
                        Name = "admin",
                        Surname = "admin",
                        EmailAddress = "admin@aspnetzero.com",
                        IsEmailConfirmed = true,
                      //  ShouldChangePasswordOnNextLogin = true,
                        IsActive = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });
                _context.SaveChanges();

                //Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();

                //Grant all permissions
                //var permissions = //_context.YtPermissions;
                //    PermissionFinder
                //    .GetAllPermissions(new AppAuthorizationProvider(true))
                //    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))
                //    .ToList();

                //foreach (var permission in permissions)
                //{
                //    _context.Permissions.Add(
                //        new RolePermissionSetting
                //        {
                //            TenantId = null,
                //            Name = permission.Name,
                //            IsGranted = true,
                //            RoleId = adminRoleForHost.Id
                //        });
                //}

                _context.SaveChanges();

                //User account of admin user
                _context.UserAccounts.Add(new UserAccount
                {
                    TenantId = null,
                    UserId = adminUserForHost.Id,
                    UserName = AbpUserBase.AdminUserName,
                    EmailAddress = adminUserForHost.EmailAddress
                });

                _context.SaveChanges();
            }
        }
    }
}