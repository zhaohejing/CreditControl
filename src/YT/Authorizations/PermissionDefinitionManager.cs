using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using YT.Authorizations.Startup;
using YT.Configuration;
using YT.Handlers;
using YT.Navigations;

namespace YT.Authorizations
{
  public  class PermissionDefinitionManager : IPermissionDefinitionManager, ISingletonDependency
    {
        private readonly IPermissionConfiguration _permissionConfiguration;
        private readonly IRepository<YtPermission> _permissionRepository;
        private readonly ILevelEntityHandler<YtPermission> _levelEntityHandler;

        public PermissionDefinitionManager(IPermissionConfiguration permissionConfiguration,
            IRepository<YtPermission> permissionRepository,
            ILevelEntityHandler<YtPermission> levelEntityHandler
          )
        {
            _permissionConfiguration = permissionConfiguration;
            _permissionRepository = permissionRepository;
            _levelEntityHandler = levelEntityHandler;
        }
        public  void Initialize()
        {
            var context = new PermissionDefinitionProviderContext(this);
            foreach (var providerType in _permissionConfiguration.Providers)
            {
                using (var provider = CreateProvider<PermissionProvider>(providerType))
                {
                    var  permissions = provider.Object.GetPermissionDefinitions(context).ToList();
                    var newList = new List<PermissionDefinition>();
                    foreach (var definition in permissions)
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

        private  void AddOrUpdate(IEnumerable<PermissionDefinition> definitions)
        {
            foreach (var definition in definitions)
            {
                var per = _permissionRepository.FirstOrDefault(t => t.Name == definition.Name);
                per = per ?? new YtPermission();
                per.DisplayName = definition.DisplayName;
                per.Name = definition.Name;
                per.ParentId = definition.ParentId;
                per.Name = definition.Name;
                per.PermissionType = definition.PermissionType;
                per.IsStatic = true;
                if (per.Id == default(int))
                {

                   per.IsActive = true;
                    _levelEntityHandler.Create(per);
                }
                else
                {
                    if (per.ParentId == definition.ParentId)
                        _levelEntityHandler.UpdateAsync(per);
                    else
                        _levelEntityHandler.UpdateParent(per);
                }

                //插入子集
                if (!definition.Childs.Any()) continue;
                foreach (var t in definition.Childs)
                    t.ParentId = per.Id;
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
