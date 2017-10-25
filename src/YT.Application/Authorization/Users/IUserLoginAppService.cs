using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Authorization.Users.Dto;

namespace YT.Authorization.Users
{/// <summary>
/// 用户登录service
/// </summary>
    public interface IUserLoginAppService : IApplicationService
    {/// <summary>
    /// 获取用户登录结果信息
    /// </summary>
    /// <returns></returns>
        Task<ListResultDto<UserLoginAttemptDto>> GetRecentUserLoginAttempts();
    }
}
