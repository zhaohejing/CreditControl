using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YT.Managers.Roles.RoleDefaults;
using YT.Navigations;

namespace YT.Managers.Users.UserDefaults
{
  public  abstract class BaseUserProvider:UserProvider
    {
    }
    public  class AdminUserProvider: BaseUserProvider
    {
        public override IEnumerable<UserDefinition> GetUserDefinitions(UserDefinitionProviderContext context)
        {
            return  new List<UserDefinition>()
            {
                new UserDefinition(StaticNames.User.Admin,"管理员","123456"),
                new UserDefinition(StaticNames.User.Default,"一般用户","123456"),
            };
        }
    }
}
