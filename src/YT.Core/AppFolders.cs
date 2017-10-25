using Abp.Dependency;

namespace YT
{
    /// <summary>
    /// 文件路径储存对象
    /// </summary>
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        /// <summary>
        /// 临时文件
        /// </summary>
        public string TempFolder { get; set; }
        /// <summary>
        /// 模板文件夹
        /// </summary>
        public  string TempleteFolder { get; set; }
        /// <summary>
        /// 图片文件夹
        /// </summary>
        public string ImageFolder { get; set; }
        /// <summary>
        /// 日志文件夹
        /// </summary>
        public string LogsFolder { get; set; }
    }
}