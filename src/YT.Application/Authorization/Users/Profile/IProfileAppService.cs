using System.Threading.Tasks;
using Abp.Application.Services;
using YT.Authorization.Users.Profile.Dto;

namespace YT.Authorization.Users.Profile
{/// <summary>
/// 个人信息编辑
/// </summary>
    public interface IProfileAppService : IApplicationService
    {/// <summary>
    /// 个人信息获取
    /// </summary>
    /// <returns></returns>
        Task<CurrentUserProfileEditDto> GetCurrentUserProfileForEdit();
        /// <summary>
        /// 更新个人信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateCurrentUserProfile(CurrentUserProfileEditDto input);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ChangePassword(ChangePasswordInput input);
        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateProfilePicture(UpdateProfilePictureInput input);
        /// <summary>
        /// 获取密码
        /// </summary>
        /// <returns></returns>
        Task<GetPasswordComplexitySettingOutput> GetPasswordComplexitySetting();
    }
}
