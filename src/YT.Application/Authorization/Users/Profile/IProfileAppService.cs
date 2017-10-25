using System.Threading.Tasks;
using Abp.Application.Services;
using YT.Authorization.Users.Profile.Dto;

namespace YT.Authorization.Users.Profile
{/// <summary>
/// ������Ϣ�༭
/// </summary>
    public interface IProfileAppService : IApplicationService
    {/// <summary>
    /// ������Ϣ��ȡ
    /// </summary>
    /// <returns></returns>
        Task<CurrentUserProfileEditDto> GetCurrentUserProfileForEdit();
        /// <summary>
        /// ���¸�����Ϣ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateCurrentUserProfile(CurrentUserProfileEditDto input);
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ChangePassword(ChangePasswordInput input);
        /// <summary>
        /// ����ͷ��
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateProfilePicture(UpdateProfilePictureInput input);
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        Task<GetPasswordComplexitySettingOutput> GetPasswordComplexitySetting();
    }
}
