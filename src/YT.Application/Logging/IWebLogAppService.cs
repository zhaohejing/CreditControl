using Abp.Application.Services;
using YT.Dto;
using YT.Logging.Dto;

namespace YT.Logging
{ /// <summary>
  /// 
  /// </summary>
    public interface IWebLogAppService : IApplicationService
    { /// <summary>
      /// ��ȡ���µ������־
      /// </summary>
        GetLatestWebLogsOutput GetLatestWebLogs();
        /// <summary>
        /// ������־i�ļ�
        /// </summary>
        FileDto DownloadWebLogs();
    }
}
