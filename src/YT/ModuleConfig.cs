using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration.Startup;
using Abp.Notifications;
using YT.Authorizations.Startup;
using YT.Managers.Roles.Startup;
using YT.Managers.Users.Startup;
using YT.Navigations.Startup;

namespace YT
{
  public  class ModuleConfig: IModuleConfig
    {
        public ModuleConfig(
          ISettingsConfiguration settingsConfiguration,
        //  IDictionaryConfiguration dictionaryConfiguration,
          IMenuConfiguration menuConfiguration,
          IRoleConfiguration roleConfiguration,
          IUserConfiguration userConfiguration,
          INotificationConfiguration notificationConfiguration,
             IPermissionConfiguration permissions
            )
        {
            Settings = settingsConfiguration;
         //   Dictionaries = dictionaryConfiguration;
            Menus = menuConfiguration;

            Roles = roleConfiguration;
            Users = userConfiguration;
            Notifications = notificationConfiguration;
            Permissions = permissions;
        }

        public ISettingsConfiguration Settings { get; }

        public IMenuConfiguration Menus { get; }

        public IPermissionConfiguration Permissions { get; }

        public IUserConfiguration Users { get; }

        public IRoleConfiguration Roles { get; }

        public INotificationConfiguration Notifications { get; }

     //   public IPostConfiguration Posts { get; }
    }
}
