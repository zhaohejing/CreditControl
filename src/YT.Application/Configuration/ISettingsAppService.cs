using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using YT.Configuration.Dto;

namespace YT.Configuration
{/// <summary>
/// 
/// </summary>
    public interface ISettingsAppService : IApplicationService
    {
        /// <summary>
        ///获取所有配置项 
        /// </summary>
        /// <returns></returns>
        Task<SettingsEditDto> GetAllSettings();
        /// <summary>
        /// 更新所有配置项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAllSettings(SettingsEditDto input);

        /// <summary>
        /// 添加配置项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateSettings(SettingInput input);

    }
}
