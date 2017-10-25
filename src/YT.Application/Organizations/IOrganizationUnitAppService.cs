using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Organizations.Dto;

namespace YT.Organizations
{ /// <summary>
  /// 组织机构service
  /// </summary>
    public interface IOrganizationUnitAppService : IApplicationService
    { /// <summary>
      /// 获取所有的组织结构
      /// </summary>
        Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnits();
        /// <summary>
        /// 获取组织机构下的人员信息
        /// </summary>
        Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input);
        /// <summary>
        /// 创建组织机构
        /// </summary>
        Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input);
        /// <summary>
        /// 更新组织机构
        /// </summary>
        Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input);
        /// <summary>
        /// 移动组织机构
        /// </summary>
        Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input);
        /// <summary>
        /// 删除组织机构 
        /// </summary>
        Task DeleteOrganizationUnit(EntityDto<long> input);
        /// <summary>
        /// 将人员添加到组织机构
        /// </summary>
        Task AddUserToOrganizationUnit(UserToOrganizationUnitInput input);
        /// <summary>
        /// 将人员移出组织机构
        /// </summary>
        Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input);
        /// <summary>
        /// 判断用户是否在组织机构内
        /// </summary>
        Task<bool> IsInOrganizationUnit(UserToOrganizationUnitInput input);
    }
}
