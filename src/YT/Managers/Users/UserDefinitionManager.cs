using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Microsoft.AspNet.Identity;
using YT.Configuration;
using YT.Managers.Roles;
using YT.Managers.Users.Startup;
namespace YT.Managers.Users
{
    public class UserDefinitionManager : IUserDefinitionManager, ITransientDependency
    {
        private readonly IUserConfiguration _userConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly ISettingManager _settingManager;
        private readonly IRepository<Role> _roleRepository;

        public UserDefinitionManager(IUserConfiguration userConfiguration,
            IRepository<User, long> userRepository,
            ISettingManager settingManager,
            IRepository<Role> roleRepository)
        {
            _userConfiguration = userConfiguration;
            _userRepository = userRepository;
            _settingManager = settingManager;
            _roleRepository = roleRepository;
        }

        public  void Initialize()
        {
            var context = new UserDefinitionProviderContext(this);
            foreach (var providerType in _userConfiguration.Providers)
            {
                using (var provider = CreateProvider<UserProvider>(providerType))
                {
                    var users = provider.Object.GetUserDefinitions(context).ToList();
                    var newList = new List<UserDefinition>();
                    foreach (var definition in users)
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

        private   void AddOrUpdate(IEnumerable<UserDefinition> definitions)
        {
            foreach (var definition in definitions)
            {
                var user = _userRepository.FirstOrDefault(t => t.UserName == definition.UserName);
                user = user ?? new User();
                user.UserName = definition.UserName;
                user.Name = definition.Name;
              //  user.Surname = definition.Name;
                user.Password = new PasswordHasher().HashPassword(definition.Password);
                user.TenantId = 1;
                user.EmailAddress = "aaaaaa";
                var roles = _roleRepository.GetAllList();
                if (user.Id == default(int))
                {
                 
                    user.Roles = roles.Select(c => new UserRole()
                    {
                        TenantId = null,
                        RoleId = c.Id
                    }).ToList();
                    user.IsActive = true;
                    _userRepository.Insert(user);
                }
                else
                {
                    _userRepository.Update(user);
                }
               
            }
        }

        private IDisposableDependencyObjectWrapper<T> CreateProvider<T>(Type providerType)
        {
            IocManager.Instance.RegisterIfNot(providerType, DependencyLifeStyle.Transient);
            return IocManager.Instance.ResolveAsDisposable<T>(providerType);
        }
    }

    public interface IUserDefinitionManager
    {
        void Initialize();
    }
}
