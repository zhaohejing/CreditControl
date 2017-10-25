using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Navigation;
using Abp.Dependency;

namespace YT.Navigations
{
  
    public abstract class MenuProvider : ITransientDependency
    {
        public abstract IEnumerable<MenuDefinition> GetMenuDefinitions(MenuDefinitionProviderContext context);
    }
}
