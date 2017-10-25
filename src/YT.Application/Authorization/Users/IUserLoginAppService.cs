using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Authorization.Users.Dto;

namespace YT.Authorization.Users
{/// <summary>
/// �û���¼service
/// </summary>
    public interface IUserLoginAppService : IApplicationService
    {/// <summary>
    /// ��ȡ�û���¼�����Ϣ
    /// </summary>
    /// <returns></returns>
        Task<ListResultDto<UserLoginAttemptDto>> GetRecentUserLoginAttempts();
    }
}
