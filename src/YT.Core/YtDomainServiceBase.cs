using Abp.Domain.Services;
using YT.Managers.Roles;
using YT.Managers.Users;

namespace YT
{
    public abstract class YtDomainServiceBase : DomainService
    {
    
        protected YtDomainServiceBase()
        {
            LocalizationSourceName = YtConsts.LocalizationSourceName;
        }
    }
}
