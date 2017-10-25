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
        ///��ȡ���������� 
        /// </summary>
        /// <returns></returns>
        Task<SettingsEditDto> GetAllSettings();
        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAllSettings(SettingsEditDto input);

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateSettings(SettingInput input);

    }
}
