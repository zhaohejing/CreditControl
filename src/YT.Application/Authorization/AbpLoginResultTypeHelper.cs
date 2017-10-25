using System;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.UI;

namespace YT.Authorization
{
    /// <summary>
    /// 登陆结果帮助类
    /// </summary>
    public class AbpLoginResultTypeHelper : YtServiceBase, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="usernameOrEmailAddress"></param>
        /// <param name="tenancyName"></param>
        /// <returns></returns>
        public Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new ApplicationException("请求成功方法不可调用此异常");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("登陆失败"), L("用户名或密码错误"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("登陆失败"), L("没有该租户{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("登陆失败"), L("租户未启用", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("登陆失败"), L("用户未激活", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("登陆失败"), L("用户邮箱未验证"));
                case AbpLoginResultType.LockedOut:
                    return new UserFriendlyException(L("登陆失败"), L("用户被锁定"));
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("无法捕捉的错误: " + result);
                    return new UserFriendlyException(L("登陆失败"));
            }
        }
    }
}
