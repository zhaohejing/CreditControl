using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration.Startup;
using Abp.Notifications;
using YT.Navigations.Startup;

namespace YT
{
    public interface IModuleConfig
    {
         ISettingsConfiguration Settings { get; }

         IMenuConfiguration Menus { get; }

        //   public IUserConfiguration Users { get; }

        //   public IDictionaryConfiguration Dictionaries { get; }

        //   public IRoleConfiguration Roles { get; }

         INotificationConfiguration Notifications { get; }
    }
}
