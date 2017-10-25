using Abp.WebApi.Controllers;

namespace YT.WebApi
{
    public abstract class YtApiControllerBase : AbpApiController
    {
        protected YtApiControllerBase()
        {
            LocalizationSourceName = YtConsts.LocalizationSourceName;
        }
        //protected void CheckErrors(IdentityResult identityResult)
        //{
        //    identityResult.CheckErrors(LocalizationManager);
        //}
    }
}