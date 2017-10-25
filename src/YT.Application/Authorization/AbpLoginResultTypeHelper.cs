using System;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.UI;

namespace YT.Authorization
{
    /// <summary>
    /// ��½���������
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
                    return new ApplicationException("����ɹ��������ɵ��ô��쳣");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("��½ʧ��"), L("�û������������"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("��½ʧ��"), L("û�и��⻧{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("��½ʧ��"), L("�⻧δ����", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("��½ʧ��"), L("�û�δ����", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("��½ʧ��"), L("�û�����δ��֤"));
                case AbpLoginResultType.LockedOut:
                    return new UserFriendlyException(L("��½ʧ��"), L("�û�������"));
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("�޷���׽�Ĵ���: " + result);
                    return new UserFriendlyException(L("��½ʧ��"));
            }
        }
    }
}
