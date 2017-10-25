using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Auditing.Dto;
using YT.Dto;

namespace YT.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuditLogAppService : IApplicationService
    {
        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input);
        /// <summary>
        /// 导出日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input);
    }
}