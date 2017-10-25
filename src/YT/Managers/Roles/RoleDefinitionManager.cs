using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization.Roles;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Castle.Components.DictionaryAdapter;
using YT.Authorizations;
using YT.Configuration;
using YT.Managers.Roles.RoleDefaults;
using YT.Managers.Roles.Startup;
using YT.Navigations;

namespace YT.Managers.Roles
{
   public class RoleDefinitionManager : IRoleDefinitionManager, ISingletonDependency
   {
       private readonly IRoleConfiguration _roleConfiguration;
       private readonly IRepository<Role> _roleRepository;
       private readonly IRepository<YtPermission> _permissionRepository;
        private readonly ISettingManager _settingManager;

       public RoleDefinitionManager(IRoleConfiguration roleConfiguration, IRepository<Role> roleRepository, ISettingManager settingManager, IRepository<YtPermission> permissionRepository)
       {
           _roleConfiguration = roleConfiguration;
           _roleRepository = roleRepository;
           _settingManager = settingManager;
           _permissionRepository = permissionRepository;
       }

       public  void Initialize()
        {
            var context = new RoleDefinitionProviderContext(this);
            foreach (var providerType in _roleConfiguration.Providers)
            {
                using (var provider = CreateProvider<RoleProvider>(providerType))
                {
                    var roles = provider.Object.GetRoleDefinitions(context).ToList();
                    var newList = new List<RoleDefinition>();
                    foreach (var definition in roles)
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

        private   void AddOrUpdate(IEnumerable<RoleDefinition> definitions)
        {
            var temp = _permissionRepository.GetAllList();

            foreach (var definition in definitions)
            {
                var role = _roleRepository.FirstOrDefault(t => t.Name == definition.Name);
                role = role ?? new Role();
                role.DisplayName = definition.DisplayName;
                role.Name = definition.Name;
                role.DisplayName = definition.DisplayName;
                role.IsStatic = true;
                role.IsDefault = definition.IsDefault;
             
                if (role.Id == default(int))
                {
                    role.IsActive = true;
                    if (role.Name.Equals(StaticNames.Role.Admin))
                    {
                        var p = temp.Where(c=>c.PermissionType==PermissionType.Common||c.PermissionType==PermissionType.Control)
                            .Select(c => new RolePermissionSetting()
                        {
                            IsGranted = true,
                            Name = c.Name,
                            TenantId = 1
                        }).ToList();
                        role.Permissions = p;
                    }

                    else if(role.Name.Equals(StaticNames.Role.Default))
                    {
                        var p = temp.Where(c => c.PermissionType == PermissionType.Common || c.PermissionType == PermissionType.Operation)
                          .Select(c => new RolePermissionSetting()
                          {
                              IsGranted = true,
                              Name = c.Name,
                              TenantId = 1
                          }).ToList();
                        role.Permissions = p;
                    }
                    _roleRepository.Insert(role);
                }
                else
                {
                    _roleRepository.Update(role);
                }
            
            }
        }

        private IDisposableDependencyObjectWrapper<T> CreateProvider<T>(Type providerType)
        {
            IocManager.Instance.RegisterIfNot(providerType, DependencyLifeStyle.Transient);
            return IocManager.Instance.ResolveAsDisposable<T>(providerType);
        }
    }

    public interface IRoleDefinitionManager
    {
        void Initialize();
    }
}
