using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Extensions;
using Abp.IO;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using YT.Authorization.Users.Profile.Dto;
using YT.Configuration;
using YT.Security;
using YT.Storage;
using YT.Timing;
using Newtonsoft.Json;

namespace YT.Authorization.Users.Profile
{
    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize]
    public class ProfileAppService : YtAppServiceBase, IProfileAppService
    {
        private readonly IAppFolders _appFolders;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ITimeZoneService _timeZoneService;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="appFolders"></param>
        /// <param name="binaryObjectManager"></param>
        /// <param name="timezoneService"></param>
        public ProfileAppService(
            IAppFolders appFolders,
            IBinaryObjectManager binaryObjectManager,
            ITimeZoneService timezoneService)
        {
            _appFolders = appFolders;
            _binaryObjectManager = binaryObjectManager;
            _timeZoneService = timezoneService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentUserProfileEditDto> GetCurrentUserProfileForEdit()
        {
            var user = await GetCurrentUserAsync();
            var userProfileEditDto = user.MapTo<CurrentUserProfileEditDto>();

            if (Clock.SupportsMultipleTimezone)
            {
                userProfileEditDto.Timezone = await SettingManager.GetSettingValueAsync(TimingSettingNames.TimeZone);

                var defaultTimeZoneId = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.User, AbpSession.TenantId);
                if (userProfileEditDto.Timezone == defaultTimeZoneId)
                {
                    userProfileEditDto.Timezone = string.Empty;
                }
            }

            return userProfileEditDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateCurrentUserProfile(CurrentUserProfileEditDto input)
        {
            var user = await GetCurrentUserAsync();
            input.MapTo(user);
            CheckErrors(await UserManager.UpdateAsync(user));

            if (Clock.SupportsMultipleTimezone)
            {
                if (input.Timezone.IsNullOrEmpty())
                {
                    var defaultValue = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.User, AbpSession.TenantId);
                    await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), TimingSettingNames.TimeZone, defaultValue);
                }
                else
                {
                    await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), TimingSettingNames.TimeZone, input.Timezone);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ChangePassword(ChangePasswordInput input)
        {
            await CheckPasswordComplexity(input.NewPassword);

            var user = await GetCurrentUserAsync();
            CheckErrors(await UserManager.ChangePasswordAsync(user.Id, input.CurrentPassword, input.NewPassword));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateProfilePicture(UpdateProfilePictureInput input)
        {
            var tempProfilePicturePath = Path.Combine(_appFolders.TempFolder, input.FileName);

            byte[] byteArray;

            using (var fsTempProfilePicture = new FileStream(tempProfilePicturePath, FileMode.Open))
            {
                using (var bmpImage = new Bitmap(fsTempProfilePicture))
                {
                    var width = input.Width == 0 ? bmpImage.Width : input.Width;
                    var height = input.Height == 0 ? bmpImage.Height : input.Height;
                    var bmCrop = bmpImage.Clone(new Rectangle(input.X, input.Y, width, height), bmpImage.PixelFormat);

                    using (var stream = new MemoryStream())
                    {
                        bmCrop.Save(stream, bmpImage.RawFormat);
                        stream.Close();
                        byteArray = stream.ToArray();
                    }
                }
            }

            if (byteArray.LongLength > 102400) //100 KB
            {
                throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit"));
            }

            var user = await UserManager.GetUserByIdAsync(AbpSession.GetUserId());

            if (user.ProfilePictureId.HasValue)
            {
                await _binaryObjectManager.DeleteAsync(user.ProfilePictureId.Value);
            }
          
            var storedFile = new BinaryObject(tempProfilePicturePath);
            await _binaryObjectManager.SaveAsync(storedFile);

            user.ProfilePictureId = storedFile.Id;

            FileHelper.DeleteIfExists(tempProfilePicturePath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GetPasswordComplexitySettingOutput> GetPasswordComplexitySetting()
        {
            var settingValue = await SettingManager.GetSettingValueAsync(AppSettings.Security.PasswordComplexity);
            var setting = JsonConvert.DeserializeObject<PasswordComplexitySetting>(settingValue);

            return new GetPasswordComplexitySettingOutput
            {
                Setting = setting
            };
        }

        private async Task CheckPasswordComplexity(string password)
        {
            var passwordComplexitySettingValue = await SettingManager.GetSettingValueAsync(AppSettings.Security.PasswordComplexity);
            var passwordComplexitySetting = JsonConvert.DeserializeObject<PasswordComplexitySetting>(passwordComplexitySettingValue);
            var passwordComplexityChecker = new PasswordComplexityChecker();
            var passwordValid = passwordComplexityChecker.Check(passwordComplexitySetting, password);
            if (!passwordValid)
            {
                throw new UserFriendlyException(L("PasswordComplexityNotSatisfied"));
            }
        }
    }
}