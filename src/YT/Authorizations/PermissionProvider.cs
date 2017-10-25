using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using YT.Navigations;

namespace YT.Authorizations
{
 
    public abstract class PermissionProvider : ITransientDependency
    {
        public abstract IEnumerable<PermissionDefinition> GetPermissionDefinitions(PermissionDefinitionProviderContext context);
    }
}
