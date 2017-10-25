using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YT.Navigations;

namespace YT.Managers.Roles.RoleDefaults
{
  public abstract   class BaseRoleProvider:RoleProvider
    {
    }

    public class AdminRoleProvider : BaseRoleProvider
    {
        public override IEnumerable<RoleDefinition> GetRoleDefinitions(RoleDefinitionProviderContext context)
        {
            return new List<RoleDefinition>()
            {
                new RoleDefinition(StaticNames.User.Admin,"管理员",false,true),
                new RoleDefinition(StaticNames.User.Default,"默认用户",true,false),
            };
        }
    }
}
