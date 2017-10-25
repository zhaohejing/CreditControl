using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Authorization.Users.Dto;
using YT.Dto;

namespace YT.Authorization.Users
{
    /// <summary>
    /// 用户servec
    /// </summary>
    public interface IUserAppService : IApplicationService
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input);
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetUsersToExcel();
        /// <summary>
        /// 详情 用户+角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input);
        /// <summary>
        /// 获取权限+用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(EntityDto<long> input);
        /// <summary>
        /// 重置权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ResetUserSpecificPermissions(EntityDto<long> input);
        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateUserPermissions(UpdateUserPermissionsInput input);
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateUser(CreateOrUpdateUserInput input);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteUser(EntityDto<long> input);
    
    }
}