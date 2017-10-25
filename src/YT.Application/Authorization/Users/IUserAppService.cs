using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Authorization.Users.Dto;
using YT.Dto;

namespace YT.Authorization.Users
{
    /// <summary>
    /// �û�servec
    /// </summary>
    public interface IUserAppService : IApplicationService
    {
        /// <summary>
        /// ��ȡ�û�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input);
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetUsersToExcel();
        /// <summary>
        /// ���� �û�+��ɫ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input);
        /// <summary>
        /// ��ȡȨ��+�û�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(EntityDto<long> input);
        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ResetUserSpecificPermissions(EntityDto<long> input);
        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateUserPermissions(UpdateUserPermissionsInput input);
        /// <summary>
        /// ����û�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateUser(CreateOrUpdateUserInput input);
        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteUser(EntityDto<long> input);
    
    }
}