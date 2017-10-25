using Abp.Application.Services;
using YT.Dto;
using YT.Logging.Dto;

namespace YT.Logging
{ /// <summary>
  /// 
  /// </summary>
    public interface IWebLogAppService : IApplicationService
    { /// <summary>
      /// 获取最新的输出日志
      /// </summary>
        GetLatestWebLogsOutput GetLatestWebLogs();
        /// <summary>
        /// 下载日志i文件
        /// </summary>
        FileDto DownloadWebLogs();
    }
}
