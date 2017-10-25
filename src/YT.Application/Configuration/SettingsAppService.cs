using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using YT.Editions;
using YT.Timing;
using YT.Configuration.Dto;

namespace YT.Configuration
{/// <summary>
 /// 设置项管理
 /// </summary>
    [DisableAuditing]
    public class SettingsAppService : YtAppServiceBase, ISettingsAppService
    {
        private readonly IRepository<Setting, long> _settingRepository;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="settingRepository"></param>
        public SettingsAppService(IRepository<Setting, long> settingRepository)
        {
            _settingRepository = settingRepository;
        }

        #region Get Settings
        /// <summary>
        /// 获取所有的配置项
        /// </summary>
        public async Task<SettingsEditDto> GetAllSettings()
        {
            return new SettingsEditDto
            {
                Permission = await GetPermissionSettingsAsync()
            };
        }
        private async Task<PermissionSettingsEditDto> GetPermissionSettingsAsync()
        {
            var settings = new PermissionSettingsEditDto()
            {
                Create = await SettingManager.GetSettingValueForApplicationAsync<bool>(YtSettings.General.UserDefaultActive)
            };
            return settings;
        }

        #endregion

        #region Update Settings
        /// <summary>
        /// 
        /// </summary>
        public async Task UpdateAllSettings(SettingsEditDto input)
        {
            await UpdatePermissionAsync(input.Permission);
        }



        private async Task UpdatePermissionAsync(PermissionSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForApplicationAsync(YtSettings.General.PermissionDefaultActive, settings.Create.ToString());
        }


        #endregion
        /// <summary>
        /// 添加配置项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateSettings(SettingInput input)
        {
            var model = input.MapTo<Setting>();

            if (input.Id.HasValue)
            {
                await _settingRepository.UpdateAsync(model);
            }
            else
            {
                model.TenantId = AbpSession.TenantId;
                await _settingRepository.InsertAsync(model);
            }

        }

    }
}