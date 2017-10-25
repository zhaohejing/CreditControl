using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using YT.Configuration;
using YT.Handlers;
using YT.Navigations.Startup;

namespace YT.Navigations
{
  public  class MenuDefinitionManager : IMenuDefinitionManager, ISingletonDependency
  {
      private readonly IMenuConfiguration _menuConfiguration;
      private readonly IRepository<Menu> _menuRepository;
      private readonly ILevelEntityHandler<Menu> _levelEntityHandler;
      private readonly ISettingManager _settingManager;
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="menuRepository"></param>
    /// <param name="menuConfiguration"></param>
    /// <param name="levelEntityHandler"></param>
    /// <param name="settingManager"></param>
      public MenuDefinitionManager(IRepository<Menu> menuRepository,
          IMenuConfiguration menuConfiguration,
          ILevelEntityHandler<Menu> levelEntityHandler,
          ISettingManager settingManager)
      {
          _menuRepository = menuRepository;
          _menuConfiguration = menuConfiguration;
          _levelEntityHandler = levelEntityHandler;
          _settingManager = settingManager;
      }
        public  void Initialize()
        {
            var context = new MenuDefinitionProviderContext(this);

            foreach (var providerType in _menuConfiguration.Providers)
            {
                using (var provider = CreateProvider<MenuProvider>(providerType))
                {
                    List<MenuDefinition> menus = provider.Object.GetMenuDefinitions(context).ToList();
                    List<MenuDefinition> newList = new List<MenuDefinition>();
                    foreach (MenuDefinition definition in menus)
                    {
                        if (newList.Any(t => t.Name == definition.Name))
                        {
                            throw new AbpException(definition.Name);
                        }
                        newList.Add(definition);
                    }
                      AddOrUpdate(newList);
                }
            }
        }

    

      private  void AddOrUpdate(IEnumerable<MenuDefinition> definitions)
        {
            foreach (MenuDefinition definition in definitions)
            {
                Menu menu = _menuRepository.FirstOrDefault(t => t.Name == definition.Name);
                menu = menu ?? new Menu();
                menu.DisplayName = definition.DisplayName;
                menu.MetaData = definition.MetaData;
                menu.Icon = definition.Icon;
                menu.Url = definition.Url;
                menu.ParentId = definition.ParentId;
                menu.Name = definition.Name;
                menu.RequiredPermissions = definition.RequiredPermissions;
                menu.RequiresAuthentication = definition.RequiresAuthentication;
                menu.IsStatic = true;
                menu.IsActive = true;

                if (menu.Id == default(int))
                {
                    _levelEntityHandler.Create(menu);
                }
                else
                {
                    _levelEntityHandler.UpdateAsync(menu);
                }
           
                //插入子集
                if (!definition.Childs.Any()) continue;
                foreach (var t in definition.Childs)
                    t.ParentId = menu.Id;
                AddOrUpdate(definition.Childs);
            }
        }

        private IDisposableDependencyObjectWrapper<T> CreateProvider<T>(Type providerType)
        {
            IocManager.Instance.RegisterIfNot(providerType, DependencyLifeStyle.Transient);
            return IocManager.Instance.ResolveAsDisposable<T>(providerType);
        }
    }

}
