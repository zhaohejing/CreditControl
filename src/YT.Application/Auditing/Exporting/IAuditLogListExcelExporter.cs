using System.Collections.Generic;
using YT.Auditing.Dto;
using YT.Dto;

namespace YT.Auditing.Exporting
{
    /// <summary>
    /// helper
    /// </summary>
    public interface IAuditLogListExcelExporter
    {
        /// <summary>
        /// µ¼³ö
        /// </summary>
        /// <param name="auditLogListDtos"></param>
        /// <returns></returns>
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
