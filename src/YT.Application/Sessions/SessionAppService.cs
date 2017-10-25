using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using YT.Sessions.Dto;

namespace YT.Sessions
{ /// <summary>
  /// 
  /// </summary>
    [AbpAuthorize]
    public class SessionAppService : YtAppServiceBase, ISessionAppService
    { /// <summary>
      /// 
      /// </summary>
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                User = (await GetCurrentUserAsync()).MapTo<UserLoginInfoDto>()
            };

            //if (AbpSession.TenantId.HasValue)
            //{
            //    output.Tenant = (await GetCurrentTenantAsync()).MapTo<TenantLoginInfoDto>();
            //}

            return output;
        }
    }
}