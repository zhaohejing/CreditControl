using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Organizations.Dto;

namespace YT.Organizations
{ /// <summary>
  /// ��֯����service
  /// </summary>
    public interface IOrganizationUnitAppService : IApplicationService
    { /// <summary>
      /// ��ȡ���е���֯�ṹ
      /// </summary>
        Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnits();
        /// <summary>
        /// ��ȡ��֯�����µ���Ա��Ϣ
        /// </summary>
        Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input);
        /// <summary>
        /// ������֯����
        /// </summary>
        Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input);
        /// <summary>
        /// ������֯����
        /// </summary>
        Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input);
        /// <summary>
        /// �ƶ���֯����
        /// </summary>
        Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input);
        /// <summary>
        /// ɾ����֯���� 
        /// </summary>
        Task DeleteOrganizationUnit(EntityDto<long> input);
        /// <summary>
        /// ����Ա��ӵ���֯����
        /// </summary>
        Task AddUserToOrganizationUnit(UserToOrganizationUnitInput input);
        /// <summary>
        /// ����Ա�Ƴ���֯����
        /// </summary>
        Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input);
        /// <summary>
        /// �ж��û��Ƿ�����֯������
        /// </summary>
        Task<bool> IsInOrganizationUnit(UserToOrganizationUnitInput input);
    }
}
