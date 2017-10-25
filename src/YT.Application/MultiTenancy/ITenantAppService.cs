using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.MultiTenancy.Dto;

namespace YT.MultiTenancy
{ /// <summary>
  /// 
  /// </summary>
    public interface ITenantAppService : IApplicationService
    { /// <summary>
      /// 
      /// </summary>
        Task<PagedResultDto<TenantListDto>> GetTenants(GetTenantsInput input);
        /// <summary>
        /// 
        /// </summary>
        Task CreateTenant(CreateTenantInput input);
        /// <summary>
        /// 
        /// </summary>
        Task<TenantEditDto> GetTenantForEdit(EntityDto input);
        /// <summary>
        /// 
        /// </summary>
        Task UpdateTenant(TenantEditDto input);
        /// <summary>
        /// 
        /// </summary>
        Task DeleteTenant(EntityDto input);
        /// <summary>
        /// 
        /// </summary>
        Task<GetTenantFeaturesForEditOutput> GetTenantFeaturesForEdit(EntityDto input);
        /// <summary>
        /// 
        /// </summary>
        Task UpdateTenantFeatures(UpdateTenantFeaturesInput input);
        /// <summary>
        /// 
        /// </summary>
        Task ResetTenantSpecificFeatures(EntityDto input);
        /// <summary>
        /// 
        /// </summary>
        Task UnlockTenantAdmin(EntityDto input);
    }
}
