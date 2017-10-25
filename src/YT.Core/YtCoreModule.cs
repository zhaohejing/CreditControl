using System;
using System.Reflection;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.Runtime.Caching;
using Abp.Zero;
using Abp.Zero.Configuration;
using Abp.Zero.Ldap;
using YT.Configuration;
using YT.Debugging;
using YT.Features;
using YT.Managers.MultiTenancy;
using YT.Managers.Roles;
using YT.Managers.Users;
using YT.MultiTenancy;
using YT.Notifications;

namespace YT
{
    /// <summary>
    /// Core (domain) module of the application.
    /// </summary>
    [DependsOn(typeof(AbpZeroCoreModule), typeof(AbpZeroLdapModule), typeof(AbpAutoMapperModule))]
    public class YtCoreModule : AbpModule
    {
        
        public override void PreInitialize()
        {
          //  IocManager.RegisterIfNot<IChatCommunicator, NullChatCommunicator>();
            //if (DebugHelper.IsDebug)
            //{
            //    //Disabling email sending in debug mode
            //    IocManager.Register<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
            //}
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //��Ӱ汾Ĭ������
           // Configuration.Features.Providers.Add<AppFeatureProvider>();

            //��������� Ĭ������
            Configuration.Settings.Providers.Add<AppSettingProvider>();

            //���֪ͨ��Ϣ Ĭ��
            Configuration.Notifications.Providers.Add<AppNotificationProvider>();
           
            //�Ƿ������⻧��Ϣ
            Configuration.MultiTenancy.IsEnabled = YtConsts.MultiTenancyEnabled;
            //Enable LDAP authentication (It can be enabled only if MultiTenancy is disabled!)
            //  Configuration.Modules.ZeroLdap().Enable(typeof(AppLdapAuthenticationSource));
            //��ǰ�û�δ��½ �Ƿ��¼��־ Ĭ��false
            Configuration.Auditing.IsEnabled = true;
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //Add/remove localization sources
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "YT",
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "YT.Core.Localization.AbpZeroTemplate"
                        )
                    )
                );
            //���û�������
            Configuration.Caching.Configure("Milk.WeChat.ACCESS_TOKEN", cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(2);
            });
            //Configure roles
            //  AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

        }

        public override void PostInitialize()
        {
         
          //  IocManager.Resolve<ChatUserStateWatcher>().Initialize();
          //  IocManager.Resolve<IMenuDefinitionManager>().Initialize();
        }
    }
}
