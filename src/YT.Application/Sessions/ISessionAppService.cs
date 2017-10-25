using System.Threading.Tasks;
using Abp.Application.Services;
using YT.Sessions.Dto;

namespace YT.Sessions
{ /// <summary>
  /// 
  /// </summary>
    public interface ISessionAppService : IApplicationService
    {
        /// <summary>
        /// 获取当前用户登录信息
        /// </summary>
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
