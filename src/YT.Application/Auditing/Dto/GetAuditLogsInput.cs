using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using Abp.Timing;
using YT.Dto;

namespace YT.Auditing.Dto
{/// <summary>
/// 
/// </summary>
    public class GetAuditLogsInput : PagedAndSortedInputDto, IShouldNormalize
    {/// <summary>
    /// 
    /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ServiceName { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "ExecutionTime DESC";
            }

            if (Sorting.IndexOf("UserName", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                Sorting = "User." + Sorting;
            }
            else
            {
                Sorting = "AuditLog." + Sorting;
            }
        }
    }
}